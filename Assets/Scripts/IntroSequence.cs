using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour {

	public GameObject[] buttons;

	void Start () {
		StartCoroutine (AnimateButtons ());
	}

	public IEnumerator AnimateButtons() {

		foreach (GameObject button in buttons) {
			yield return new WaitForSeconds (0.1f);
			button.SetActive (true);
		}

	}
}
