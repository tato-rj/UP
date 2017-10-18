using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRecord : MonoBehaviour {

	Text score;

	void Awake () {
		ScoreManager.instance.LoadData ();
		score = GetComponent<Text> ();
		score.text = ScoreManager.instance.score.ToString ();
	}
}
