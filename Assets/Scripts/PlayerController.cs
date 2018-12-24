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

	private Rigidbody rb;
	private float jumpHeight = 8f;
	private float runSpeed = 3f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
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

		if (Input.GetButton ("BombFire")) {
			Debug.Log ("Aiming Bomb");
		}

		if (Input.GetButtonUp ("BombFire")) {
			Debug.Log ("Bomb Launch");
		}

		if (Input.GetButtonDown ("ChargeAttack")) {
			Debug.Log ("Charge Attack Activated");
		}

		if (Input.GetButtonDown ("Pause")) {
			Debug.Log ("Game Paused");
		}

		Debug.Log (Input.GetAxis ("Vertical"));



		/* Debug Block for inital mappings
		for (int i = 0;i < 20; i++) {
			if(Input.GetKeyDown("joystick 1 button "+i)){
				print("joystick 1 button "+i);
			}
		}
		*/
	}

}
