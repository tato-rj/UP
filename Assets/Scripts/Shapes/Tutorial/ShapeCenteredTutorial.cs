using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCenteredTutorial : MonoBehaviour {

	public float shapePosition;

	//Update the shape's position
	void Update () {

		//If shape is not dead, keep it centered...
		if (tag != "Dead" && GetComponent<Rigidbody2D>().constraints > 0) {
			CenterShape (shapePosition);
		}

	}

	void CenterShape (float yPos) {
		//Stick shape to center
		transform.position = Vector3.Lerp (
			transform.position, 
			new Vector3 (0, yPos, 0), 
			1f
		);

		if (gameObject.GetComponents<AudioSource> () [1].isPlaying) {

			gameObject.GetComponents<AudioSource> () [1].Stop ();

		}
	}
}
