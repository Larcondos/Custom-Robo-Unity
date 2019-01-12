﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPath : MonoBehaviour {
	
	public Transform target;

	private Rigidbody rb;

	public float ATK; // Damage amount
	public float SPD; // Speed the bullets move at
	public float HMG; // How homing the bullets are
	public float DWN; // How much knockback the bullets apply
	public float RLD; // Reload Time before next shot.

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = transform.forward * SPD;

		var targetRotation = Quaternion.LookRotation(target.position - transform.position);

		rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, HMG));
	}
}