using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rate : MonoBehaviour {

	public void RateApp () {
		Application.OpenURL ("itms-apps://itunes.apple.com/app/"); 
	}

	public void ContactUs () {

		string email = "contact@leftlaneapps.com";
		string subject = MyEscapeURL ("Contact from UP");

		Application.OpenURL ("mailto:" + email + "?subject=" + subject);

	}

	string MyEscapeURL (string url) {

		return WWW.EscapeURL (url).Replace ("+", "%20");

	}
}
