﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCube : MonoBehaviour {

	// The box's HP.
	private float HP = 200;

	// Material for the box.
	private Material mat;

	// Cubes related to destruction of object.
	private float cubeSize = 0.2f;
	private int cubesInRow = 5;

	// A pivot for the cubes.
	float cubesPivotDistance;
	Vector3 cubesPivot;

	// Forces for the destruction of object.
	private float explosionForce = 50f;
	private float explosionRadius = 4f;
	private float explosionUpward = 0.4f;

	// Use this for initialization
	void Start() {
		mat = GetComponent<MeshRenderer> ().material;

		//calculate pivot distance
		cubesPivotDistance = cubeSize * cubesInRow / 2;
		//use this value to create pivot vector)
		cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

	}

	// Deals damage to this unique object, and will check if this object needs to die or not.
	public void doDamage(int dmg) {
		HP -= dmg;
		if (HP <= 0)
			explode ();
	}

	public void explode() {
		//make object disappear
		Destroy(this.gameObject);

		//loop 3 times to create 5x5x5 pieces in x,y,z coordinates
		for (int x = 0; x < cubesInRow; x++) {
			for (int y = 0; y < cubesInRow; y++) {
				for (int z = 0; z < cubesInRow; z++) {
					createPiece(x, y, z);
				}
			}
		}

		//get explosion position
		Vector3 explosionPos = transform.position;
		//get colliders in that position and radius
		Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
		//add explosion force to all colliders in that overlap sphere
		foreach (Collider hit in colliders) {
			//get rigidbody from collider object
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null) {
				//add explosion force to this body with given parameters
				rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
			}
		}

	}

	void createPiece(int x, int y, int z) {

		// create piece, and name it.
		GameObject piece;
		piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
		piece.name = "cubeShard";
		// set piece position and scale, and assign material.
		piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
		piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
		piece.GetComponent<MeshRenderer> ().material = mat;
		// add rigidbody and set mass, add destruction.
		piece.AddComponent<Rigidbody>();
		piece.GetComponent<Rigidbody>().mass = cubeSize;
		piece.AddComponent<DestroyOnTouch> ();
	}
		

}
