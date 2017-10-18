using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public Camera cam;

	public Transform shapesContainer;
	public GameObject gameOver;
	public GameObject score;
	public GameObject zeroScoreBox;
	public GameObject regularScoreBox;
	public GameObject bestScoreBox;
	public GameObject pauseScreen;
	public GameObject bonusMsg;
	public GameObject powerModeOverlay;
	public bool gameOn;
	public GameObject rays;
	public GameObject powerNumberText;
	public GameObject pointsPrefab;
	public bool powerMode;
	public GameObject confetti;
	public GameObject ribbons1;
	public GameObject ribbons2;
	public bool isNewRecord;
	int counter = 0;

	public Coroutine shapes = null;

	void Awake () {

		instance = this;
		powerMode = false;
		ScoreManager.instance.LoadData ();
	}

	void Start () {

		//Load video AD to be shown at the end of the game
		AdsManager.instance.RequestInterstitial();

		BackgroundMusic.instance.bgMusic.Stop ();
		StartGame();

	}

	//-----------------------------------------------------
	//START GAME
	//-----------------------------------------------------

	public void StartGame () {
		ScoreManager.instance.LoadData ();
		UIManagerScript.instance.SetScoreDisplay ();
		gameOn = true;
		shapes = StartCoroutine (Shapes());

	}

	public IEnumerator Shapes () {
		yield return new WaitForSeconds (1f);
		while (gameOn) {
			ShapeManager.instance.InstantiateShape ();
			yield return new WaitForSeconds (ShapeManager.instance.shapeSpawnRate);
		}
	}
		
	//-----------------------------------------------------
	//END GAME
	//-----------------------------------------------------

	public IEnumerator GameOver () {
		
		yield return new WaitForSeconds (0.5f);

		int record = ScoreManager.instance.score;
		int currentScore = UIManagerScript.instance.DisplayScore ();

		//Set up screen for final score
		powerMode = false;
		gameOver.SetActive(true);
		gameOver.transform.GetChild (0).gameObject.SetActive (true);
		WallMovementLeft.instance.speed = -1f;
		WallMovementRight.instance.speed = -1f;

		//If score was zero and it is the first time playing the game
		if (record == 0 && currentScore == 0) {

			print ("Game over");
			print ("First time player!");
			print ("SCORE: " + currentScore);
			print ("RECORD: " + record);

			isNewRecord = false;
			zeroScoreBox.SetActive(true);
			regularScoreBox.SetActive (false);
			bestScoreBox.SetActive (false);
			ShapeManager.instance.noPoints.Play ();

			//If score is a new record
		} else if (record < currentScore) {

			print ("Game over");
			print ("New record!");
			print ("SCORE: " + currentScore);

			bestScoreBox.GetComponentInChildren<Text> ().text = currentScore.ToString();

			isNewRecord = true;
			ScoreManager.instance.SaveData (currentScore);
			zeroScoreBox.SetActive(false);
			regularScoreBox.SetActive (false);
			bestScoreBox.SetActive (true);
			CountUpScore();

			//If score is NOT a new record
		} else {

			print ("Game over");
			print ("Your score: " + currentScore);
			print ("RECORD: " + record);

			isNewRecord = false;
			ShapeManager.instance.noPoints.Play ();
			zeroScoreBox.SetActive(false);
			regularScoreBox.SetActive (true);
			bestScoreBox.SetActive (false);
			regularScoreBox.transform.GetChild(0).GetComponent<Text>().text = "Best score: " + record;
			regularScoreBox.transform.GetChild(1).GetComponent<Text>().text = currentScore.ToString();
		}
			
	}

	public void CountUpScore () {

		ShapeManager.instance.cymbalCrash.Play ();
		confetti.SetActive (true);
		ribbons1.SetActive (true);
		ribbons2.SetActive (true);
		bestScoreBox.transform.GetChild (1).GetComponent<Animation> ().Play ();
		BackgroundMusic.instance.bgMusic.Play ();
	}

	public IEnumerator KillGame () {

		gameOn = false;
		yield return new WaitForSeconds (2f);

		foreach (Transform child in shapesContainer) {
			ShapeManager.instance.ExplodeShape (child.gameObject);
		}

		yield return new WaitForSeconds (2f);

		StartCoroutine (GameOver ());

	}
}
