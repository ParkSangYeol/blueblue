using System;
using System.Collections;
using System.Collections.Generic;
using System.Interaction;
using UnityEngine;

public class SampleInteractable : MonoBehaviour, IInteractable
{
    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void StartInteract()
    {
        Debug.Log("Start Interact!");
    }

    public void StopInteract()
    {
        Debug.Log("Stop Interact!");
    }
}
