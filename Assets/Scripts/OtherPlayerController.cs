using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerController : MonoBehaviour {

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
	private float runSpeed = 8f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);
			print ("jumpy");
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.AddForce (Vector3.left* runSpeed, ForceMode.Force);
			print ("run");
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb.AddForce (Vector3.right* runSpeed, ForceMode.Force);
			print ("run");
		}
	}
		




}
