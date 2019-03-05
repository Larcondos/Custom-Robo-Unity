using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePod : MonoBehaviour {

	// This is the character that spawned this object.
	private GameObject parent;

	// Materials for the MeshRenderer and color material.
	private MeshRenderer meshRen;
	private Material mat;

	// Is this exploded or not?
	private bool exploded;

	// Particle Game Objects
	public GameObject ringParticle, towerParticle;

	// Particle Systems, associated with those objects. Used to scale stats on them.
	private ParticleSystem ringParts, towerParts;

	// Is the flash on or off?
	private bool flashOn = false;

	// How long to flash?
	public float timer;

	// The Light object on this, to give a more "flashy" effect.
	private Light redLight;

	// Stats 
	public int ATK; // Damage amount
	public float SPD; // Speed the bombs move at
	public float TIM; // Time the explosion will last for
	public float RNG; // How far these pods will go
	public float SIZ; // Size of explosion
	public int DWN; // Knockback applied


	// Use this for initialization
	void Start () {
		meshRen = GetComponent<MeshRenderer> ();
		mat = meshRen.material;
		redLight = GetComponent<Light> ();
		StartCoroutine (Flash ());

		ringParts = ringParticle.GetComponent<ParticleSystem> ();
		towerParts = towerParticle.GetComponent<ParticleSystem> ();

		adjustStats ();

		// I want the mine to ignore the collision from their parent who spawned them...
		Physics.IgnoreCollision (GetComponent<MeshCollider>(), parent.GetComponent<CapsuleCollider>());
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0.03f) {
			explode ();
		}
	}

	void OnCollisionEnter(Collision col) {
		if ((col.gameObject.CompareTag ("Enemy"))) {
			explode ();
		}
	}

	void explode() {
		// Only explode once!
		if (!exploded) {

			exploded = true;

			// Reset the mines rotation, as they can move a little bit.
			transform.rotation = Quaternion.identity;

			// Activate and deparent appropriate particle effects, and fix scalings and apply stats to the particles. 
			ringParticle.transform.SetParent (null);
			towerParticle.transform.SetParent (null);

			ringParticle.transform.localScale = Vector3.one;
			towerParticle.transform.localScale = Vector3.one;

			ringParticle.SetActive (true);
			towerParticle.SetActive (true);
			ringParticle.GetComponent<partCollide> ().applyStats (ATK, DWN);
			towerParticle.GetComponent<partCollide> ().applyStats (ATK, DWN);

			// The mine goes away.
			Destroy (this.gameObject);

		}
	}

	public void assignParent(GameObject g) {
		parent = g;
	}

	// Stat adjustment for scalings of particles.
	void adjustStats() {
		var rMain = ringParts.main;
		var tMain = towerParts.main;

		rMain.startSpeed = SIZ;
		tMain.startSpeed = SIZ * 10;


		rMain.startLifetime = TIM;
		tMain.startLifetime = TIM;

		rMain.duration = TIM;
		tMain.duration = TIM;
	}

	// Turns the lighting effect off and on. Also turns the mine red because I cheated.
	IEnumerator Flash() {
		if (flashOn) {
			// Turn off emission
			mat.DisableKeyword("_EMISSION");
			flashOn = !flashOn;
			redLight.enabled = false;
		}
		else {
			// Turn on emission
			mat.EnableKeyword("_EMISSION");
			flashOn = !flashOn;
			redLight.enabled = true;
		}
		yield return new WaitForSeconds (timer);

		// Decrease the timer, and start again.
		timer *= 0.9f;

		StartCoroutine (Flash ());
	}
}
