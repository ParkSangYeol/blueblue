using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Problem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ProblemData", fileName = "ProblemData")]
    public class ProblemScriptableObject : ScriptableObject
    {
        [EnumToggleButtons, HideLabel]
        public ProblemType type;
        [AssetsOnly]
        public GameObject problemPrefab;
    }
}