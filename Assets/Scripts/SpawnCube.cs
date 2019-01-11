using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour {

	// This is a random number that determines how long until a cube will open up. It's random, not fair!
	private int randNum; 

	// Rigidbody component attached to this object.
	private Rigidbody rb;

	// Mesh Renderer Component.
	private MeshRenderer mesh;

	// How fast is the cube moving? Used to determine when to begin the countdown for real.
	private float speed;

	// Is the cube sleeping?
	private bool sleeping;

	// All of the materials for the cube to take based on number while alseep.
	public Material[] sprites;

	// How many buttons did the player mash while in cube form?
	private float buttonsMashed;

	// Use this for initialization
	void Start () {
		randNum = Random.Range (1, 6);
		rb = GetComponent<Rigidbody> ();
		mesh = GetComponent<MeshRenderer> ();

	}

	// Update is called once per frame
	void Update () {
		if (!sleeping) {
			mesh.material = sprites [randNum - 1];
			speed = rb.velocity.magnitude;
			if (speed < 0.01 && speed != 0) {
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
		

		if (Input.GetButton ("BombFire")) {
			buttonsMashed++;
		}

		if (Input.GetButtonUp ("BombFire")) {
			buttonsMashed++;		
		}

		if (Input.GetButtonDown ("Pause")) {
			buttonsMashed++;		
		}

	}


	IEnumerator tickDown() {
		yield return new WaitForSeconds (1 - (buttonsMashed * 0.01f));

		randNum--;

		// Only call if it's not time to destroy.
		if (randNum > 0) {
			mesh.material = sprites [randNum - 1];
			buttonsMashed = 0;
			StartCoroutine (tickDown ());
		} else {
			Destroy (this.gameObject);
		}
	}
}
