using System.Collections;
using System.Collections.Generic;
using Anomaly;
using UnityEngine;

namespace Book
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PageData", fileName = "PageData")]
    public class PageScriptableObject : ScriptableObject
    {
        public AnomalyType anomalyType;
        public AnomalyScriptableObject anomalyData;
        public Sprite photo;
        public string description;
    }
}
