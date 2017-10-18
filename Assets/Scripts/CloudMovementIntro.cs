using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementIntro : MonoBehaviour {

	float xSpeed;
	float ySpeed = 12f;
	float dist;
	// Use this for initialization
	void Awake () {

		xSpeed = Random.Range (5f, 20f);
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (transform.position.x < 335f || transform.position.x > 415f) {
			xSpeed = -xSpeed;
		}

		if (transform.position.y < 192f || transform.position.y > 200f) {
			ySpeed = -ySpeed;
		}

		transform.Translate (-xSpeed * Time.deltaTime, -ySpeed * Time.deltaTime ,0);
	}
}
