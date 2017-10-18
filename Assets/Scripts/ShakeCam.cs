using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour {

	public static ShakeCam instance;

	float shakeTimer;
	float shakeAmount;
	Vector3 camPos;

	void Awake () {
		instance = this;
		camPos = transform.position;
	}

	// Update is called once per frame
	void Update () {

		if (shakeTimer >= 0) {
		
			Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

			transform.position = new Vector3 (
				transform.position.x + shakePos.x, 
				transform.position.y + shakePos.y,
				transform.position.z);
			
			shakeTimer -= Time.deltaTime;

		} else {

			transform.position = camPos;
		}

	}

	public void ShakeCamera (float shakePwr, float shakeDur) {
		shakeAmount = shakePwr;
		shakeTimer = shakeDur;
	}
}
