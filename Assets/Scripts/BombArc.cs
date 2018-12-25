using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {

	protected float Animation;
	private Vector3 targetPos;

	void Update() {
		if (targetPos != null) {
			Animation += Time.deltaTime;

			Animation = Animation % 5f;

			transform.position = MathParabola.Parabola (transform.position, targetPos, 1, Animation / 5f);
		}
	}

	public void setTarget(Vector3 pos) {
		targetPos = pos;
	}
}
