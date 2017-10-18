using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

	float xSpeed;
	float ySpeed;
	float initialXpos;
	float initialYpos;
	float limitX;
	float limitY;

	void Awake () {
		initialYpos = transform.position.y;
		initialXpos = transform.position.x;
		xSpeed = Random.Range (0.08f, 0.2f);
		ySpeed = Random.Range (0.06f, 0.09f);
		limitX = Random.Range (1f, 1.6f);
		limitY = Random.Range (0.03f, 0.06f);
	}
		
	void FixedUpdate () {

		if (transform.position.x < (initialXpos - limitX) || transform.position.x > (initialXpos + limitX)) {
			xSpeed = -xSpeed;
		}

		if (transform.position.y < (initialYpos - limitY) || transform.position.y > (initialYpos + limitY)) {
			ySpeed = -ySpeed;
		}

		transform.Translate (-xSpeed * Time.deltaTime, -ySpeed * Time.deltaTime ,0);
	}
}
