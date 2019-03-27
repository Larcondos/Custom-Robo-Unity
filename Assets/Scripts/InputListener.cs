using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		System.Array values = System.Enum.GetValues(typeof(KeyCode));
		foreach(KeyCode code in values){
			if(Input.GetKeyDown(code)){ print(System.Enum.GetName(typeof(KeyCode), code)); }                    
		}

	}
}
