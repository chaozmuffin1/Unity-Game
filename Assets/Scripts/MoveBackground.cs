using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {
	private float pos;

	// Use this for initialization
	void Start () {
		pos = transform.position.x;
	}
	
	// Update is called once per frame
	void Update()
	{	
		pos+= .05f;
		transform.position = new Vector3(pos, transform.position.y, transform.position.z);
	}
}
