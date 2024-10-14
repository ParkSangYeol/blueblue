using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Problem
{
    public class NextProblemChoicer : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider choiceCollider;
        public bool isTrueChoiceArea;
        public ProblemsLoader loader;
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