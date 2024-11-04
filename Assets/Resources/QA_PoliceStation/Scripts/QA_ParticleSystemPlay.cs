using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class QA_ParticleSystemPlay : MonoBehaviour {
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float triggerRadius = 1.2f;
    private ParticleSystem particleFX;
    private SphereCollider trigger;
	// Use this for initialization
	void Start () {
        particleFX = gameObject.GetComponent<ParticleSystem>();

        trigger = gameObject.GetComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = triggerRadius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            particleFX.Play();
        }
    }
}
