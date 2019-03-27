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

	// A multiplier for turning speed.
	private const int turnSpeed = 50;

	// The camera game object. Used to assign the focus on them.
	private GameObject cam;

	// Camera Controller script on the camera
	private CameraController cameraCont;

	// For the second player, they get seperate controls. Sorta.
	private bool otherController;


	void Start() {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraCont = cam.GetComponent<CameraController> ();
		cameraCont.targets.Add (cube.transform);

		if (this.gameObject.name == "P2 Launcher")
			otherController = true;

		// Begin the countdown as soon as the screen renders!
		StartCoroutine (countdown ());
	}

	// Update is called once per frame
	void Update () {
		// While the countdown isn't over, you can move it!
		if (!launched && !otherController) {

			// Input from player to move the launcher direction, clamped at certain angles.
			axis.z += (Input.GetAxis ("Horizontal") * Time.deltaTime * turnSpeed);
			axis.z = Mathf.Clamp (axis.z, -45, 45);
			axis.x += (Input.GetAxis ("Vertical") * Time.deltaTime * turnSpeed);
			axis.x = Mathf.Clamp (axis.x, -45, 45);

			// Rotate said angles.
			transform.localEulerAngles = new Vector3 (axis.x, transform.localEulerAngles.y, -axis.z);
		}

		// Modified controls
		if (!launched && otherController) {

			// Input from player to move the launcher direction, clamped at certain angles.
			axis.z += (Input.GetAxis ("Horizontal2") * Time.deltaTime * turnSpeed);
			axis.z = Mathf.Clamp (axis.z, -45, 45);
			axis.x += (Input.GetAxis ("Vertical2") * Time.deltaTime * turnSpeed);
			axis.x = Mathf.Clamp (axis.x, -45, 45);

			// Rotate said angles.
			transform.localEulerAngles = new Vector3 (axis.x, transform.localEulerAngles.y, -axis.z);
		}

		/*// Launch the cube.
		if (countdown == 0 && !launched) {
			launched = true;

			cameraCont.targets.Clear ();

			// Un-parent the cube, and launch it off on it's own. They grow up so fast.
			cube.transform.parent = null;
			cube.GetComponent<Rigidbody> ().AddRelativeForce(Vector3.up * 5000);
			cube.GetComponent<Rigidbody> ().useGravity = true;
			cube.GetComponent<SpawnCube> ().enabled = true;
			StartCoroutine (destroyIt());
		}*/
	}

	IEnumerator countdown() {

		// Launch timer based on song.
		yield return new WaitForSeconds (6f);

		// Launches cube, and all is well in the world.
		launched = true;

		cameraCont.targets.Clear ();

		// Un-parent the cube, and launch it off on it's own. They grow up so fast.
		cube.transform.parent = null;
		cube.GetComponent<Rigidbody> ().AddRelativeForce(Vector3.up * 5000);
		cube.GetComponent<Rigidbody> ().useGravity = true;
		cube.GetComponent<SpawnCube> ().enabled = true;
		StartCoroutine (destroyIt());

	}

	// Destroy the launcher after the cubes are sent flying.
	IEnumerator destroyIt() {
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}
}
