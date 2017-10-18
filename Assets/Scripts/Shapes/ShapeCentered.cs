using UnityEngine;
using System.Collections;

public class ShapeCentered : MonoBehaviour {

	public float shapePosition;

	//Update the shape's position
	void Update () {

		//Shape speed
		shapePosition += Time.deltaTime*ShapeManager.instance.shapeSpeed;

		//If shape is not dead, keep it centered...
		if (tag != "Dead" && GetComponent<Rigidbody2D>().constraints > 0) {
			CenterShape (shapePosition);
		}

	}

	void CenterShape (float yPos) {
		//Stick shape to center
		transform.position = Vector3.Lerp (
			transform.position, 
			new Vector3 (0, 0.8f+yPos, 0), 
			1f
		);

		if (gameObject.GetComponents<AudioSource> () [1].isPlaying) {
			
			gameObject.GetComponents<AudioSource> () [1].Stop ();

		}
	}	
}
