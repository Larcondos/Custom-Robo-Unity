using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPath : MonoBehaviour {

	// The transform for the target location to go to.
	private Transform target;

	// Rigibody for the bullet, used in angling and collision detection.
	private Rigidbody rb;

	// Stats for the bullets. All public for easy play testing.
	public int ATK; // Damage amount
	public float SPD; // Speed the bullets move at
	public float HMG; // How homing the bullets are
	public int DWN; // How much knockback the bullets apply
	public float RLD; // Reload Time before next shot.
	public float RNG; // The range of the bullets before disappearing.

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		// The frame that a bullet is born, it should also begin counting its life...
		StartCoroutine (delayedDestroy ());

		// The target should be the enemy (This will change later), and the bullet will spawn looking at the target so it is sent in the right direction.
		target = GameObject.FindGameObjectWithTag ("Enemy").transform;
		transform.LookAt (target);

		// Prevents the bullet from spawning inside player. Will be changed later to come out of gun tip.
		// TODO: Change to come out of gun tip.
		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, 0.8f);
	}

	// Fixed Update so that the physics cycles only occur as often as needed, and allow for post calculations of target movement.
	void FixedUpdate () {
		rb.velocity = transform.forward * SPD;

		var targetRotation = Quaternion.LookRotation(target.position - transform.position);

		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, HMG));
	}

	// Used to destroy the bullet after it's life cycle has completed, if it hasn't already died.
	IEnumerator delayedDestroy() {
		yield return new WaitForSeconds (RNG);

		Destroy (this.gameObject);
	}

	// The collision code, called when the bullet collides with stuff.
	void OnTriggerEnter(Collider col) {
		if (col.CompareTag ("Enemy")) {
			col.gameObject.GetComponent<PlayerStats> ().doDamage (ATK);
			Destroy (this.gameObject);
		} else if (col.CompareTag ("Destructible")) {
			col.gameObject.GetComponent<DestructibleCube> ().doDamage (ATK);
			Destroy (this.gameObject);
		} else if (!col.CompareTag("Bomb Particle") || !col.CompareTag("Bomb")) {
			// Nothing happens on these tags, this is the ignore zone. Code is being weird.

		} else {
			// If we hit anything else, the bullet gets destroyed.
			Destroy (this.gameObject);
		}

	}

	private void doDamage(GameObject g) {
		g.GetComponent<PlayerStats> ().doDamage (ATK);
	}

	// This function is called by anything that needs to know how long it takes to reload this bullet type.
	public float getReload() {
		return RLD;
	}

	public int getAttack() {
		return ATK;
	}

	public int getDown() {
		return DWN;
	}

	void OnDestroy() {
		
	}

}
