using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Anomaly.Object
{
    public class JumpScareSample : AnomalyObject
    {
        public GameObject scareObject;

        private void Start()
        {
            base.Start();
            scareObject.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ActivePhenomenon();
            }
        }

        protected override void ActivePhenomenon()
        {
            scareObject.SetActive(true);
        }

        public override void ResetProblem()
        {
            scareObject.SetActive(false);
        }
    }
}

