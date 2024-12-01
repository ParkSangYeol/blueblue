using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private static readonly int Close = Animator.StringToHash("Close");
        private static readonly int Open = Animator.StringToHash("Open");

        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
        }

        public void OpenDoor()
        {
            Debug.Log("문 열기");
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("door_open"))
            {
                return;
            }
            animator.SetTrigger(Open);
        }

        public void CloseDoor()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("door_close"))
            {
                return;
            }
            Debug.Log("문 닫기");
            animator.SetTrigger(Close);
        }
    }

}