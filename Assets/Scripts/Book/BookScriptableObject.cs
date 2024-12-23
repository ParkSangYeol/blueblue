using System.Collections;
using System.Collections.Generic;
using Anomaly;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Book
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BookData", fileName = "BookData")]
    public class BookScriptableObject : SerializedScriptableObject
    {
        public SortedDictionary<AnomalyType, List<PageScriptableObject>> pages;
    }
}