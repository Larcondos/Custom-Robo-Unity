using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partCollide : MonoBehaviour {

	private SphereCollider blastRadius;

	void Start() {
//		blastRadius = GetComponent<SphereCollider> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			// Do Damage and stuff!
			print ("Player hit by bomb!");
		}
	}
}
