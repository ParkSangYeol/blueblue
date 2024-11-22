using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly
{
    public class AnomalyUnLoader : MonoBehaviour
    {
        [SerializeField] 
        private BoxCollider boxCollider;

        public AnomalyLoader loader;
        private void Awake()
        {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // 맵 언로드
                loader.UnloadProblem();
                Destroy(this);
            }
        }
    }
}