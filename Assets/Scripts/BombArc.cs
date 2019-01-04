using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {
	// I need to detect when I'm done moving and explode!


	private ParabolaController paraC;
	public GameObject ringParticle;
	public GameObject towerParticle;
	private bool exploded;

	void Start () {
		paraC = GetComponent<ParabolaController> ();
	}

	void Update() {

		// When the bomb has reached it's destination, explode.
		if (!paraC.Animation) {
			Explode ();
		}
	}
		
	void OnTriggerEnter(Collider col) {
		// On Contact with an enemy or wall, explode.
		if (col.tag == "Enemy" || col.tag == "Wall") {
			Explode ();
		}
	}

	void Explode() {
		if (!exploded) {
			exploded = true;
			print ("BOOM!");
			// create particle effects
			//time variable for bomb explosion will affect how many seconds the 2nd aprticel system works
			ringParticle.SetActive (true);
			towerParticle.SetActive (true);
			GetComponent<Renderer> ().enabled = false;
			StartCoroutine (disableParticles ());
		}
	}

	IEnumerator disableParticles() {
		yield return new WaitForSeconds (1);
		Destroy (this.gameObject);
	}

}
