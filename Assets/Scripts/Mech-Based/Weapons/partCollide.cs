using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partCollide : MonoBehaviour {

	// Particles should only do damage once. Once they do damage, deactivate it so that that person doesn't get hurt again.
	// TODO: Make it so it remembers WHO it hit, so it can hit again...
	private bool didDamage = false;

	// Inherited Dmg and knockback values.
	private int ATK, DWN;

	void OnParticleCollision(GameObject col) {
		if ((col.CompareTag("Player") || col.CompareTag("Enemy")) && !didDamage) {
			didDamage = true;
			col.gameObject.GetComponent<PlayerStats>().doDamage(ATK, DWN);
		}
	}

	void OnParticleTrigger() {

	}

	public void applyStats(int a, int d) {
		ATK = a;
		DWN = d;
	}



}
