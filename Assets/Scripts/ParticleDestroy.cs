using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour {

	private ParticleSystem part;
	private float deathTimer;

	// Use this for initialization
	void Start () {
		part = GetComponent<ParticleSystem> ();
		deathTimer = part.main.duration;

		StartCoroutine (killMe ());
	}
	
	IEnumerator killMe() {
		yield return new WaitForSeconds (deathTimer);

		Destroy (this.gameObject);
	}
}
