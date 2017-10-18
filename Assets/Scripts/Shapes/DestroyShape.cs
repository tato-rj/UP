using UnityEngine;
using System.Collections;

public class DestroyShape : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Dead") {
			Destroy	(other.gameObject);

		}

		if (other.tag == "Dead" && !other.GetComponent<Rigidbody2D> ().isKinematic) {
			StartCoroutine (GameController.instance.GameOver ());
		}

	}
}
