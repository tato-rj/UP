using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

	public static UIManagerScript instance;

	public Material wallMaterial;
	public Material leftWallMaterial;
	public Text recordText;
	public Text scoreText;
	public GameObject scoreButton;

	private int score;
	public float increment;
	//private Color32 darkColor;
	//private float maxShapeSpeed;

	void Awake () {

		instance = this;

		//Get random new colors
		Color32 rightWallColor = updateBackgroundColor ();
		Color32 leftWallColor = updateBackgroundColor ();

			//If colors for both wall are the same, continue to look a different one
			while (rightWallColor.Equals(leftWallColor)) {
				leftWallColor = updateBackgroundColor ();
			}

		//Set colors for walls
		leftWallMaterial.color = leftWallColor;
		wallMaterial.color = rightWallColor;

		//Sets initial score and increment to zero and loads user record
		score = 0;
		increment = 0;

	}

	//-----------------------------------------------------
	//MANAGE SCORE
	//-----------------------------------------------------

	public void SetScoreDisplay () {
		scoreText.text = "0";
		recordText.text = ScoreManager.instance.score.ToString ();
	}

	public void IncrementScore (int hits) {

		int num = hits;
		score += num;
		increment ++;
		if (!scoreButton.GetComponent<Animation>().isPlaying) {
			scoreButton.GetComponent<Animation>().Play();	
		}
		scoreText.text = score.ToString ();
		//StartCoroutine (AnimatePoints(num));
		//DisplayPoints (num);

		if (score >= ScoreManager.instance.score) {
			recordText.text = score.ToString();	
		}
	}

	public int DisplayScore() {
		return score;
	}

	public Color32 updateBackgroundColor() {

		//Background
		Color32 orange = new Color32 (255, 151, 0, 255);
		Color32 red = new Color32 (239, 68, 55, 255);
		Color32 violet = new Color32 (143, 62, 151, 255);
		Color32 blue = new Color32 (3, 168, 244, 255);
		Color32 green = new Color32 (75, 175, 80, 255);

		Color32[] backgroundColors = {orange, red, violet, blue, green};

		int randomIndex = Random.Range(0, 5);

		return backgroundColors[randomIndex];

	}


}
