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

	// How much Knockdown this user must take to get knocked down (public as varies by mech)
	public int knockdownLimit = 100;

	// How much Knockdown has this user recieved lately?
	private int curKnockdown = 0;

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
		StartCoroutine (deductKnockdown ());
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

	public void doDamage(int ATK, int DWN) {
		// Just to spice things up, every time you take damage it can be multiplied by up to -20% or up to 20%.
		HP -= (ATK * Random.Range(80, 120)) / 100;

		// Add to your knockdown rate.
		curKnockdown += DWN;

		// On a hit, update your state text to reflect that.
		stateText.text = "HIT";
		stateText.color = new Color (1f, 0.5f, 0.0f);
		stateTextTimer = 200;

		// Update UI in general afterwards, in case we need to overwrite it.
		UIUpdate ();
	}

	private void UIUpdate() {
		HPText.text = HP.ToString();
		HPBar.fillAmount = (HP / maxHP);
		if (HP <= 0) {
			HPText.text = "";
			stateText.text = "";
			Identifier.text = "";
			StatusBar.sprite = loseBar;
		}

	}

	private IEnumerator deductKnockdown() {

		print (curKnockdown);

		yield return new WaitForSeconds (1);

		// Every second, deduct some of the knockdown, as the mech can recover over time.
		if (curKnockdown > 0) {
			curKnockdown -= 5;
		}

		// If you accidentally bring down the knockdown below 0, put it back.
		if (curKnockdown < 0) {
			curKnockdown = 0;
		}

		// Start the recall again.
		StartCoroutine(deductKnockdown ());
	}

	private void Die() {
		dead = true;
		Instantiate (killScreenObj);
		Time.timeScale = 0.3f;
		GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
	}

}
