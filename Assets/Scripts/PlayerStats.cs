using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	private int HP = 1000;

	// Represents the various states, 1 = ready, 2 = rebirth, 3 = downed.
	private int state;

	public Text HPText;
	public Text stateText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
