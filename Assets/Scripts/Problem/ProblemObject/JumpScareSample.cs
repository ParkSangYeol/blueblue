using System;
using System.Collections;
using System.Collections.Generic;
using Problem.Object;
using Unity.VisualScripting;
using UnityEngine;

namespace Problem.Object
{
    public class JumpScareSample : ProblemObject
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

