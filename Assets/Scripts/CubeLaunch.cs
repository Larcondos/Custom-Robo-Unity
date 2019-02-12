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
		if (countdown > 0) {
			countdown--;

			axis.z += Input.GetAxis ("Horizontal");
			axis.z = Mathf.Clamp (axis.z, -45, 45);
			axis.x += Input.GetAxis ("Vertical");
			axis.x = Mathf.Clamp (axis.x, -45, 45);

			transform.localEulerAngles = new Vector3 (axis.x, transform.localEulerAngles.y, -axis.z);
		}
		if (countdown == 0 && !launched) {
			launched = true;

			StartCoroutine (launchIt());
		}


	
	}

	IEnumerator launchIt() {
		yield return new WaitForSeconds (0.1f);

		cube.GetComponent<Rigidbody> ().AddRelativeForce(Vector3.up * 5000);
		cube.GetComponent<Rigidbody> ().useGravity = true;
		cube.GetComponent<MeshRenderer> ().enabled = true;
		cube.transform.parent = null;
		cube.GetComponent<SpawnCube> ().enabled = true;
		StartCoroutine (destroyIt());
	}

	IEnumerator destroyIt() {
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}
}
