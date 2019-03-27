using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {

	// Script for the parabola logic.
	private ParabolaController paraC;

	// Gameobjects for the particle systems.
	public GameObject ringParticle, towerParticle, exhaustParticle;

	// Used to get specific details from the systems, if needed.
	private ParticleSystem ringParts, towerParts;

	// Has this bomb exploded or not?
	private bool exploded;

	public int ATK; // Damage amount
	public float SPD; // Speed the bombs move at
	public float TIM; // Time the explosion will last for
	public int DWN; // How much knockback the bombs apply
	public float SIZ; // Size of explosion

	private float now; // Used in calculations for aiming the bomb in mid air (for particles)
	private float startLife; // Used in calculations for aiming the bomb in mid air (for particles)

	void Start () {
		
		paraC = GetComponent<ParabolaController> ();
		ringParts = ringParticle.GetComponent<ParticleSystem> ();
		towerParts = towerParticle.GetComponent<ParticleSystem> ();

		adjustStats ();
		startLife = Time.timeSinceLevelLoad;
	}

	void Update() {

		// Bomb rotation calculation.
		now = Time.timeSinceLevelLoad - startLife;
		print (now);
		// When the bomb has reached it's destination, explode.
		if (!paraC.Animation) {
			Explode ();
		}

		// Used to determine the rotation of the bomb. Not very great.
		if (exploded) {
			//blastRadius.radius += SIZ / 4;
			transform.rotation = Quaternion.identity;
		} else {
			transform.LookAt (-paraC.GetPositionAtTime (now / 2));
		}

	}

	// This function grabs various values from the particle systems and applies them to components of the bomb for visual, scaling, and damage effects.
	void adjustStats() {
		var rMain = ringParts.main;
		var tMain = towerParts.main;

		paraC.Speed = SPD;

		rMain.startSpeed = SIZ;
		tMain.startSpeed = SIZ * 10;


		rMain.startLifetime = 1;
		tMain.startLifetime = TIM;

		rMain.duration = 1;
		tMain.duration = TIM;
	}
		
	void OnTriggerEnter(Collider col) {
		// On Contact with an enemy or wall, explode. Also the bomb takes .2 seconds to arm so it doesn't blow up on yourself ;)
		if ((col.gameObject.tag == "Player" || col.gameObject.tag == "Wall" || col.gameObject.tag == "Destructible" || col.gameObject.tag == "Player2") && now > 0.2f) {
			Explode ();
		}
	}
		
	// Called when the bomb goes boom.
	void Explode() {

		// Only explode once!
		if (!exploded) {
			
			exploded = true;

			// Stop moving the bomb.
			paraC.Animation = false;

			// Activate all the particle effects, and give them stats.
			ringParticle.SetActive (true);
			towerParticle.SetActive (true);
			ringParticle.GetComponent<partCollide> ().applyStats (ATK, DWN);
			towerParticle.GetComponent<partCollide> ().applyStats (ATK, DWN);

			// Disable these particles, and the renderer on the bomb.
			exhaustParticle.SetActive (false);
			GetComponent<Renderer> ().enabled = false;
			StartCoroutine (disableParticles ());
			//GetComponent<Capsul> ().enabled = false;
		}
	}
		
	// Destroy the particle explosion after the time is done.
	IEnumerator disableParticles() {
		yield return new WaitForSeconds (TIM);
		Destroy (this.gameObject);
	}
		
	
}
