using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour {

	// This is a boolean to determine which way the walls are moving.
	private bool movingLeft;

	// A speed multiplier at which the walls will move.
	private float moveSpeed = 3f;
	
	// Update is called once per frame
	void Update () {

		// If I'm moving left, go left! If not, go right.
		if (movingLeft) {
			goLeft ();
		} else
			goRight ();
	}

	// Translates to the left at a fixed rate. If it goes past a certain boundary, it switches direction.
	void goLeft() {
		transform.Translate (Vector3.left * Time.deltaTime * moveSpeed);
		if (transform.position.x < -5)
			movingLeft = false;
	}

	// Translates to the right at a fixed rate. If it goes past a certain boundary, it switches direction.
	void goRight() {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		if (transform.position.x > 5)
			movingLeft = true;
	}



}
