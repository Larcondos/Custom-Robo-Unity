using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script zooms in on the winner and will let them do a dance and play victory music.
public class ZoomInOnWinner : MonoBehaviour {

	public AudioSource victoryAudio;
	public AudioClip vic;

	public GameObject cam;
	private Vector3 camFinalLocation;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		camFinalLocation = this.transform.position + (Vector3.forward * 5) + (Vector3.up * 2);
		StartCoroutine (playVictory ());
	}
	
	// Update is called once per frame
	void Update () {
		cam.GetComponent<CameraController> ().winnerMode(this.gameObject);
	}


	IEnumerator playVictory () {

		this.gameObject.AddComponent<AudioSource> ();
		victoryAudio = GetComponent<AudioSource> ();
		victoryAudio.playOnAwake = false;
		vic = Resources.Load<AudioClip>("Audio/SFX/Victory SFX");
		victoryAudio.clip = vic;

		// TODO: Lower the other audios once a winner is picked.

		yield return new WaitForSeconds (1.5f);
		victoryAudio.Play ();
		print ("Hear it?");
	}
}
