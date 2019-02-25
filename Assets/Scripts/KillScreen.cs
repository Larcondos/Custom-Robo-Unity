﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillScreen : MonoBehaviour {

	public Image KO, KO2, blackOverlay, blackOverlay2;

	private float overlayFillAmt;

	int KOCounter = 0;
	int KO2Counter = 0;

	// Lets the fully complete animation screen stay for a little bit, but then goes away.
	private float goAwayTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		overlayFiller ();
		KOSlider ();

		// Once the timer has gone up to 200 (reasonable enough time here), the game goes back to a normal state to allow the victory stuff to play.
		if (goAwayTimer > 200) {
			Time.timeScale = 1;
			Destroy (this.gameObject);
		}
	}

	void KOSlider() {
		// KO starts at -800, KO2 at 800, both go to 0.

		// TODO: Fix this scaling, this is bad syntax.
		if (KOCounter < 30) {
			KO.transform.Translate (27f, 0f, 0f);
			KOCounter++;
		}
		if (KOCounter == 30) {
			KO.transform.Translate (-10f, 0f, 0f);
			KOCounter++;
		}

		if (KO2Counter < 30) {
			KO2.transform.Translate (-27f, 0f, 0f);
			KO2Counter++;
		}
		if (KO2Counter == 30) {
			KO2.transform.Translate (10f, 0f, 0f);
			KO2Counter++;
		}

		// TODO: Both start at scale 0.1, finish at scale 0.7
	}

	void overlayFiller() {
		if (overlayFillAmt < 1) {
			overlayFillAmt += 0.03f;
		}
		if (overlayFillAmt >= 1 || goAwayTimer > 0) {
			goAwayTimer++;
		}

		blackOverlay.fillAmount = overlayFillAmt;
		blackOverlay2.fillAmount = overlayFillAmt;
	}


}
