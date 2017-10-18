using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour {
	
	public GameObject wall;

	void OnTriggerEnter2D (Collider2D other) {
		
		if (other.tag == "Wall trigger") {

			GameObject newWall;
			float yPos = GetComponent<BoxCollider2D> ().size.y + transform.position.y;
			float xPos = transform.position.x;

			newWall = (GameObject) Instantiate (
				wall, 
				new Vector3(xPos,yPos,0), 
				Quaternion.identity
			);

			newWall.transform.parent = transform.parent;
		
		}
	}
}
