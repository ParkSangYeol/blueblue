using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Interaction {
    public class PhoneInteractive : MonoBehaviour, IInteractable
    {
        public GameObject blackSurface;
        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
        public void StartInteract()
        {
            blackSurface.SetActive(false);
        }

        public void StopInteract()
        {
           //none
        }

    } 
}
