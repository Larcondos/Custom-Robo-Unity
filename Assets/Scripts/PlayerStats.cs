using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	// Health. Always starts at 1000, just like in the real game!
	private const float maxHP = 1000;

	// Current HP.
	private int HP;

	// Keeps track if you are dead or not.
	private bool dead = false;

	// Represents the various states, 1 = ready, 2 = rebirth, 3 = downed.
	private int state;

	// How much damage this user must take to get knocked down
	private int knockdownLimit;

	// The white bar that represents a visual healthbar.
	public Image HPBar;

	// The text that represents a numerical healthbar.
	public Text HPText;

	// Text that represents what "state" the mech is in.
	public Text stateText;

	// The prefab for launching a kill screen animation.
	public GameObject killScreenObj;

	// Use this for initialization
	void Start () {
		HP = (int)maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (HP <= 0 && !dead)
			Die();
	}

	void OnCollisionEnter(Collision col) {
		
	}

	void OnTriggerEnter(Collider col) {
		
	}

	public void doDamage(int ATK) {
		HP -= ATK;
		UIUpdate ();
	}

	private void UIUpdate() {
		HPText.text = HP.ToString();
		HPBar.fillAmount = (HP / maxHP);
		print (HP);
	}

	private void Die() {
		dead = true;
		Instantiate (killScreenObj);
		Time.timeScale = 0.3f;
		GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
	}

}
