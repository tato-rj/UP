using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour {

	public static AdsManager instance;

	public string bannerID;
	public string videoID;
	InterstitialAd interstitial;

	void Awake () {
		DontDestroyOnLoad (this);

		if (instance == null) {
			instance = this;
		} else {
			DestroyObject (gameObject);		
		}
	}

	void Start () {
		RequestBanner ();
	}

	void RequestBanner () {
		AdSize adSize = new AdSize (750,60);
		BannerView bannerView = new BannerView (bannerID, adSize, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder ().Build();
		bannerView.LoadAd (request);

	}

	public void RequestInterstitial () {

		interstitial = new InterstitialAd (videoID);
		AdRequest request = new AdRequest.Builder ().Build ();

		interstitial.LoadAd (request);

	}

	public void LoadInterstitital () {
		if (interstitial.IsLoaded()) {
			interstitial.Show ();
		}
	}

	public void DestroyVideo () {
		interstitial.Destroy ();
	}
}
