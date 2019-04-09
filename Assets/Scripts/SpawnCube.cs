using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCube : MonoBehaviour {

	// The camera game object. Used to assign the focus on them.
	private GameObject cam;

	// Camera Controller script on the camera
	private CameraController cameraCont;

	// This is a random number that determines how long until a cube will open up. It's random, not fair!
	private int randNum; 

	// Rigidbody component attached to this object.
	private Rigidbody rb;

	// The sprites for the number to display upon.
	public Sprite[] sprites;

	// The Image for the sprites to update to.
	public Image img;

	// How fast is the cube moving? Used to determine when to begin the countdown for real.
	private float speed;

	// Is the cube sleeping?
	private bool sleeping;

	// Has this cube touched the floor?
	private bool touchedFloor;

	// How many buttons did the player mash while in cube form?
	private float buttonsMashed;

	// A "see thru" material for the numbers to appear through in the cube.
	public Material seeThru;

	// The player object to spawn.
	public GameObject player;

	// For the second player, they get seperate controls. Sorta.
	private bool otherController;

	// Use this for initialization
	void Start () {
		randNum = Random.Range (1, 7);
		rb = GetComponent<Rigidbody> ();
		//mesh = GetComponent<MeshRenderer> ();
		img.material = seeThru;
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraCont = cam.GetComponent<CameraController> ();
		cameraCont.targets.Add (this.gameObject.transform);

		if (this.gameObject.name == "P2 Launcher")
			otherController = true;

	}

	// Update is called once per frame
	void Update () {
		if (!sleeping) {
			//mesh.material = sprites [randNum - 1];
			img.sprite = sprites[randNum - 1];
			speed = rb.velocity.magnitude;
			if (speed < 0.01 && speed != 0 && touchedFloor) {
				rb.velocity = new Vector3 (0, 0, 0);
				StartCoroutine (tickDown ());
				sleeping = true;

				print ("Sleep!");
			} 
		}
		//print (randNum);
		if (sleeping) {
			getMash ();
		}
	}

	// If you press any of these buttons, you get to respawn faster!
	void getMash() {

		// P1
		if (!otherController) {
			if (Input.GetButtonDown ("Jump")) {
				buttonsMashed++;
			}

			if (Input.GetButtonDown ("GunFire")) {
				buttonsMashed++;		
			}

			if (Input.GetButtonDown ("PodFire")) {
				buttonsMashed++;		
			}


			if (Input.GetButtonDown ("ChargeAttack")) {
				buttonsMashed++;		
			}
		

			if (Input.GetButtonDown ("BombFire")) {
				buttonsMashed++;
			}

			if (Input.GetButtonDown ("Pause")) {
				buttonsMashed++;		
			}
		}

		//P2
		if (otherController) {
			if (Input.GetButtonDown ("Jump2")) {
				buttonsMashed++;
			}

			if (Input.GetButtonDown ("GunFire2")) {
				buttonsMashed++;		
			}

			if (Input.GetButtonDown ("PodFire2")) {
				buttonsMashed++;		
			}


			if (Input.GetButtonDown ("ChargeAttack2")) {
				buttonsMashed++;		
			}


			if (Input.GetButtonDown ("BombFire2")) {
				buttonsMashed++;
			}

			if (Input.GetButtonDown ("Pause2")) {
				buttonsMashed++;		
			}
		}
	}
		
	// Ticks down the timer on the cube.
	IEnumerator tickDown() {
		
		yield return new WaitForSeconds (1 - (buttonsMashed * 0.03f));

		randNum--;

		// Only call if it's not time to destroy.
		if (randNum > 0) {
			img.sprite = sprites [randNum - 1];
			print ("Next Time Tick: " + (1 - (buttonsMashed * 0.03f)));
			StartCoroutine (tickDown ());
			buttonsMashed = 0;
		} else {
			// The player won't be hurt when they spawn normally.
			spawnPlayer (0,0);
		}
	}

	// This way if the cube launches straight up it doesn't register the one frame at peak as "sleeping".
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Destructible" || col.gameObject.tag == "Wall") {
			touchedFloor = true;
		} else {
			// If you get shot as a cube, you take some base damage. Sorry, that's really how it be.
			openEarly (50,15);
		}
	}

	void spawnPlayer(int dmg, int down) {
		Instantiate (player, transform.position + Vector3.up, new Quaternion(0,180,0,0));

		cameraCont.targets.Add (player.transform);
		cameraCont.targets.Remove(this.gameObject.transform);
		Destroy (this.gameObject);

		player.GetComponent<PlayerStats> ().doDamage (dmg, down);
	}

	public void openEarly(int dmg, int down) {
		StopAllCoroutines ();
		randNum = 0;
		spawnPlayer (dmg, down);


	}
}
