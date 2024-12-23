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
        public string anomalyName;
        public Sprite photo;
        public string description;
    }
}
