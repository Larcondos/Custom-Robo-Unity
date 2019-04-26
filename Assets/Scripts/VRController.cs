using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour {

	//[SteamVR_DefaultAction("Squeeze")]
	public SteamVR_Action_Single squeezeAction;

	public SteamVR_Action_Vector2 touchPadAction;

	public SteamVR_Action_Boolean touchPadClick;

	public GameObject vrCamera;

	private float runSpeed = 5;

	private PlayerController playerController;
	private bool touchClickedLeft;
	private bool touchClickedRight;
 

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
		touchClickedLeft = touchPadClick.GetState (SteamVR_Input_Sources.LeftHand);
		touchClickedRight = touchPadClick.GetState (SteamVR_Input_Sources.RightHand);

		// When pushing down the left touchpad, move based on where your thumb is
		if (touchClickedLeft) {
			// TODO: Move by some modifier of the touchpad value.
			Quaternion quat = vrCamera.transform.rotation;
			//quat = new Quaternion(0, quat.y, quat.z, quat.w);
			//Vector3 fwd = quat * Vector3.forward;
			//transform.Translate(fwd * moveSpeed); 

			transform.Translate (touchpadValue.x * (Vector3.right) * runSpeed * Time.deltaTime, Space.Self);	//transform.Translate (touchpadValue * Vector3.forward * runSpeed * Time.deltaTime, Space.World);
			transform.Translate (touchpadValue.y * (quat.w * Vector3.forward) * runSpeed * Time.deltaTime, Space.Self);
			print ("Click!");
		}

		// Fire gun - Right Trigger
		if (SteamVR_Actions._default.GrabPinch.GetStateDown (SteamVR_Input_Sources.RightHand) && playerController.gunFireCooldown == 0) {

			StartCoroutine (playerController.fireGun ());
			print ("He shooting!");
		}

		// Aim bomb - Left Trigger Down
		if (SteamVR_Actions._default.GrabPinch.GetStateDown (SteamVR_Input_Sources.LeftHand) && playerController.bombFireCooldown == 0) {
			playerController.aimBomb ();
		}

		// Fire bomb - Left Trigger Up
		if (SteamVR_Actions._default.GrabPinch.GetStateUp (SteamVR_Input_Sources.LeftHand) && playerController.bombFireCooldown == 0) {
			playerController.fireBomb ();
		}

		// Drop Pod - Right touchpad pushed down.
		if (touchClickedRight && playerController.podFireCooldown == 0) {
			playerController.firePod ();
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
