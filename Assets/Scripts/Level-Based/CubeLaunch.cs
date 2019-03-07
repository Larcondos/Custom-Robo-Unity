using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLaunch : MonoBehaviour {


	// The gameobject of the cube to launch.
	public GameObject cube;

	// The focal axis of the launcher.
	private Vector3 axis;

	// Has the cube been launched yet?
	private bool launched = false;

	// A specific number used to determine how long until the official "Start" of the match.
	// TODO: Find a real value for this because right now it's arbitrary. Maybe turn this into a coRoutine?
	private int countdown = 200;

	// The camera game object. Used to assign the focus on them.
	private GameObject cam;

	// Camera Controller script on the camera
	private CameraController cameraCont;

	void Start() {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraCont = cam.GetComponent<CameraController> ();
		//if (cube != null) {
			cameraCont.targets.Add (cube.transform);
		//}
	}

	// Update is called once per frame
	void Update () {
		// While the countdown isn't over, you can move it!
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

			cameraCont.targets.Clear ();

			// Un-parent the cube, and launch it off on it's own. They grow up so fast.
			cube.transform.parent = null;
			cube.GetComponent<Rigidbody> ().AddRelativeForce(Vector3.up * 5000);
			cube.GetComponent<Rigidbody> ().useGravity = true;
			cube.GetComponent<SpawnCube> ().enabled = true;
			StartCoroutine (destroyIt());
		}
	}

	// Destroy the launcher after the cubes are sent flying.
	IEnumerator destroyIt() {
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}
}
