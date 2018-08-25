using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip[] anthems;

	public AudioSource anthemPlayer;

	// Use this for initialization
	void Start () {
		
	}

	public void PlayAnthem(int value) {

		if (anthems [value] == null) {
			return;
		}

		anthemPlayer.PlayOneShot(anthems[value]);
	}

}
