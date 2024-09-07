using com.kleberswf.lib.core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace System.Scenes
{
    public class SceneController : Singleton<SceneController>
    {
        private Scene currentMainScene;
        private Scene leftScene;
        private Scene rightScene;

        private void Start()
        {
            currentMainScene = SceneManager.GetActiveScene();
        }

        public void ReplaceScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            currentMainScene = SceneManager.GetSceneByName(sceneName);
        }

        public void ReplaceScene(int idx)
        {
            SceneManager.LoadScene(idx, LoadSceneMode.Single);
            currentMainScene = SceneManager.GetSceneAt(idx);
        }

        public void AddScene(string sceneName, SceneAddType type)
        {
            switch (type)
            {
                case SceneAddType.LEFT:
                    SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (obj) =>
                    {
                        obj.allowSceneActivation = true;
                        leftScene = SceneManager.GetSceneByName(sceneName);
                    };
                    break;
                case SceneAddType.RIGHT:
                    SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (obj) =>
                    {
                        obj.allowSceneActivation = true;
                        rightScene = SceneManager.GetSceneByName(sceneName);
                    };
                    break;
            }
        }

        public void AddScene(int idx, SceneAddType type)
        {
            switch (type)
            {
                case SceneAddType.LEFT:
                    SceneManager.LoadSceneAsync(idx, LoadSceneMode.Additive).completed += (obj) =>
                    {
                        obj.allowSceneActivation = true;
                        leftScene = SceneManager.GetSceneAt(idx);
                    };
                    break;
                case SceneAddType.RIGHT:
                    SceneManager.LoadSceneAsync(idx, LoadSceneMode.Additive).completed += (obj) =>
                    {
                        obj.allowSceneActivation = true;
                        rightScene = SceneManager.GetSceneAt(idx);
                    };
                    break;
            }
        }

        /// <summary>
        /// 특정 타입에 해당하는 씬 제거
        /// </summary>
        /// <param name="type"></param>
        public void RemoveScene(SceneAddType type)
        {
            switch (type)
            {
                case SceneAddType.LEFT:
                    SceneManager.UnloadSceneAsync(leftScene);
                    break;
                case SceneAddType.RIGHT:
                    SceneManager.UnloadSceneAsync(rightScene);
                    break;
            }
        }
        
        /// <summary>
        /// 왼쪽, 오른쪽 방 중 해당 방을 제외한 나머지 씬을 제거
        /// </summary>
        /// <param name="type">남길 방을 포함하는 씬</param>
        public void RemoveSceneWithout(SceneAddType type)
        {
            SceneManager.UnloadSceneAsync(currentMainScene);
            switch (type)
            {
                case SceneAddType.LEFT:
                    // LEFT씬을 제외한 모든 씬 제거
                    RemoveScene(SceneAddType.RIGHT);
                    currentMainScene = leftScene;
                    break;
                case SceneAddType.RIGHT:
                    // RIGHT씬을 제외한 모든 씬 제거
                    RemoveScene(SceneAddType.LEFT);
                    currentMainScene = rightScene;
                    break;
            }
        }

        /// <summary>
        /// 씬 로드 후 한 번만 실행되는 이벤트 추가
        /// </summary>
        /// <param name="action">추가할 Action</param>
        public void AddOneShotLoadedEvent(UnityAction action)
        {
            UnityAction<Scene, LoadSceneMode> handler = null;
            handler = (scene, mode) =>
            {
                action.Invoke();
                SceneManager.sceneLoaded -= handler;
            };
            
            SceneManager.sceneLoaded += handler;
        }
        
        /// <summary>
        /// 씬 언 로드 후 한 번만 실행되는 이벤트 추가
        /// </summary>
        /// <param name="action">추가할 Action</param>
        public void AddOneShotUnLoadedEvent(UnityAction action)
        {
            UnityAction<Scene> handler = null;
            handler = (scene) =>
            {
                action.Invoke();
                SceneManager.sceneUnloaded -= handler;
            };
            
            SceneManager.sceneUnloaded += handler;
        }
    }
}