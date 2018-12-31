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
			Debug.Log ("Aiming Bomb");
			if (aimingBomb != true)
				bombMarkerInstance = Instantiate (bombMarker, enemy.transform.position + (Vector3.down * .5f), Quaternion.identity);
			
			aimingBomb = true;

			if (Input.GetAxis ("Horizontal") != 0)
				bombMarkerInstance.transform.Translate (Input.GetAxis ("Horizontal") * Vector3.right * .1f);
			if (Input.GetAxis ("Vertical") != 0)
				bombMarkerInstance.transform.Translate (Input.GetAxis ("Vertical") * Vector3.forward * .1f);

		}

		if (Input.GetButtonUp ("BombFire")) {
			Debug.Log ("Bomb Launch");
			aimingBomb = false;

			var bomba = Instantiate (bomb, transform.position, Quaternion.identity);
			bomba.GetComponent<BombArc> ().setTarget (bombMarkerInstance.transform.position);
			Destroy (bombMarkerInstance);
		}

		if (Input.GetButtonDown ("Pause")) {
			Debug.Log ("Game Paused");
		}
	}
}
