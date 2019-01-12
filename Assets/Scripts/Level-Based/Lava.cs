﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
		if (col.collider.gameObject.tag == "Player") {
			// Hurt player for 125
			col.collider.attachedRigidbody.AddForce(Vector3.up * 1000);
		}

		if (col.collider.gameObject.tag == "Spawn Cube") {
			// Destroy the cube
			// Hurt player for 125
		}
		//print (col.collider.gameObject.tag);
	}
}
