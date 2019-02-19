using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

	private const int dmg = 125;

	void OnCollisionEnter(Collision col) {
		if (col.collider.gameObject.tag == "Player") {

			// Hurt them, they shouldn't step on lava.
			col.gameObject.GetComponent<PlayerStats> ().doDamage (dmg);

			// Blast em up, teach them to come back here...
			col.collider.attachedRigidbody.AddForce(Vector3.up * 1000);
		}

		if (col.collider.gameObject.tag == "Spawn Cube") {
			// TODO: Destroy cube, immediately do damage to player, put them in knockdown.
		}

	}
}
