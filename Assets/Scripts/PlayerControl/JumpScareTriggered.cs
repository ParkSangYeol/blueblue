using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class JumpScareTriggered : MonoBehaviour
    {
        public bool isTriggered = false;
        public GameObject cameraRotated;
        private PlayerController p;
        private RotateCamera r;

        private void Start()
        {
            r = cameraRotated.GetComponent<RotateCamera>();
            p = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (isTriggered)
            {
                WhenJumpScareTriggered();
            }
        }

        public void WhenJumpScareTriggered()
        {
            r.WhenJumpScareTriggered();
            p.WhenJumpScareTriggered();
        }
    }
}
