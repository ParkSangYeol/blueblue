using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace UnityToolbarExtender.Examples
{
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;
        public static readonly GUIStyle commandInputFieldStyle;

        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Button")
            {
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Normal,
            };

            commandInputFieldStyle = new GUIStyle("TextField")
            {
                alignment = TextAnchor.MiddleLeft,
                fontStyle = FontStyle.Normal,
                fixedWidth = 150,
            };

        }
    }


    [InitializeOnLoad]
    public class SceneSwitchLeftButton
    {
        static SceneSwitchLeftButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("StartGame", "최초 세팅 씬부터 게임을 진입합니다."), ToolbarStyles.commandButtonStyle))
            {
                string[] guids = AssetDatabase.FindAssets("t:scene InitializeScene", new[] { "Assets/Scenes/Builds" });
                SceneHelper.StartScene(guids[0], true);
            }
        }
    }

    [InitializeOnLoad]
    public class SceneSwitchRightButton
    {
        static GenericMenu menu;
        static bool isAutoPlay = false;

        static SceneSwitchRightButton()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            EditorGUIUtility.labelWidth = 60;
            isAutoPlay = EditorGUILayout.Toggle(new GUIContent("자동 시작"), isAutoPlay);

            if (EditorGUILayout.DropdownButton(new GUIContent("씬 선택 이동"), FocusType.Keyboard))
            {
                var allScenes = SceneHelper.FindAllScenes();

                menu = new GenericMenu();

                for (var i = 0; i < allScenes.Count; ++i)
                {
                    menu.AddItem(new GUIContent($"{allScenes[i].Item1}"), false, OnClickDropdown, allScenes[i].Item2);
                }

                menu.ShowAsContext();
            }

            GUILayout.FlexibleSpace();
        }

        private static void OnClickDropdown(object parm)
        {
            SceneHelper.StartScene((string)parm, isAutoPlay);
        }
    }


    static class SceneHelper
    {
        static string sceneGUID;
        static bool isAutoPlay = false;

        public static void StartScene(string sceneGUID, bool isPlay)
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            SceneHelper.sceneGUID = sceneGUID;
            isAutoPlay = isPlay;

            EditorApplication.update += OnUpdate;
        }

        public static List<(string, string)> FindAllScenes()
        {
            List<(string, string)> result = new List<(string, string)>();

            string[] guids = AssetDatabase.FindAssets("t:scene ", new[] { "Assets/Scenes" });

            foreach (string guid in guids)
            {

                string scenePath = AssetDatabase.GUIDToAssetPath(guid);
                var fileName = Path.GetFileName(scenePath);
                result.Add(new(scenePath.Replace("Assets/Scenes/", "").Replace(".unity", ""), guid));
            }

            return result;
        }


        static void OnUpdate()
        {
            if (sceneGUID == null ||
                EditorApplication.isPlaying || EditorApplication.isPaused ||
                EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            EditorApplication.update -= OnUpdate;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // need to get scene via search because the path to the scene
                // file contains the package version so it'll change over time
                string scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);
                EditorSceneManager.OpenScene(scenePath);
                EditorApplication.isPlaying = isAutoPlay;
            }

            isAutoPlay = false;
            sceneGUID = null;
        }
    }
}