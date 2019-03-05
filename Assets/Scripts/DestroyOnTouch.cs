using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// If this object touches anything its detroyed. As long as it isn't a cube shard, because that would be bad. This class was made for cube shards.
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
