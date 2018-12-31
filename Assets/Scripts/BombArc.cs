using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {

	protected float Animation;
	private Vector3 targetPos = Vector3.zero;

	void Update() {
		if (targetPos != Vector3.zero) {
			GetComponent<ParabolaController> ().FollowParabola ();
		}
	}

	public void setTarget(Vector3 pos) {
		targetPos = pos;
	}
}
