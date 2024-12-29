using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly
{
    public class NextAnomalyChoicer : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider choiceCollider;
        public bool isTrueChoiceArea;
        public AnomalyLoader loader;
        private void Awake()
        {
            if (choiceCollider == null)
            {
                choiceCollider = GetComponent<BoxCollider>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                // 플레이어 입장
                loader.MakeChoice(isTrueChoiceArea);
            }
        }
    }
}