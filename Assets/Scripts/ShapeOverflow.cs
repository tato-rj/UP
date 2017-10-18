using UnityEngine;
using System.Collections;

public class ShapeOverflow : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {

		if (other.GetComponent<Rigidbody2D>().gravityScale < 0) {

			print ("END GAME!");
			StartCoroutine (GameController.instance.KillGame());

		}
	
	}
}
