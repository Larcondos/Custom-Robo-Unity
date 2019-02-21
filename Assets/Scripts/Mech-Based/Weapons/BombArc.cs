using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {

	private ParabolaController paraC;
	public GameObject ringParticle, towerParticle, exhaustParticle;
	private ParticleSystem ringParts, towerParts;
	private bool exploded;

	public int ATK; // Damage amount
	public float SPD; // Speed the bombs move at
	public float TIM; // Time the explosion will last for
	public int DWN; // How much knockback the bombs apply
	public float SIZ; // Size of explosion

	private float now; // Used in calculations for aiming the bomb in mid air (for particles)
	private float startLife; // Used in calculations for aiming the bomb in mid air (for particles)

	//TODO: Should Instanciate these particles rather than children.

	void Start () {
		paraC = GetComponent<ParabolaController> ();
		ringParts = ringParticle.GetComponent<ParticleSystem> ();
		towerParts = towerParticle.GetComponent<ParticleSystem> ();

		adjustStats ();
		startLife = Time.timeSinceLevelLoad;
	}

	void Update() {

		now = Time.timeSinceLevelLoad - startLife;

		// When the bomb has reached it's destination, explode.
		if (!paraC.Animation) {
			Explode ();
		}
		if (exploded) {
			//blastRadius.radius += SIZ / 4;
			transform.rotation = Quaternion.identity;
		} else {
			transform.LookAt (-paraC.GetPositionAtTime (now / 3));
		}

	}

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
		// On Contact with an enemy or wall, explode.
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Wall" || col.gameObject.tag == "Destructible") {
			Explode ();
		}
	}
		
	void Explode() {
		if (!exploded) {
			exploded = true;
			paraC.Animation = false;

			ringParticle.SetActive (true);
			towerParticle.SetActive (true);
			exhaustParticle.SetActive (false);
			GetComponent<Renderer> ().enabled = false;
			StartCoroutine (disableParticles ());
			//GetComponent<Capsul> ().enabled = false;
		}
	}
		

	IEnumerator disableParticles() {
		yield return new WaitForSeconds (TIM);
		// TODO: Fix the particle angle on destruction...
		Destroy (this.gameObject);
	}
		
	
}
