using Anomaly.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly.Object
{
    public class EndBlackOut1 : MonoBehaviour
    {
        private BlackOutSight blackOutSight;
        private bool isTriggered = false;

        private void Start()
        {
            blackOutSight = GetComponentInParent<BlackOutSight>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isTriggered)
            {
                isTriggered = true;
                blackOutSight.ResetProblem();
            }
        }

        public void ResetAnomaly()
        {
            isTriggered = false;
        }
    }
}
