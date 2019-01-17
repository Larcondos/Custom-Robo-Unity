using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
	public Camera m_Camera;
	int searching;

	void Start() {
		//m_Camera = GameObject.FindGameObjectWithTag ("Player").GetComponent<Camera> ();
	}

	void Update()
	{
			
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
			m_Camera.transform.rotation * Vector3.up);
	}
}