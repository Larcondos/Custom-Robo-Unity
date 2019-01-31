using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCube : MonoBehaviour {

	// The box's HP. Change to private after testing.
	public float HP;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (HP <= 0) {

			Destroy (this.gameObject);
		}
	}

	public void takeDamage(float dmg) {
		HP -= dmg;
	}
}
