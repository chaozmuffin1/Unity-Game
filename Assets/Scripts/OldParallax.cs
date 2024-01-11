using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {



	private float length;
	private float startpos;
	public float parallaxEffect;
	public GameObject cam;
	
	// Use this for initialization
	void Start () {
		// Sets the startpos variable equal to the current position of the object
		startpos = transform.position.x;
		// Gives size in x of the sprite (length of the sprite)
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		float temp = (cam.transform.position.x * (1 - parallaxEffect));
		// How far object has moved in world space
		float dist = (cam.transform.position.x * parallaxEffect);
		
		// Moves the Camera cam object
		transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.x);

		if (temp > startpos + length) startpos += length;
		else if (temp < startpos - length) startpos -= length;
	}
}
