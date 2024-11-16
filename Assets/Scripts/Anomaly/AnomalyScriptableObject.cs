using System.Collections;
using System.Collections.Generic;
using Anomaly;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anomaly
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ProblemData", fileName = "ProblemData")]
    public class AnomalyScriptableObject : ScriptableObject
    {
        [EnumToggleButtons, HideLabel]
        public AnomalyType type;
        [AssetsOnly]
        public GameObject problemPrefab;
    }
}