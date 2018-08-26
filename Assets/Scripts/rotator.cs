using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {

	private float speed = 12;

	private Transform t;

	// Use this for initialization
	void Start () {
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {
		t.Rotate (new Vector3 (0, Time.deltaTime * speed, 0));
	}
}
