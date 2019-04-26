using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

	private NavMeshAgent agent; // This allows us to move our AI.
	public GameObject[] waypoints; // A series of pickups we need to get.

	private GameObject player;  // A reference to the player.

	private PlayerController playCon;
	private PlayerStats playStats;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		calculateNextPoint ();
		playCon = GetComponent<PlayerController> ();
		playStats = GetComponent<PlayerStats> ();
	}

	// Update is called once per frame
	void Update () {
		// Only if the agent is within a range and has more places to go...
		if (agent.remainingDistance < 0.5f && waypoints.Length != 0)
			calculateNextPoint ();
		//if (playStats.

		int randomWeaponNum = Random.Range (0, 300);

		// While the AI is down we dont want it shooting or moving.
		if (playStats.downed) {
			agent.speed = 0;
			randomWeaponNum = 0;
		} else {
			agent.speed = 3.5f;
		}

		if (randomWeaponNum == 50) {
			randomWeaponAttack ();
		}
	}

	// We use this to calculate our closest objective.
	void calculateNextPoint() {
		// Recalculate our waypoints in case any have been removed.
		int randomNum = Random.Range(0,waypoints.Length);
		agent.SetDestination (waypoints [randomNum].transform.position);

	}
		
	void randomWeaponAttack() {
		int randomWepNum = Random.Range (0, 10);

		if (randomWepNum <= 7) {
			StartCoroutine(playCon.fireGun ());
		}

		if (randomWepNum < 9 && randomWepNum > 7) {
			playCon.aimBomb ();
			playCon.fireBomb ();
		}

		if (randomWepNum == 9) {
			playCon.firePod();
		}


	}



	void OnTriggerEnter(Collider col) {
	}
		
	void OnCollisionEnter(Collision col) {
	}
}