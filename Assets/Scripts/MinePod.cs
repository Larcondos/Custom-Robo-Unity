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

	// Use this for initialization
	void Start () {
		meshRen = GetComponent<MeshRenderer> ();
		mat = meshRen.material;
		StartCoroutine (Flash ());
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0.03f) {
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
		}
		else {
			// Turn on emission
			mat.EnableKeyword("_EMISSION");
			flashOn = !flashOn;
		}
		yield return new WaitForSeconds (timer);

		timer *= 0.9f;

		StartCoroutine (Flash ());
	}
}
