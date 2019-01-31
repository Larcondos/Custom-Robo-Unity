using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name != "cubeShard")
			Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name != "cubeShard")
			Destroy (this.gameObject);
	}
}
