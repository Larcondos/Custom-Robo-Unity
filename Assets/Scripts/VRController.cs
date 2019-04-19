using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour {

	//[SteamVR_DefaultAction("Squeeze")]
	public SteamVR_Action_Single squeezeAction;

	public SteamVR_Action_Vector2 touchPadAction;

	public SteamVR_Action_Boolean touchPadClick;

	private float runSpeed = 5;

	private PlayerController playerController;
	private bool touchClicked;

	//public GameObject rightHand;
	//public GameObject leftHand;

	// Use this for initialization
	void Start () {
		playerController = GetComponentInChildren<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Left touchpad touchzone.
		Vector2 touchpadValue = touchPadAction.GetAxis (SteamVR_Input_Sources.LeftHand);

		// The click.
		touchClicked = touchPadClick.GetState (SteamVR_Input_Sources.LeftHand);

		// When pushing down the left touchpad, move based on where your thumb is
		if (touchClicked) {
			// TODO: Move by some modifier of the touchpad value.
			transform.Translate (touchpadValue.x * Vector3.right * runSpeed * Time.deltaTime, Space.World);	//transform.Translate (touchpadValue * Vector3.forward * runSpeed * Time.deltaTime, Space.World);
			transform.Translate (touchpadValue.y * -Vector3.forward * runSpeed * Time.deltaTime, Space.World);
			print ("Click!");
		}

		// Fire gun - Right Trigger
		if (SteamVR_Actions._default.GrabPinch.GetStateDown (SteamVR_Input_Sources.RightHand)) {
			StartCoroutine (playerController.fireGun ());
			print ("Fire!");
			if (playerController == null) {
				print ("Null!");
			}
		}






		/*if (Controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			Vector2 touchPadAxis = Controller.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);
			if (touchPadAxis.y > 0.7f); // Up
			if (touchPadAxis.y < -0.7f); // Down
			if (touchPadAxis.x > 0.7f); // Right
			if (touchPadAxis.x < -0.7f); // Left
		}*/
	}
}
