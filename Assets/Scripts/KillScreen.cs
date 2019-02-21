using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillScreen : MonoBehaviour {

	public Image KO, KO2, blackOverlay, blackOverlay2;

	private float overlayFillAmt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		overlayFiller ();
		KOSlider ();
	}

	void KOSlider() {
		// KO starts at -800, KO2 at 800, both go to 0.

		// TODO: Fix this scaling, this is bad syntax.
		if (KO.transform.position.x < 0) {
			KO.transform.position.x += 27;
		}
		if (KO.transform.position.x > 0) {
			KO.transform.position.x = 0;
		}

		if (KO2.transform.position.x > 0) {
			KO2.transform.position.x += 27;
		}
		if (KO2.transform.position.x < 0) {
			KO2.transform.position.x = 0;
		}

		// TODO: Both start at scale 0.1, finish at scale 0.7
	}

	void overlayFiller() {
		if (overlayFillAmt < 1) {
			overlayFillAmt += 0.03f;
		}

		blackOverlay.fillAmount = overlayFillAmt;
		blackOverlay2.fillAmount = overlayFillAmt;
	}


}
