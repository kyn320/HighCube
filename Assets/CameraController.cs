using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 margin;
    public Vector3 movePos;
    public float lerpTime = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, movePos + margin, Time.deltaTime * lerpTime);
	}
}
