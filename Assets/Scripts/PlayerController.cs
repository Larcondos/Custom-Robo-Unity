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

	#region Cooldowns
	// A series of timers for shooting.
	public float gunFireCooldown, bombFireCooldown, podFireCooldown, chargeFireCooldown;

	// Booleans to tell if they are actively cooling
	public bool bombCooling, gunCooling, podCooling, chargeCooling;

	#endregion Cooldowns

	// The object that will act as a charge hitbox.
	public GameObject chargeObject;

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

	// Determines if this is for player one, or two.
	private bool isPlayerOne;

	// Audio Source
	private AudioSource audioPlayer;

	private AudioClip gunFireSound;

	public GameObject vrLeftHandAim, vrRightHandAim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraCont = cam.GetComponent<CameraController> ();
		//cameraCont.targets.Add (this.gameObject.transform);
		if (this.gameObject.tag == "Player") {
			isPlayerOne = true;
		}
		audioPlayer = GetComponent<AudioSource> ();
		gunFireSound = Resources.Load<AudioClip> ("Audio/Weapon_Sounds/BASIC");
	}

	// TODO: Make cubes hurtable.
	
	// Update is called once per frame
	void Update () {


		if (this.gameObject.tag != "Player2") {
			if (enemy == null && isPlayerOne) {
				enemy = GameObject.FindGameObjectWithTag ("Player2");
			} else if (enemy == null && !isPlayerOne)
				enemy = GameObject.FindGameObjectWithTag ("Player");

			// If this is still null, it means the actual enemy has not yet spawned!
			if (enemy == null) {
				enemy = GameObject.FindGameObjectWithTag ("Respawn");
			}
		}
		if (isPlayerOne) {
			getInput ();
		} 
		// There is a separate "getInput" for the player two, using a different control layout.
		else {
			getInput2 ();
		}



		
	}


	void getInput() {
		// If you're aiming a bomb you can't do anything else.
		if (!aimingBomb) {
			if (Input.GetAxis ("Horizontal") != 0) {
				transform.Translate (Input.GetAxis ("Horizontal") * Vector3.right * runSpeed * Time.deltaTime, Space.World);
			}
		
			if (Input.GetAxis ("Vertical") != 0) {
				transform.Translate (Input.GetAxis ("Vertical") * Vector3.forward * runSpeed * Time.deltaTime, Space.World);
			}

		// Jumps the player.
			// TODO: Air Dashing.
			if (Input.GetButtonDown ("Jump")) {
				if (jumpCount < maxJumps) {
					jump ();
					jumpCount++;
				}
			}

			// Fires the gun.
			if (Input.GetButtonDown ("GunFire") && gunFireCooldown == 0) {
				StartCoroutine (fireGun ());
			}

			// Fires the pod weapon.
			if (Input.GetButtonDown ("PodFire") && podFireCooldown == 0) {
				firePod ();
			}
			
			// Launches a charge attack.
			if (Input.GetButtonDown ("ChargeAttack") && chargeFireCooldown == 0) {
				StartCoroutine (chargeAttack ());
			}
		}

		// Begins aiming of the bomb whilst held down.
		if (Input.GetButton ("BombFire") && bombFireCooldown == 0) {
			aimBomb ();
		}

		// Launches the bomb when the trigger is lifted.
		if (Input.GetButtonUp ("BombFire") && bombFireCooldown == 0) {
			fireBomb ();
		}

		//Pauses the game.
		if (Input.GetButtonDown ("Pause")) {
			pauseGame ();
		}
	} 


	void getInput2() {
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
			if (Input.GetButtonDown ("GunFire2") && gunFireCooldown == 0) {
				fireGun ();
			}

			// TODO: Will fire the pod weapon.
			if (Input.GetButtonDown ("PodFire2") && podFireCooldown == 0) {
				firePod ();
			}

			// TODO: Will launch a charge attack.
			if (Input.GetButtonDown ("ChargeAttack2") && chargeFireCooldown == 0) {
				chargeAttack ();
			}
		}

		// Begins aiming of the bomb whilst held down.
		if (Input.GetButton ("BombFire2") && bombFireCooldown == 0) {
			aimBomb ();
		}

		// Launches the bomb when the trigger is lifted.
		if (Input.GetButtonUp ("BombFire2") && bombFireCooldown == 0) {
			fireBomb ();
		}

		// TODO: Will pause the game.
		if (Input.GetButtonDown ("Pause2")) {
			pauseGame ();
		}
	} 


	public IEnumerator fireGun() {


		var v = Instantiate (bullet, gunPart.transform.position, Quaternion.identity);
		v.GetComponent<BulletPath> ().setTarget (enemy, this.gameObject, vrRightHandAim);
		gunFireCooldown = v.GetComponent<BulletPath> ().RLD;
//		audioPlayer.clip = gunFireSound;
		//audioPlayer.Play ();

		yield return new WaitForSeconds (0.15f);

		var v2 = Instantiate (bullet, gunPart.transform.position, Quaternion.identity);
		v2.GetComponent<BulletPath> ().setTarget (enemy, this.gameObject, vrRightHandAim);
		//audioPlayer.clip = gunFireSound;
		//audioPlayer.Play ();

		yield return new WaitForSeconds (0.15f);

		var v3 = Instantiate (bullet, gunPart.transform.position, Quaternion.identity);
		v3.GetComponent<BulletPath> ().setTarget (enemy, this.gameObject, vrRightHandAim);
	//	audioPlayer.clip = gunFireSound;
	//	audioPlayer.Play ();

		yield return new WaitForSeconds (0.15f);

		cooldownTimer ();

	}

	#region Bombs

	// This function is used to place the location you wish your 'bomb' weapon to drop.
	// It also creates and manages the parabola for the bomb to follow.
	public void aimBomb() {
		// On the first frame where this is called, it will instantiate a marker for the bomb's target area.
		if (aimingBomb != true)
			bombMarkerInstance = Instantiate (bombMarker, enemy.transform.position + (Vector3.down * .5f), Quaternion.identity);

		// The bomb is now being aimed, so no more instances will spawn.
		aimingBomb = true;

		// Detects control stick movement to place the bomb marker.
		if (Input.GetAxis ("Horizontal") != 0)
			bombMarkerInstance.transform.Translate (Input.GetAxis ("Horizontal") * Vector3.right * 3* Time.deltaTime);
		if (Input.GetAxis ("Vertical") != 0)
			bombMarkerInstance.transform.Translate (Input.GetAxis ("Vertical") * Vector3.forward * 3 * Time.deltaTime);
	}

	// Instaciates a path for the bomb, and it's on it's way.
	public void fireBomb() {
		// You are no longer aiming the bomb, so reset this variable.
		aimingBomb = false;

		// Create a new parabola to make an arc.
		var para = Instantiate (parabola, Vector3.zero, Quaternion.identity);

		// Grab the children transforms to make your main points.
		Transform[] paraRoots = para.GetComponentsInChildren<Transform> ();

		// Sets the start location.
		paraRoots [3].position = bombPart.transform.position;

		// Sets the target location.
		if (bombMarkerInstance != null) {
			paraRoots [1].position = bombMarkerInstance.transform.position;
		}
		// A middle point, with a height factor added in at the end.
		paraRoots [2].position = ((bombPart.transform.position + bombMarkerInstance.transform.position) * 0.5f) + (.25f * Vector3.up * Vector3.Distance(bombPart.transform.position, bombMarkerInstance.transform.position));

		// Spawn the bomb, and assign it the path of the parabola we made.
		var bomba = Instantiate (bomb, transform.position, Quaternion.identity);
		bomba.GetComponent<ParabolaController> ().ParabolaRoot = para;
		Destroy (para);
		Destroy (bombMarkerInstance);
		cooldownTimer ();
		// Set the bomb limit.
		bombFireCooldown = 2;
	}

	#endregion Bombs

	// Drop that baby.
	public void firePod() {
		var newPod = Instantiate (pod, backPart.transform.position, Quaternion.identity);
		newPod.GetComponent<MinePod>().assignParent (this.gameObject);
		podFireCooldown = 3f;
		cooldownTimer ();
	}

	public IEnumerator chargeAttack() {

		// Start the cooldown immediately.
		chargeFireCooldown = 3f;

		Debug.Log ("Charge Attack Activated");
		// The charge attack hitbox is 2 standard units wide, 4 units tall, and 4 units forward.

		// Charge up for 1 second before dashing with the attack. 
		yield return new WaitForSeconds(1);

		// Check if an enemy is within the hitbox RIGHT BEFORE MOVING
		chargeObject.SetActive(true);

		yield return new WaitForSeconds (0.1f);
		chargeObject.SetActive(false);

		// Move forward 4 units.
		transform.position += (transform.forward * 4);
	}

	public void jump() {
		rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);
	}

	// Freezes the game.
	public void pauseGame() {
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			// TODO: Remove the pause screen.
		}
		else {
			Time.timeScale = 0;
			// TODO: Add a pause screen.
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Floor") {
			jumpCount = 0;
		}
	}

	/*void OnTriggerEnter(Collider col) {
		if ((col.gameObject.tag == "Player" || col.gameObject.tag == "Player2") && col.gameObject.tag != this.gameObject.tag) {
			print ("Charge hit!");
			print (col.gameObject.tag);
			col.gameObject.GetComponent<PlayerStats> ().chargeHit ();
		}
	}*/

	IEnumerator gunTimerCooldown(float x) {
		gunCooling = true;
		yield return new WaitForSeconds (x);

		gunCooling = false;
		gunFireCooldown = 0;
	}

	IEnumerator bombTimerCooldown(float x) {
		bombCooling = true;

		yield return new WaitForSeconds (x);
		bombCooling = false;
		bombFireCooldown = 0;
	}

	IEnumerator podTimerCooldown(float x) {
		podCooling = true;
		yield return new WaitForSeconds (x);

		podCooling = false;
		podFireCooldown = 0;
	}

	IEnumerator chargeTimerCooldown(float x) {
		chargeCooling = true;
		yield return new WaitForSeconds (x);

		chargeCooling = false;
		chargeFireCooldown = 0;
	}



	void cooldownTimer() {
		if (bombFireCooldown > 0 && !bombCooling)
			StartCoroutine (bombTimerCooldown (bombFireCooldown));
		if (gunFireCooldown > 0 && !gunCooling)
			StartCoroutine (gunTimerCooldown (gunFireCooldown));
		if (podFireCooldown > 0 && !podCooling)	
			StartCoroutine (podTimerCooldown (podFireCooldown));
		if (chargeFireCooldown > 0 && !chargeCooling)
			StartCoroutine (chargeTimerCooldown (chargeFireCooldown));
	}

}


