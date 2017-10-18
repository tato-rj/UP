using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadRestart : MonoBehaviour {

	public static LoadRestart instance;

	public GameObject scoreBox;
	public GameObject ratingBox;
	public bool pause = false;
	private AudioSource buttonSound;

	void Awake () {

		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}

		buttonSound = GetComponent<AudioSource> ();

	}
		
	public void StartGame () {

		Scene currentScene = SceneManager.GetActiveScene ();

		buttonSound.Play ();
		SceneManager.LoadScene ("game");
		if (currentScene.name == "game") {
			AdsManager.instance.LoadInterstitital ();
		}
		Time.timeScale = 1;
	}

	public void AskForRating () {

		if (GameController.instance.isNewRecord && ScoreManager.instance.LoadRating () == -1) {
			scoreBox.SetActive (false);
			ratingBox.SetActive (true);	
		} else {			
			StartGame ();
		}

	}

	public void ResetData() {
		ScoreManager.instance.DeleteData();
		SceneManager.LoadScene ("intro");
	}

	public void ShowTutorial () {
		buttonSound.Play ();
		SceneManager.LoadScene ("tutorial");
	}

	public void ShowRecord () {
		buttonSound.Play ();
		SceneManager.LoadScene ("record");
		// Unpause the game
		Time.timeScale = 1;
	}

	public void RateApp () {
		buttonSound.Play ();
		SceneManager.LoadScene ("rate");
		// Unpause the game
		Time.timeScale = 1;
	}

	public void BackToHomeScreen () {
		if (!BackgroundMusic.instance.bgMusic.isPlaying) {
			BackgroundMusic.instance.bgMusic.Play ();
		}
		buttonSound.Play ();
		SceneManager.LoadScene ("intro");
		// Unpause the game
		Time.timeScale = 1;
	}

	public void Info () {
		buttonSound.Play ();
		Application.OpenURL ("http://www.leftlaneapps.com");
	}
		
	public void PauseGame() {
		buttonSound.Play ();
		pause = !pause;

		if (pause) {

			GameController.instance.pauseScreen.SetActive (true);
			Time.timeScale = 0;

		} else {
			GameController.instance.pauseScreen.SetActive (false);
			Time.timeScale = 1;

		}
	}
		
}

