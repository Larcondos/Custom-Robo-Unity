using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLaunch : MonoBehaviour {

	public GameObject pivotBall;
	private Transform center;
	private Vector3 axis;
	private Vector3 desiredPosition;
	public float radius = 2.0f;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 200.0f;

	// The cube the launcher will shoot.
	public GameObject cube;

	// A specific number used to determine how long until the official "Start" of the match.
	private int countdown  = 1000;

	// Use this for initialization
	void Start () {
		//cube = GameObject.FindWithTag("Cube");
		center = pivotBall.transform;
		transform.position = (transform.position - center.position).normalized * radius + center.position;
		radius = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//while (countdown > 0) {
		countdown--;

		transform.RotateAround (center.position, Input.GetAxis ("Horizontal"), rotationSpeed * Time.deltaTime);

		desiredPosition = (transform.position - center.position).normalized * radius + center.position;
			
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
			//if (Input.GetAxis ("Horizontal") != 0)
				//rb.AddForce (Input.GetAxis ("Horizontal") * Vector3.right * runSpeed, ForceMode.Force);

			//if (Input.GetAxis ("Vertical") != 0)
				//rb.AddForce (Input.GetAxis ("Vertical") * Vector3.forward * runSpeed, ForceMode.Force);
		//}
	}
}
