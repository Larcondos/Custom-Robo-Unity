using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeBar : MonoBehaviour {

	public Image not;
	public Image notBG;
	private float noticeAmt;
	private AI aiScript;
	public Material thisMat;
	public Text HPText;

	// Use this for initialization
	void Start () {
		aiScript = GetComponent<AI> ();

		// Quality of Life, custom materials, not AI.
		not.material = thisMat;
		HPText.material = thisMat;
		HPText.color = Color.green;
	}
	
	// Update the "notice bar" to fill up respectively for this AI's current level of alertness. Also only exist if it's not 0.
	void Update () {
		not.fillAmount = (aiScript.getHuntChecker () / 200f);
		if (not.fillAmount > 0) {
			notBG.fillAmount = 1;
			HPText.text = "HP: " + aiScript.getHP ().ToString ();
		}
		else {
			notBG.fillAmount = 0;
			HPText.text = "";
		}

	}
}
