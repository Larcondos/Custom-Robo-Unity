using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePod : MonoBehaviour {

	private MeshRenderer meshRen;
	private Material mat;

	// Is the flash on or off?
	private bool flashOn = false;

	// How long to flash?
	private float timer = 1.5f;

	// The Light object on this, to give a more "flashy" effect.
	private Light redLight;


	// Use this for initialization
	void Start () {
		meshRen = GetComponent<MeshRenderer> ();
		mat = meshRen.material;
		redLight = GetComponent<Light> ();
		StartCoroutine (Flash ());
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0.03f) {
			explode ();
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag ("Player") || (col.gameObject.CompareTag ("Enemy"))) {
			explode ();
		}
	}

	void explode() {
		Destroy (this.gameObject);
	}

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

		timer *= 0.9f;

		StartCoroutine (Flash ());
	}
}
