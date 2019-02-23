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

	// Timer for how long the stateText will be active.
	private int stateTextTimer;

	// The white bar that represents a visual healthbar.
	public Image HPBar;

	// The text that represents a numerical healthbar.
	public Text HPText;

	// Text that represents what "state" the mech is in.
	public Text stateText;

	// The overall health bar and status section.
	public Image StatusBar;

	// A sprite for lose text.
	public Sprite loseBar;

	// The player identifier.
	public Text Identifier;

	// The prefab for launching a kill screen animation.
	public GameObject killScreenObj;

	// Use this for initialization
	void Start () {
		HP = (int)maxHP;
	}
	
	// Update is called once per frame
	void Update () {

		if (stateTextTimer > 0) {
			stateTextTimer--;
		} else {
			stateText.text = "";
		}

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
		stateText.text = "HIT";
		stateText.color = new Color (1f, 0.5f, 0.0f);
		stateTextTimer = 200;
	}

	private void UIUpdate() {
		HPText.text = HP.ToString();
		HPBar.fillAmount = (HP / maxHP);
		if (HP <= 0) {
			HPText.text = "";
			stateText.text = "";
			StatusBar.sprite = loseBar;
			Identifier.text = "";
		}

	}

	private void Die() {
		dead = true;
		Instantiate (killScreenObj);
		Time.timeScale = 0.3f;
		GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
	}

}
