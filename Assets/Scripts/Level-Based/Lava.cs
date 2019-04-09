using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

	// Lava will always do the same damage, and always knockdown a player instantly.
	private const int DMG = 125;
	private const int DWN = 150;

	void OnCollisionEnter(Collision col) {
		if (col.collider.gameObject.tag == "Player") {

			// Hurt them, they shouldn't step on lava.
			col.gameObject.GetComponent<PlayerStats> ().doDamage (DMG, DWN);

			// Blast em up, teach them to come back here...
			col.collider.attachedRigidbody.AddForce(Vector3.up * 1000);
		}

		if (col.collider.gameObject.tag == "Spawn Cube") {
			// TODO2: Destroy cube, immediately do damage to player, put them in knockdown.
			col.gameObject.GetComponent<SpawnCube>().openEarly(DMG , DWN);
		}

	}
}
