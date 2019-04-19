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

	// Use this for initialization
	void Start () {
		//playerController = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (SteamVR_Actions._default.GrabPinch.GetStateDown (SteamVR_Input_Sources.RightHand)) {
			// Right Trigger All the Way In
			//TODO: Fire the gun.
			print ("Right hand pinch");

		}

		// How the left touchPad is being touched. On a click send this info in.
		Vector2 touchpadValue = touchPadAction.GetAxis (SteamVR_Input_Sources.LeftHand);

		if (touchpadValue != Vector2.zero) {
			print (touchpadValue);
		}

		// The click.
		bool touchClicked = touchPadClick.GetState (SteamVR_Input_Sources.LeftHand);

		// We are walkin baby.
		if (touchClicked) {
			// TODO: Move by some modifier of the touchpad value.
			transform.Translate (touchpadValue.x * Vector3.right * runSpeed * Time.deltaTime, Space.World);	//transform.Translate (touchpadValue * Vector3.forward * runSpeed * Time.deltaTime, Space.World);
			transform.Translate (touchpadValue.y * Vector3.forward * runSpeed * Time.deltaTime, Space.World);
			print ("Click!");
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
