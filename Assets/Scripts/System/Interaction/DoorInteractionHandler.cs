using System.Collections;
using System.Collections.Generic;
using System.Interaction;
using Anomaly;
using UnityEngine;

namespace System.Interaction
{
    public class DoorInteractionHandler : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private DoorController controller;
    
        public void StartInteract()
        {
            // 문 열기
            controller.OpenDoor();
        }

        public void StopInteract()
        {
            // NONE
        }
    }
}
