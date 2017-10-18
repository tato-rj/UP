using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSkyColor : MonoBehaviour {

	Image bg;
	Color32 color1;
	Color32 color2;

	void Start () {
		color2 = new Color32 (255, 255, 255, 255);
		bg = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		
		if (GameController.instance.powerMode) {
			Color32 color1 = new Color32 (250, 255, 208, 255);//WallMovementLeft.instance.wallLeftMaterial.color;
			//color1.a = 60;

			bg.color = Color.Lerp (color1, color2, Mathf.PingPong (Time.time * 8, 1));
		} else {
			bg.color = Color.white;
		}

	}
}
