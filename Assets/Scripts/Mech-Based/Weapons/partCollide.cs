using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partCollide : MonoBehaviour {

	void Start() {
//		blastRadius = GetComponent<SphereCollider> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.CompareTag("Player") || col.CompareTag("Enemy")) {
			print ("I did something!");
			print (GetComponentInParent<BombArc> ().ATK);
			col.gameObject.GetComponent<PlayerStats>().doDamage(GetComponentInParent<BombArc>().ATK);
		}
	}
}
