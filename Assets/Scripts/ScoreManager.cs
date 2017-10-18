using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;
	public int score;
	public bool resetData;
	public bool showTutorial;

	void Awake () {
		if (resetData) {
			DeleteData ();	
		}

		DontDestroyOnLoad (this);
		Screen.orientation = ScreenOrientation.Portrait;

		if (instance == null) {
			instance = this;
		} else {
			DestroyObject (gameObject);		
		}

		LoadData ();
		if (!showTutorial) {
			SceneManager.LoadScene ("intro");
		} else {
			SceneManager.LoadScene ("tutorial");
		}

	}

	/******************
	 * 
	 * 
	 * 	RATING
	 * 
	 * 
	 ******************/ 

	public void SaveRating (int num) {

		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/upRating.dat");
		UPRating data = new UPRating ();

		//Save score to data.score
		data.rating = num;
		print ("Rating saved!");
		binaryFormatter.Serialize (file, data);
		file.Close ();

	}

	public int LoadRating () {

		if (File.Exists (Application.persistentDataPath + "/upRating.dat")) {			

			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/upRating.dat", FileMode.Open);
			UPRating data = (UPRating)binaryFormatter.Deserialize (file);
			file.Close ();

			return data.rating;

		} else {
			print ("You have not yet rated this app.");
			return -1;
		}
	}

	/******************
	 * 
	 * 
	 * 	SCORE
	 * 
	 * 
	 ******************/ 

	public void SaveData (int num) {

		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/upScore.dat");
		UPScore data = new UPScore ();

		//Save score to data.score
		data.score = num;

		binaryFormatter.Serialize (file, data);
		file.Close ();

	}

	public void LoadData () {

		if (File.Exists (Application.persistentDataPath + "/upScore.dat")) {			

			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/upScore.dat", FileMode.Open);
			UPScore data = (UPScore)binaryFormatter.Deserialize (file);
			file.Close ();

			//Load data.score to score
			score = data.score;
			print ("Welcome back!");
			print ("Your current record is: " + score);
			showTutorial = false;

		} else {
			print ("You have no records set.");

			//Sets score to 0
			score = 0;
			showTutorial = true;

		}
	}

	public void DeleteData () {
		File.Delete (Application.persistentDataPath + "/upScore.dat");
		File.Delete (Application.persistentDataPath + "/upRating.dat");
	}
}

[Serializable]
class UPScore {

	public int score;

}

[Serializable]
class UPRating {

	public int rating;

}