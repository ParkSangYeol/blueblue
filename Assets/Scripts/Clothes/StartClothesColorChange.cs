using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly
{
    public class StartClothesColorChange : MonoBehaviour
    {
        private Animator animator;
        private bool isTriggered = false;

        private void Start()
        {
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
                animator.SetBool("IsVisible", true);
            }
        }
    }
}