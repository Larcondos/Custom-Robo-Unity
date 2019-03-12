using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillScreen : MonoBehaviour {

	// Various images that will be moved during the kill screen.
	public Image KO, KO2, blackOverlay, blackOverlay2;

	// Used to control the overlay fill amount.
	private float overlayFillAmt;

	// Used for movement of KO timer.
	int KOCounter = 0;
	int KO2Counter = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine(goAway ());
	}
	
	// Update is called once per frame
	void Update () {
		overlayFiller ();
		KOSlider ();

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

	// Fills the overlay overtime.
	void overlayFiller() {
		if (overlayFillAmt < 1) {
			overlayFillAmt += 0.03f;
		}

		blackOverlay.fillAmount = overlayFillAmt;
		blackOverlay2.fillAmount = overlayFillAmt;
	}

	IEnumerator goAway() {



		yield return new WaitForSeconds (3);

		Time.timeScale = 1;
		Destroy (this.gameObject);
	}


}
