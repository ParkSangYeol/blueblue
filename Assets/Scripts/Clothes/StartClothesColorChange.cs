using System.Collections;
using System.Collections.Generic;
using Anomaly.Object;
using UnityEngine;
using Plugins.Animate_UI_Materials;

namespace Anomaly
{
    public class StartClothesColorChange : AnomalyObject
    {
        private Animator animator;
        private bool isTriggered = false;
        private void Start()
        {
            base.Start();
            animator = GetComponentInParent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator not found in parent objects.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isTriggered)
            {
                isTriggered = true;
                Debug.Log("Player entered trigger zone, changing clothes color.");
                ActivePhenomenon();
            }
        }

        protected override void ActivePhenomenon()
        {
            animator.SetBool("IsVisible", true);
            animator.SetBool("IsEnd", false);
        }

        public override void ResetProblem()
        {
            isTriggered= false;
            animator.SetBool("IsEnd", true);
            animator.SetBool("IsVisible", false);
        }
    }
}