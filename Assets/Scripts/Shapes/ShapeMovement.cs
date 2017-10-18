using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShapeMovement : MonoBehaviour {

	int comboCount = 0;
	Color32 shapeColor;
	int points;
	Animation afterBubblePop;
	GameObject bubble;

	void Start() {
		afterBubblePop = GetComponent<Animation> ();
		bubble = transform.Find ("Bubble").gameObject;
		shapeColor = GetComponentsInChildren<SpriteRenderer>()[1].color;
	}

	void OnMouseDown () {

		if (!bubble.activeSelf) {
			this.GetComponent<ShapeCentered> ().enabled = false;
			this.GetComponent<ShapeSwipe> ().enabled = true;
		} else {
			PopBubble ();
		}
	
	}

	void OnMouseUp () {

		ShapeManager.instance.shapeHoldTimer = 0;
		this.GetComponent<ShapeCentered> ().enabled = true;
		this.GetComponent<ShapeSwipe> ().enabled = false;

	}

	void OnTriggerEnter2D (Collider2D other) {
		
		//If shape is not dead and the game is on...
		if (tag != "Dead" && GetComponent<Rigidbody2D>().constraints > 0 && other.tag != "BonusDestroyer") {
			//Stops the growing sound that might be playing
			if (gameObject.GetComponents<AudioSource> () [1].isPlaying) {
				gameObject.GetComponents<AudioSource> () [1].Stop ();
			}
			//If hole is correct...
			if (other.tag == tag) {

				KillCousins ();
				SetWallColors (other);
				FitShapeInsideWall (other);
				GameController.instance.powerNumberText.GetComponent<Text> ().text = "+" + points;
				GameController.instance.powerNumberText.SetActive (true);
				Invoke ("HidePointsText", 0.8f);
				UIManagerScript.instance.IncrementScore (points);

				tag = "Dead";

			//If hole is NOT correct...
			} else if (other.tag != tag && other.tag != "Top wall") {

				GameController.instance.gameOn = false;
				ShakeCam.instance.ShakeCamera (0.04f, 0.4f);
				KillShapeGameOver ();
				tag = "Dead";

			}

		}
		//If hits the top wall...
		if (other.tag == "Top wall") {

			FloatShape ();
			bubble.SetActive(false);
		}
	}


	//-----------------------------------------------------
	// FUNCTIONS
	//-----------------------------------------------------

	void HidePointsText () {
		GameController.instance.powerNumberText.SetActive (false);
	}

	void PopBubble () {
		bubble.SetActive (false);
		afterBubblePop.Play ("afterBubblePop");
		ShapeManager.instance.bubbleSound.Play ();
	}

	void FloatShape () {

		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		GetComponent<Rigidbody2D> ().gravityScale = -2f;
		GetComponent<ShapeCentered> ().enabled = false;

	}

	void KillShapeGameOver () {

		//...plays sound for incorrect match...
		ShapeManager.instance.wrongShape.Play ();
		//...resets kinematic value...
		GetComponent<Rigidbody2D> ().isKinematic = false;
		//...unsets the constraints, so that the shape will move freely in any direction...
		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;

	}

	void FitShapeInsideWall (Collider2D other) {

		//...plays the animation...
		GetComponent<Animation> ().Play();
		//...blocks the shape's response to user's input...
		GetComponent<Rigidbody2D> ().isKinematic = true;
		//...removes the collider, so the shape will no longer hit new boundaries...
		//Destroy (GetComponent<Collider2D>());
		//...puts the shape inside the parent of the ("other") hole it hits...
		transform.parent = other.transform.Find("Locked shape position").transform;
		//...locks the shape's position to its new parent's position...
		transform.position = new Vector3 (
			transform.parent.transform.position.x,
			transform.parent.transform.position.y,
			0
		);
	}

	void SetWallColors (Collider2D wall) {

		Material wallMaterial = wall.transform.parent.GetComponent<SpriteRenderer> ().sharedMaterial;
		int index;

		points = (wallMaterial.color == shapeColor) ? 3 : 1; 
		wallMaterial.color = shapeColor;
		GameController.instance.cam.GetComponent<Animation> ().Play ();

		if (WallMovementLeft.instance.wallLeftMaterial.color == WallMovementRight.instance.wallRightMaterial.color) {

			if (wall.transform.parent.parent.tag == "Right") {
				WallMovementRight.instance.speed -= 1f;
			} else {
				WallMovementLeft.instance.speed -= 1f;
			}

			index = (comboCount <= 6) ? comboCount : 6;

			if (!GameController.instance.powerMode) {
				StartCoroutine (ShowPowerOverlay());	
			}
			ShapeManager.instance.shapeSpeed = 2.4f;
			ShapeManager.instance.shapeSpawnRate = 0.7f;
			GameController.instance.powerMode = true;
			//GameController.instance.rays.SetActive (true);
			GameController.instance.bonusMsg.GetComponent<Text> ().text = GetScreenMessage (index);
			comboCount++;
			points = 10;
			ShapeManager.instance.combo.Play ();
			ShapeManager.instance.correctShape.Play ();
			GameController.instance.bonusMsg.GetComponent<Animation> ().Play ();
		} else {
			ShapeManager.instance.shapeSpeed = 2f;
			ShapeManager.instance.shapeSpawnRate = 1.2f;
			//GameController.instance.rays.SetActive (false);
			GameController.instance.powerMode = false;
			comboCount = 0;
			GameController.instance.powerNumberText.SetActive (false);
			WallMovementLeft.instance.speed = -1.4f;
			WallMovementRight.instance.speed = -1.4f;
			ShapeManager.instance.correctShape.Play ();
		}
	}

	public IEnumerator ShowPowerOverlay () {
		GameController.instance.powerModeOverlay.SetActive (true);
		yield return new WaitForSeconds (1.2f);
		GameController.instance.powerModeOverlay.SetActive (false);
	}

	string GetScreenMessage (int index) {

		string[] messages = {"", "nice", "great", "excellent", "sweet", "amazing", "wow"};
		return messages[index];

	}

	void KillCousins () {
		
		//...gets all matching shapes that are floating on top of the screen...
		GameObject[] cousins = GameObject.FindGameObjectsWithTag (tag);
		//...and kills any floating shape of the same type...
		StartCoroutine(KillOneAtTime(cousins));
					
	}

	public IEnumerator KillOneAtTime (GameObject[] shapes) {

		GameObject[] cousins = shapes;

		foreach (GameObject cousin in cousins) {
			
			if (cousin != null && cousin.name.Contains("(Clone)")) {
				if (cousin.GetComponent<Rigidbody2D>().constraints == 0) {
					ShapeManager.instance.ExplodeShape (cousin);
					yield return new WaitForSeconds (0.2f);
				}
			}
		}
	}
}