using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShapeMovementTutorial : MonoBehaviour {

	public GameObject finger;
	public GameObject instructions;
	public GameObject wall;
	public GameObject overlay;

	void Start () {
		StartCoroutine (IntroSequence ());
	}

	void OnMouseDown () {

		this.GetComponent<ShapeCenteredTutorial> ().enabled = false;
		this.GetComponent<ShapeSwipeTutorial> ().enabled = true;
	}

	void OnMouseUp () {

		this.GetComponent<ShapeCenteredTutorial> ().enabled = true;
		this.GetComponent<ShapeSwipeTutorial> ().enabled = false;

	}

	void OnTriggerEnter2D (Collider2D other) {
		FitShapeInsideWall (other);
		GetComponents<AudioSource> ()[1].Play ();
		tag = "Dead";

	}

	public IEnumerator IntroSequence () {
		yield return new WaitForSeconds (0.5f);
		GetComponents<AudioSource> ()[0].Play ();
		GetComponent<Animation>().Play("shape_popin");
	
		yield return new WaitForSeconds (1.5f);
		wall.SetActive (true);
		wall.GetComponent<Animation> ().Play ("tutorialWallIn");

		instructions.SetActive (true);
		finger.SetActive (true);	

	}

	void FitShapeInsideWall (Collider2D other) {

		//...blocks the shape's response to user's input...
		GetComponent<Rigidbody2D> ().isKinematic = true;

		finger.SetActive (false);
		instructions.SetActive (false);
		//...plays the animation...
		GetComponent<Animation> ().Play();
		//Destroy (GetComponent<Collider2D>());
		//...puts the shape inside the parent of the ("other") hole it hits...
		transform.parent = other.transform.Find("Locked shape position");
		//...locks the shape's position to its new parent's position...
		transform.position = new Vector3 (
			transform.parent.position.x,
			transform.parent.position.y,
			0
		);
		StartCoroutine (EndOfTutorial ());
	}

	public IEnumerator EndOfTutorial () {

		yield return new WaitForSeconds (0.5f);
		overlay.SetActive (true);
		yield return new WaitForSeconds (0.8f);
		SceneManager.LoadScene ("intro");
	}
		
}
