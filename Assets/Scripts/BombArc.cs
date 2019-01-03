using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {
	// I need to detect when I'm done moving and explode!


	private ParabolaController paraC;

	void Start () {
		paraC = GetComponent<ParabolaController> ();
	}

	void Update() {

		// When the bomb has reached it's destination, explode.
		if (!paraC.Animation) {
			Explode ();
		}
	}
		
	void OnTriggerEnter(Collider col) {
		// On Contact with an enemy or wall, explode.
		if (col.tag == "Enemy" || col.tag == "Wall") {
			Explode ();
		}
	}

	void Explode() {
		print ("BOOM!");
		// create particle effects
		//time variable for bomb explosion will affect how many seconds the 2nd aprticel system works
	}

}
