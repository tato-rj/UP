using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSwipeTutorial : MonoBehaviour {

	public Camera cam;
	private float maxWidth;


	void Awake () {

		cam = Camera.main;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		maxWidth = targetWidth.x;
	}

	//Update the shape's position
	void FixedUpdate () {

		//If shape is not dead...
		if (tag != "Dead" && GetComponent<Rigidbody2D>().constraints > 0) {
			//...if there are no keys being pressed...
			if (Input.GetMouseButton(0)) {

				MoveShape ();
				//...if an arrow is bring pressed (condition checked inside MoveShape() function) move shape in that direction.
			} else {
				gameObject.transform.localScale = new Vector3 (1,1,1);
			}
		}
	}

	//-----------------------------------------------------
	//MOVE SHAPE (STICK TO CENTER WHEN NOT MOVING)
	//-----------------------------------------------------

	void MoveShape () {

		Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
		Vector3 targetPosition = new Vector3 (rawPosition.x, 0f, 0f);
		float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth + 0.7f, maxWidth - 0.9f);

		targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
		GetComponent<Rigidbody2D> ().MovePosition (targetPosition);

	}
}
