using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

	public static BackgroundMusic instance;
	public AudioSource bgMusic;

	// Use this for initialization
	void Awake () {

		DontDestroyOnLoad (this);

		if (instance == null) {
			instance = this;
		} else {
			DestroyObject (gameObject);		
		}

		bgMusic = GetComponent<AudioSource> ();
	}

}
