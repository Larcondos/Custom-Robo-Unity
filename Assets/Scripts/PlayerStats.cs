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
	// TODO: Lock Framerate and make this a constant.
	private int stateTextTimer;

	// The default timer for the stateTextTimer to be set at.
	private const int stateTextTimerResetConst = 200;

	// The white bar that represents a visual healthbar.
	public Image HPBar;

	// The text that represents a numerical healthbar.
	public Text HPText;

	// Text that represents what "state" the mech is in.
	public Text stateText;

	// The overall health bar and status section.
	public Image statusBar;

	// A sprite for lose text.
	public Sprite loseBar;

	// The player identifier.
	public Text Identifier;

	// The prefab for launching a kill screen animation.
	public GameObject killScreenObj;

	// Is the mech currently invincible? Give them this once they hit Rebirth.
	private bool invincible = false;

	// Is the mech currently downed? 
	private bool downed = false;

	// The three indicators for how much Knockdown you have currently.
	public Image knockdownLow;
	public Image knockdownMed;
	public Image knockdownHigh;


	// Use this for initialization
	void Start () {
		HP = (int)maxHP;
		StartCoroutine (deductKnockdown ());
	}
	
	// Update is called once per frame
	void Update () {

		if (stateTextTimer > 0) {
			stateTextTimer--;
		} 
		if ((!downed || !invincible) && stateTextTimer <= 0) {
			print ("I'm resetting.");
			stateText.text = "";
		}

		if (HP <= 0 && !dead)
			Die();


	}

	void OnCollisionEnter(Collision col) {
		
	}

	void OnTriggerEnter(Collider col) {
		
	}

	void knockdownBreak() {
		if (!downed || !invincible) {
			
			// Disable Movement


			// Give Invincibility for duration of movement lock and then an additional 3 secs.
			StartCoroutine (goDowned ());
		}
	}

	IEnumerator goDowned() {
		
		if (downed || invincible) {
			yield return null;
		// Don't want this being called multiple times...
		} else {
			print ("Going Down!");
			downed = true;
			stateText.text = "DOWNED";
			stateTextTimer = stateTextTimerResetConst;

			// Set the knockdown very High so the bars won't restore themselves. Just arbitrary number > 100.
			curKnockdown = 1000;

			yield return new WaitForSeconds (3);

			StartCoroutine (rebirth ());
		}
	}

	IEnumerator rebirth() {

		print ("I am reborn!");
		invincible = true;
		downed = false;
		stateText.text = "REBIRTH";
		curKnockdown = 0;
		stateTextTimer = stateTextTimerResetConst;
		UIUpdate ();

		//TODO: Make the mesh slightly transparent while invincible.

		yield return new WaitForSeconds (3);

		// Do a hard reset on the text timer once the invinicbility is over.
		stateTextTimer = 0;
		invincible = false;

	}

	public void doDamage(int ATK, int DWN) {
		// Just to spice things up, every time you take damage it can be multiplied by up to -20% or up to 20%.
		if (!invincible && !downed) {
			HP -= (ATK * Random.Range (80, 120)) / 100;

			// Add to your knockdown rate.
			curKnockdown += DWN;

			// On a hit, update your state text to reflect that.
			stateText.text = "HIT";
			stateText.color = new Color (1f, 0.5f, 0.0f);
			stateTextTimer = 180;

			// Update UI in general afterwards, in case we need to overwrite it.
			UIUpdate ();
		}

		if (downed) {
			// While downed, you only take 50% dmg. Also, knockdown does not apply.
			HP -= (ATK * Random.Range (80, 120)) / 200;
			stateText.text = "DOWNED";
			UIUpdate ();
		}
	}

	private void UIUpdate() {
		// Convert HP to be usable.
		HPText.text = HP.ToString ();
		HPBar.fillAmount = (HP / maxHP);

		// Uh Oh I'm dead.
		if (HP <= 0) {
			HPText.text = "";
			stateText.text = "";
			Identifier.text = "";
			statusBar.sprite = loseBar;
		}

		// Knockdown Indicators.
		if (!downed || !invincible) {
			if (curKnockdown > 33) {
				knockdownHigh.enabled = false;
			} else {
				knockdownHigh.enabled = true;
			}

			if (curKnockdown > 66) {
				knockdownMed.enabled = false;
			} else {
				knockdownMed.enabled = true;
			}

			if (curKnockdown > 100) {
				knockdownLow.enabled = false;
				StartCoroutine (goDowned ());
			} else {
				knockdownLow.enabled = true;
			}
		}

	}

	private IEnumerator deductKnockdown() {

		//print (curKnockdown);
		if (!dead) {
			yield return new WaitForSeconds (1);

			// Every second, deduct some of the knockdown, as the mech can recover over time.
			if (curKnockdown > 0) {
				curKnockdown -= 5;
			}

			// If you accidentally bring down the knockdown below 0, put it back.
			if (curKnockdown <= 0) {
				curKnockdown = 0;
			}

			UIUpdate ();

			// Start the recall again.
			StartCoroutine (deductKnockdown ());
		}
	}

	private void Die() {
		dead = true;
		Instantiate (killScreenObj);
		Time.timeScale = 0.3f;
		GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
	}

}
