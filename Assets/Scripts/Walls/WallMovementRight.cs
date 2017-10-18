using UnityEngine;
using System.Collections;

public class WallMovementRight : MonoBehaviour {

	public static WallMovementRight instance;

	public Material wallRightMaterial;
	public Camera cam;
	public float speed;
	Vector3 pos;
	Vector3 screenDimensions;

	void Awake () {

		instance = this;
		speed = -1.4f;
		screenDimensions = cam.ScreenToWorldPoint (new Vector3(Screen.width,-4.4f,0));
		pos = transform.position;
		pos.x = screenDimensions.x-0.18f;

	}

	void Update () {
		
		//speed -= (UIManagerScript.instance.DisplayIncrement()/Random.Range(1f,10f));
		pos.y += speed * Time.deltaTime;
		transform.position = pos;

	}
}
