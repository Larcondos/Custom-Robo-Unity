using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArc : MonoBehaviour {
	// I need to detect when I'm done moving and explode!


	private ParabolaController paraC;

	void Start () {
		paraC = GetComponent<ParabolaController> ();
	}
		
}
