using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps both players on the screen at all time, and zooms and resizes appropriately.
public class CameraController : MonoBehaviour {

	// A list of targets's transforms to follow
	public List<Transform> targets;

	// An offset and smoothness factor for moving the camera.
	public Vector3 offset;
	public float smoothTime = 0.5f;

	// How far in and out to zoom in on the players.
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

	// Move to the point between the targets.
	void Move() {
		Vector3 centerPoint = getCenterPoint ();

		Vector3 newPosition = centerPoint + offset;

		transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref velocity, smoothTime); 
		transform.LookAt (getEncapsulatingBounds ().center);
	}

	// Zoom in and out as needed.
	void Zoom() {
		float newZoom = Mathf.Lerp (maxZoom, minZoom, getGreatestDistance () / zoomLimit);
		cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, newZoom, Time.deltaTime);
	}
		
	float getGreatestDistance() {
		return getEncapsulatingBounds ().size.x;
	}

	Bounds getEncapsulatingBounds() {
		
		if (targets [0] == null) {
			if (GameObject.FindGameObjectWithTag ("Respawn").transform != null) {
				targets [0] = GameObject.FindGameObjectWithTag ("Respawn").transform;
			}
			 else if (GameObject.FindGameObjectWithTag ("Enemy").transform != null) {
				targets [0] = GameObject.FindGameObjectWithTag ("Enemy").transform;
			}
			return new Bounds (Vector3.zero, Vector3.zero);
		}
		if (targets [1] == null) {
			if (GameObject.FindGameObjectWithTag ("Respawn").transform != null) {
				targets [1] = GameObject.FindGameObjectWithTag ("Respawn").transform;
			}
			else if (GameObject.FindGameObjectWithTag ("Player").transform != null) {
				targets [1] = GameObject.FindGameObjectWithTag ("Player").transform;
			}
			return new Bounds (Vector3.zero, Vector3.zero);
		
		} else {
			var bounds = new Bounds (targets [0].position, Vector3.zero);
			for (int i = 0; i < targets.Count; i++) {
				bounds.Encapsulate (targets [i].position);
			}
			return bounds;
		}
	}

	// Gets the midpoint of the bounds.
	Vector3 getCenterPoint() {
		if (targets.Count == 1) {
			return targets [0].position;
		}

		return getEncapsulatingBounds ().center;

	}


}
