using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps both players on the screen at all time, and zooms and resizes appropriately.
public class CameraController : MonoBehaviour {

	// A list of targets's transforms to follow
	public List<Transform> targets;

	public Vector3 offset;
	public float smoothTime = 0.5f;

	public float minZoom = 40f;
	public float maxZoom = 10f;
	public float zoomLimit = 50f;

	private Vector3 velocity;
	private Camera cam;

	void Start() {
		cam = GetComponent<Camera> ();

		Application.targetFrameRate = 60;
	}

	// Late Update for camera movement to calcualte this post other objects movement.
	void LateUpdate() {

		if (targets.Count == 0)
			return;
		
		Move ();
		Zoom ();
		}

	void Move() {
		Vector3 centerPoint = getCenterPoint ();

		Vector3 newPosition = centerPoint + offset;

		transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref velocity, smoothTime); 
		transform.LookAt (getEncapsulatingBounds ().center);
	}


	void Zoom() {
		float newZoom = Mathf.Lerp (maxZoom, minZoom, getGreatestDistance () / zoomLimit);
		cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, newZoom, Time.deltaTime);
	}

	float getGreatestDistance() {
		return getEncapsulatingBounds ().size.x;
	}

	Bounds getEncapsulatingBounds() {
		var bounds = new Bounds (targets [0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++) {
			bounds.Encapsulate (targets [i].position);
		}
		return bounds;
	}

	Vector3 getCenterPoint() {
		if (targets.Count == 1) {
			return targets [0].position;
		}

		return getEncapsulatingBounds ().center;

	}


}
