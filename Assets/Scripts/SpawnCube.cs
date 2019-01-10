﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour {

	// This is a random number that determines how long until a cube will open up. It's random, not fair!
	private int randNum; 

	// Rigidbody component attached to this object.
	private Rigidbody rb;

	// How fast is the cube moving? Used to determine when to begin the countdown for real.
	private float speed;

	private bool sleeping;


	// Use this for initialization
	void Start () {
		randNum = Random.Range (1, 6);
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (!sleeping) {
			speed = rb.velocity.magnitude;
			if (speed < 0.01 && speed != 0) {
				rb.velocity = new Vector3 (0, 0, 0);
				StartCoroutine (tickDown ());
				sleeping = true;

				print ("Sleep!");

			} 
		}
		print (randNum);
	}
	IEnumerator tickDown() {
		yield return new WaitForSeconds (1);

		randNum--;

		if (randNum > 0) {
			StartCoroutine (tickDown ());
		} else {
			Destroy (this.gameObject);
		}
	}
}
