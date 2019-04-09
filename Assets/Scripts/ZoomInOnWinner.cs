using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script zooms in on the winner and will let them do a dance and play victory music.
public class ZoomInOnWinner : MonoBehaviour {

	public AudioSource victoryAudio;
	public AudioClip vic;

	public GameObject cam;
	private CameraController camC;
	private Vector3 camFinalLocation;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		camFinalLocation = (this.transform.position + (transform.forward * 3) + (transform.up));
		StartCoroutine (playVictory ());
		camC = cam.GetComponent<CameraController> ();
		camC.winnerMode(this.gameObject, camFinalLocation);
	}
	
	// Update is called once per frame
	void Update () {
		

	}


	IEnumerator playVictory () {

		this.gameObject.AddComponent<AudioSource> ();
		victoryAudio = GetComponent<AudioSource> ();
		victoryAudio.playOnAwake = false;
		vic = Resources.Load<AudioClip>("Audio/SFX/Victory SFX");
		victoryAudio.clip = vic;

		// TODO: Lower the other audios once a winner is picked.
		// Lock the players movement so they done.
		gameObject.GetComponent<PlayerController>().enabled = false;

		yield return new WaitForSeconds (1.5f);
		victoryAudio.Play ();
		print ("Hear it?");
	}
}
