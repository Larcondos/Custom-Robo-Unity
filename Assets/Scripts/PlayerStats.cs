using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	// Health. Always starts at 1000, just like in the real game!
	private int HP = 1000;

	// Represents the various states, 1 = ready, 2 = rebirth, 3 = downed.
	private int state;

	// How much damage this user must take to get knocked down
	private int knockdownLimit;

	// 

	public Text HPText;
	public Text stateText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		print (HP);
	}

	void OnCollisionEnter(Collision col) {
		
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Bullet")) {
			HP -= col.gameObject.GetComponent<BulletPath> ().getAttack ();
		}
	}

}
