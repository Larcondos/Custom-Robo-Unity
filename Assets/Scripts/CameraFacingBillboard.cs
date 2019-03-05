using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
	public GameObject m_Camera;

	void Start() {
		m_Camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	// Keep the canvas aimed at the camera.
	void Update() { 
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
			m_Camera.transform.rotation * Vector3.up);
	}
}