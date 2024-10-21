using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Problem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StageData")]
    public class StageScriptableObject : ScriptableObject
    {
        public string stageName;
        [EnumToggleButtons, HideLabel]
        public ProblemType type;
        [AssetsOnly]
        public GameObject defaultPrefab;
        public List<ProblemScriptableObject> problems;
        
        [Button]
        public void MakeProblemsList()
        {
            problems = new List<ProblemScriptableObject>();
            string[] guids = AssetDatabase.FindAssets("t:ProblemScriptableObject");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ProblemScriptableObject problemData = AssetDatabase.LoadAssetAtPath<ProblemScriptableObject>(path);
                if (problemData.type.Equals(this.type))
                {
                    problems.Add(problemData);
                }
            }
        }
    }
}