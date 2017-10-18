﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLeftWall : MonoBehaviour {

	public GameObject wall;

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Left") {

			GameObject newWall;
			float yPos = other.GetComponent<BoxCollider2D> ().size.y + transform.position.y;
			float xPos = other.transform.position.x;

			newWall = (GameObject) Instantiate (
				wall, 
				new Vector3(xPos,yPos,0), 
				Quaternion.identity
			);

			newWall.transform.parent = other.transform.parent;

		}
	}
}
