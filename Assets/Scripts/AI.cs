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
	}

	// Update is called once per frame
	void Update () {
		// Only if the agent is within a range and has more places to go...
		if (agent.remainingDistance < 0.5f && waypoints.Length != 0)
			calculateNextPoint ();
		//if (playStats.
	}

	// We use this to calculate our closest objective.
	void calculateNextPoint() {
		// Recalculate our waypoints in case any have been removed.
		int randomNum = Random.Range(0,waypoints.Length);
		agent.SetDestination (waypoints [randomNum].transform.position);

	}
		
	void OnTriggerEnter(Collider col) {
	}
		
	void OnCollisionEnter(Collision col) {
	}
}