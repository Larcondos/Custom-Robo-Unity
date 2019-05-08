using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPath : MonoBehaviour {

	// The transform for the target location to go to.
	public GameObject target;

	// The gameobject that spawned this bullet. Used to the bullet doesn't hurt it's parent.
	private GameObject parent;

	// Rigibody for the bullet, used in angling and collision detection.
	private Rigidbody rb;

	// Particles on Collision
	public GameObject explodeParticle, impactParticle;

	private float lifeSoFar;

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
		//target = GameObject.FindGameObjectWithTag ("Enemy").transform;


		// Prevents the bullet from spawning inside player. Will be changed later to come out of gun tip.
		// TODO: Change to come out of gun tip.

	}

	// Fixed Update so that the physics cycles only occur as often as needed, and allow for post calculations of target movement.
	void FixedUpdate () {
		lifeSoFar += 1;
		// Always move forward.
		rb.velocity = transform.forward * SPD;

		if (target == null) {

			if (GameObject.FindGameObjectWithTag ("Respawn") != null) {
				target = GameObject.FindGameObjectWithTag ("Respawn");
				transform.LookAt (target.transform);
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, 0.8f);
			}

			if (GameObject.FindGameObjectWithTag ("Enemy") != null) {
				target = GameObject.FindGameObjectWithTag ("Enemy");
				transform.LookAt (target.transform);
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, 0.8f);
			}

		}

		// If you have a target, rotate towards them. Otherwise you just keep going forward.
		if (target != null) {

			var targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);

			rb.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, HMG));
		}
	}

	// Used to destroy the bullet after it's life cycle has completed, if it hasn't already died.
	IEnumerator delayedDestroy() {
		yield return new WaitForSeconds (RNG);

		Destroy (this.gameObject);
	}

	// The collision code, called when the bullet collides with stuff.
	void OnTriggerEnter(Collider col) {
		if (col.CompareTag ("Player") || col.CompareTag ("Player2")) {
			col.gameObject.GetComponent<PlayerStats> ().doDamage (ATK, DWN);
			destroyMe ();
		} else if (col.CompareTag ("Destructible")) {
			col.gameObject.GetComponent<DestructibleCube> ().doDamage (ATK);
			destroyMe ();
		} else if (col.CompareTag ("Wall")) {
			destroyMe ();
		} else if (lifeSoFar > 10) {
			// if we hit something within 20 frames, its gonna blow.
			//destroyMe ();
		}
	}

	// Anything else we hit will destroy the bullets. Tragic, I know.
	void OnCollisionEnter(Collision col) {
		destroyMe ();

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

	// Activate the bullets particles that it needs, and destroy it.
	public void destroyMe() {
		explodeParticle.transform.SetParent (null);
		impactParticle.transform.SetParent (null);

		explodeParticle.SetActive (true);
		impactParticle.SetActive (true);

		Destroy (this.gameObject);
	}

	public void setTarget2(GameObject inTarget, GameObject inParent) {
		if (inTarget != null) {
			target = inTarget;
			transform.LookAt (target.transform);
		}
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
			transform.LookAt (target.transform);
		}
		parent = inParent;
		Physics.IgnoreCollision (GetComponent<CapsuleCollider>(), parent.GetComponent<CapsuleCollider>());


	}

	public void setTarget(GameObject inTarget, GameObject inParent, GameObject inVrRightHandAim) {
		if (inTarget != null) {
			target = inTarget;
			if (inVrRightHandAim != null) {
				transform.LookAt (inVrRightHandAim.transform);
			} else {
				transform.LookAt (target.transform);
			}
		}
		parent = inParent;
		Physics.IgnoreCollision (GetComponent<CapsuleCollider>(), parent.GetComponent<CapsuleCollider>());
	}

	public GameObject getParent() {
		return parent;
	}

}
