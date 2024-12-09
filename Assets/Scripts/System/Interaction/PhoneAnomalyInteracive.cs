using Anomaly.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Interaction
{
    public class PhoneAnomalyInteracive : MonoBehaviour, IInteractable
    {
        public GameObject blackSurface;
        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
        public void StartInteract()
        {
            blackSurface.SetActive(false);
            transform.GetComponentInChildren<PhoneAddressDelete>().CallActivePhenomenon();
        }

        public void StopInteract()
        {
            //none
        }
    }
}
