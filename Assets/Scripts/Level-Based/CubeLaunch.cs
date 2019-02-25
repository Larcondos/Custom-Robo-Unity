using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLaunch : MonoBehaviour {

	public GameObject cube;
	private Transform center;
	private Vector3 axis;
	private Vector3 desiredPosition;
	public float radius = 2.0f;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 200.0f;

	// The cube the launcher will shoot.
	//public GameObject cube;

	// Has the cube been launched yet?
	private bool launched = false;

	// A specific number used to determine how long until the official "Start" of the match.
	private int countdown  = 200;

	// Use this for initialization
	void Start () {
		//cube = GameObject.FindWithTag("Cube");
	}
	
	// Update is called once per frame
	void Update () {
		// Calls a countdown to occur.
		if (countdown > 0) {
			countdown--;

			// Input from player to move the launcher direction, clamped at certain angles.
			axis.z += Input.GetAxis ("Horizontal");
			axis.z = Mathf.Clamp (axis.z, -45, 45);
			axis.x += Input.GetAxis ("Vertical");
			axis.x = Mathf.Clamp (axis.x, -45, 45);

			// Rotate said angles.
			transform.localEulerAngles = new Vector3 (axis.x, transform.localEulerAngles.y, -axis.z);
		}

		// Launch the cube.
		if (countdown == 0 && !launched) {
			launched = true;

			// Un-parent the cube, and launch it off on it's own. They grow up so fast.
			cube.transform.parent = null;
			cube.GetComponent<Rigidbody> ().AddRelativeForce(Vector3.up * 5000);
			cube.GetComponent<Rigidbody> ().useGravity = true;
			cube.GetComponent<SpawnCube> ().enabled = true;
			StartCoroutine (destroyIt());
		}
	}

	IEnumerator destroyIt() {
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}
}
