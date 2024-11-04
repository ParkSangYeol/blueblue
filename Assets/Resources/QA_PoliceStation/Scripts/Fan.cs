using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    public Transform fanBlades;
    private Animation fanHeadAnim;

    public float fanSpeed = 10;
    public float fanHeadSpeed = 1;

    private string animName;

	// Use this for initialization
	void Start () {
        fanHeadAnim = gameObject.GetComponent<Animation>();
        animName = fanHeadAnim.clip.name;

    }
	
	// Update is called once per frame
	void Update () {

        fanBlades.Rotate(Vector3.forward * -fanSpeed * Time.deltaTime * 10);
        fanHeadAnim[animName].speed = fanHeadSpeed;
	}
}
