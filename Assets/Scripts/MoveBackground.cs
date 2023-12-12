using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {



	private float length;
	private float startPos;
	public float parallaxEffect;
	public GameObject cam;
	
	// Use this for initialization
	void Start () {
		// Sets the startpos variable equal to the current position of the object
		startPos = transform.position.x;
		// Gives size in x of the sprite (length of the sprite)
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = (cam.transform.position.x * parallaxEffect);

		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.x);
	}
}
