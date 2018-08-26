using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip[] anthems;


	public AudioClip[] readySetGo;

	public AudioClip[] randomWank;

	public AudioClip[] finishWank;

	public AudioClip[] lastPlayerFinishWank;

	public AudioSource anthemPlayer;
	public AudioSource announcerPlayer;

	// Use this for initialization
	void Start () {
		
	}

	public bool isAnnouncerTalking() {
		return announcerPlayer.isPlaying ;
	}

	public void PlayAnthem(int value) {

		if (anthems [value] == null) {
			return;
		}

		anthemPlayer.PlayOneShot(anthems[value]);
	}

	public void PlayReadySetGo() {
		announcerPlayer.PlayOneShot (readySetGo [Random.Range (0, readySetGo.Length)]);
	}

	public void PlayRandomWank() {
		announcerPlayer.PlayOneShot (randomWank [Random.Range (0, randomWank.Length)], 0.75f);
	}

	public void PlayFinishWank() {
		announcerPlayer.Stop ();
		announcerPlayer.PlayOneShot (finishWank [Random.Range (0, finishWank.Length)]);
	}

	public void PlayDisappointedWank() {

		announcerPlayer.PlayOneShot (lastPlayerFinishWank [Random.Range (0, lastPlayerFinishWank.Length)]);
	}

}
