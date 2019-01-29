using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCube : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		randNum = Random.Range (1, 7);
		rb = GetComponent<Rigidbody> ();
		//mesh = GetComponent<MeshRenderer> ();
		img.material = seeThru;
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

	void getMash() {
		

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


	IEnumerator tickDown() {
		yield return new WaitForSeconds (1 - (buttonsMashed * 0.03f));

		randNum--;

		// Only call if it's not time to destroy.
		if (randNum > 0) {
			img.sprite = sprites [randNum - 1];
			print ("Buttons Mashed: " + buttonsMashed);
			buttonsMashed = 0;
			StartCoroutine (tickDown ());
		} else {
			player.transform.SetParent (null);
			player.GetComponent<Rigidbody> ().useGravity = true;
			player.GetComponent<MeshRenderer> ().enabled = true;
			player.GetComponent<SphereCollider> ().enabled = true;
			player.GetComponent<PlayerController> ().enabled = true;


			Destroy (this.gameObject);
		}
	}

	// This way if the cube launches straight up it doesn't register the one frame at peak as "sleeping".
	void OnCollisionEnter(Collision col) {
		touchedFloor = true;
	}
}
