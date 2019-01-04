using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {
	// I need to detect when I'm done moving and explode!


	private ParabolaController paraC;
	public GameObject ringParticle, towerParticle;
	private ParticleSystem ringParts, towerParts;
	private bool exploded;

	public float ATK; // Damage amount
	public float SPD; // Speed the bombs move at
	public float TIM; // Time the explosion will last for
	public float DWN; // How much knockback the bombs apply
	public float SIZ; // Size of explosion

	//TODO: Should Instanciate these particles rather than children.

	void Start () {
		paraC = GetComponent<ParabolaController> ();
		ringParts = ringParticle.GetComponent<ParticleSystem> ();
		towerParts = towerParticle.GetComponent<ParticleSystem> ();

		adjustStats ();
	}

	void Update() {

		// When the bomb has reached it's destination, explode.
		if (!paraC.Animation) {
			Explode ();
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
			GetComponent<Renderer> ().enabled = false;
			StartCoroutine (disableParticles ());
		}
	}
		

	IEnumerator disableParticles() {
		yield return new WaitForSeconds (TIM);

		Destroy (this.gameObject);
	}

}
