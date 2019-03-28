﻿using System.Collections;
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

	/*
	private Rigidbody rb;
	private float jumpHeight = 8f;
	private float runSpeed = 8f;

	// Use this for initialization

	// The camera game object. Used to assign the focus on them.
	private GameObject cam;

	// Camera Controller script on the camera
	private CameraController cameraCont;

	void Start () {
		rb = GetComponent<Rigidbody>();
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraCont = cam.GetComponent<CameraController> ();
		cameraCont.targets.Add (this.gameObject.transform);
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

*/

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

	// Bullets for the main gun to shoot.
	public GameObject bullet;

	// An access variable for the player's rigidbody.
	private Rigidbody rb;

	// How high this mech can jump.
	private float jumpHeight = 8f;

	// Number of jumps this mech took.
	private int jumpCount = 0;

	// Max number of jumps on this mech.
	public int maxJumps;

	// How quickly this mech can run.
	private float runSpeed = 3f;

	// Is a bomb being aimed right now? (Is R button down?)
	private bool aimingBomb = false;

	// GameObject for the Pod
	public GameObject pod;

	// A Series of empty game objects that represent locations on the mech. Used for parenting and easy placement of projectiles, etc.
	public GameObject backPart;
	public GameObject gunPart;
	public GameObject bombPart;
	public GameObject legsPart;

	// The camera game object. Used to assign the focus on them.
	private GameObject cam;

	// Camera Controller script on the camera
	private CameraController cameraCont;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraCont = cam.GetComponent<CameraController> ();
		cameraCont.targets.Add (this.gameObject.transform);
	}

	// Update is called once per frame
	void Update () {

		if (enemy == null) {
			enemy = GameObject.FindGameObjectWithTag ("Player");
		}

		// If this is still null, it means the actual enemy has not yet spawned!
		if (enemy == null) {
			enemy = GameObject.FindGameObjectWithTag ("Respawn");
		}

		getInput ();

		//Debug Block for inital mappings
		for (int i = 0;i < 20; i++) {
			if(Input.GetKeyDown("joystick 1 button "+i)){
				print("joystick 1 button "+i);
			}
		}



	}


	void getInput() {
		// If you're aiming a bomb you can't do anything else.
		if (!aimingBomb) {
			if (Input.GetAxis ("Horizontal2") != 0) {
				transform.Translate (Input.GetAxis ("Horizontal") * Vector3.right * runSpeed * Time.deltaTime, Space.World);
			}

			if (Input.GetAxis ("Vertical2") != 0) {
				transform.Translate (Input.GetAxis ("Vertical") * Vector3.forward * runSpeed * Time.deltaTime, Space.World);
			}

			// Jumps the player.
			// TODO: Needs to have limits on jumps.
			// TODO: Air Dashing.
			if (Input.GetButtonDown ("Jump2")) {
				if (jumpCount < maxJumps) {
					jump ();
					jumpCount++;
				}
			}

			// Fires the gun.
			if (Input.GetButtonDown ("GunFire2")) {
				fireGun ();
			}

			// TODO: Will fire the pod weapon.
			if (Input.GetButtonDown ("PodFire2")) {
				firePod ();
			}

			// TODO: Will launch a charge attack.
			if (Input.GetButtonDown ("ChargeAttack2")) {
				chargeAttack ();
			}
		}

		// Begins aiming of the bomb whilst held down.
		if (Input.GetButton ("BombFire2")) {
			aimBomb ();
		}

		// Launches the bomb when the trigger is lifted.
		if (Input.GetButtonUp ("BombFire2")) {
			fireBomb ();
		}

		// TODO: Will pause the game.
		if (Input.GetButtonDown ("Pause2")) {
			pauseGame ();
		}
	} 

	void fireGun() {
		var v = Instantiate (bullet, gunPart.transform.position, Quaternion.identity);
		v.GetComponent<BulletPath> ().setTarget (enemy, this.gameObject);
	}

	#region Bombs

	// This script is used to place the location you wish your 'bomb' weapon to drop.
	// It also creates and manages the parabola for the bomb to follow.
	void aimBomb() {
		// On the first frame where this is called, it will instantiate a marker for the bomb's target area.
		if (aimingBomb != true)
			bombMarkerInstance = Instantiate (bombMarker, enemy.transform.position + (Vector3.down * .5f), Quaternion.identity);

		// The bomb is now being aimed, so no more instances will spawn.
		aimingBomb = true;

		// Detects control stick movement to place the bomb marker.
		if (Input.GetAxis ("Horizontal2") != 0)
			bombMarkerInstance.transform.Translate (Input.GetAxis ("Horizontal") * Vector3.right * 3* Time.deltaTime);
		if (Input.GetAxis ("Vertical2") != 0)
			bombMarkerInstance.transform.Translate (Input.GetAxis ("Vertical") * Vector3.forward * 3 * Time.deltaTime);
	}

	void fireBomb() {
		// You are no longer aiming the bomb, so reset this variable.
		aimingBomb = false;

		// Create a new parabola to make an arc.
		var para = Instantiate (parabola, Vector3.zero, Quaternion.identity);

		// Grab the children transforms to make your main points.
		Transform[] paraRoots = para.GetComponentsInChildren<Transform> ();

		// Sets the start location.
		paraRoots [3].position = bombPart.transform.position;

		// Sets the target location.
		paraRoots [1].position = bombMarkerInstance.transform.position;

		// A middle point, with a height factor added in at the end.
		paraRoots [2].position = ((bombPart.transform.position + bombMarkerInstance.transform.position) * 0.5f) + (.25f * Vector3.up * Vector3.Distance(bombPart.transform.position, bombMarkerInstance.transform.position));

		// Spawn the bomb, and assign it the path of the parabola we made.
		var bomba = Instantiate (bomb, transform.position, Quaternion.identity);
		bomba.GetComponent<ParabolaController> ().ParabolaRoot = para;
		Destroy (para);
		Destroy (bombMarkerInstance);
	}

	#endregion Bombs

	// Drop that baby.
	void firePod() {
		var newPod = Instantiate (pod, backPart.transform.position, Quaternion.identity);
		newPod.GetComponent<MinePod>().assignParent (this.gameObject);
	}

	void chargeAttack() {
		Debug.Log ("Charge Attack Activated");
	}

	void jump() {
		rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);
	}

	// Freezes the game.
	void pauseGame() {
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
		}
		else {
			Time.timeScale = 0;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Floor") {
			jumpCount = 0;
		}
	}


}


