using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	#region Button Mapping
	/*

	Control Stick moves character in World Space
	A Button to Jump or Air Dash
	B Button is to Gun Attack
	L Button is for Pod Attack
	R Button is for Bomb Attack
	X Button is for Charge Attack

	*/
	#endregion Button Mapping
	// The enemy game object.
	public GameObject enemy;

	// A marker for the bomb's intended location.
	public GameObject bombMarker;

	// The current instanciation of the bomb marker.
	private GameObject bombMarkerInstance;

	// The game object for the bomb weapon.
	public GameObject bomb;

	// The path for the bomb to take.
	public GameObject parabola;

	// An access variable for the player's rigidbody.
	private Rigidbody rb;

	// How high this mech can jump.
	private float jumpHeight = 8f;

	// How quickly this mech can run.
	private float runSpeed = 3f;

	// Is a bomb being aimed right now? (Is R button down?)
	private bool aimingBomb = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
		getInput ();



		/* Debug Block for inital mappings
		for (int i = 0;i < 20; i++) {
			if(Input.GetKeyDown("joystick 1 button "+i)){
				print("joystick 1 button "+i);
			}
		}
		*/
	}


	void getInput() {

		if (!aimingBomb) {
			if (Input.GetAxis ("Horizontal") != 0)
				rb.AddForce (Input.GetAxis ("Horizontal") * Vector3.right * runSpeed, ForceMode.Force);

			if (Input.GetAxis ("Vertical") != 0)
				rb.AddForce (Input.GetAxis ("Vertical") * Vector3.forward * runSpeed, ForceMode.Force);

			if (Input.GetButtonDown ("Jump")) {
				rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);
				Debug.Log ("Jumpin'");
			}

			if (Input.GetButtonDown ("GunFire")) {
				Debug.Log ("Main Gun Fired");
			}

			if (Input.GetButtonDown ("PodFire")) {
				Debug.Log ("Pod Launched");
			}
			

			if (Input.GetButtonDown ("ChargeAttack")) {
				Debug.Log ("Charge Attack Activated");
			}
		}

		if (Input.GetButton ("BombFire")) {
			aimBomb ();
		}

		if (Input.GetButtonUp ("BombFire")) {
			fireBomb ();
		}

		if (Input.GetButtonDown ("Pause")) {
			Debug.Log ("Game Paused");
		}
	}

	// This script is used to place the location you wish your 'bomb' weapon to drop.
	// It also creates and manages the parabola for the bomb to follow.
	void aimBomb() {
		// On the first frame where this is called, it will instantiate a marker for the bomb's target area.
		if (aimingBomb != true)
			bombMarkerInstance = Instantiate (bombMarker, enemy.transform.position + (Vector3.down * .5f), Quaternion.identity);

		// The bomb is now being aimed, so no more instances will spawn.
		aimingBomb = true;

		// Detects control stick movement to place the bomb marker.
		if (Input.GetAxis ("Horizontal") != 0)
			bombMarkerInstance.transform.Translate (Input.GetAxis ("Horizontal") * Vector3.right * .1f);
		if (Input.GetAxis ("Vertical") != 0)
			bombMarkerInstance.transform.Translate (Input.GetAxis ("Vertical") * Vector3.forward * .1f);
	}

	void fireBomb() {
		// You are no longer aiming the bomb, so reset this variable.
		aimingBomb = false;

		// Create a new parabola to make an arc.
		var para = Instantiate (parabola, Vector3.zero, Quaternion.identity);

		// Grab the children transforms to make your main points.
		Transform[] paraRoots = para.GetComponentsInChildren<Transform> ();

		// Sets the start location.
		paraRoots [3].position = transform.position;

		// Sets the target location.
		paraRoots [1].position = bombMarkerInstance.transform.position;

		// A middle point, with a height factor added in at the end.
		paraRoots [2].position = ((transform.position + bombMarkerInstance.transform.position) * 0.5f) + (2 * Vector3.up);

		// Spawn the bomb, and assign it the path of the parabola we made.
		var bomba = Instantiate (bomb, transform.position, Quaternion.identity);
		bomba.GetComponent<ParabolaController> ().ParabolaRoot = para;
		Destroy (bombMarkerInstance);
	}
}
