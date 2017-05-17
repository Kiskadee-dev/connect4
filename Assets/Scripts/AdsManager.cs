using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
	public bool showed;
	// Use this for initialization
	void Start () {
		StarterAd ();
	}

	void StarterAd(){
		if (Advertisement.IsReady()) {
			if (PlayerPrefs.HasKey ("FirstRun")) {
				int run = PlayerPrefs.GetInt ("FirstRun");
				if (run == 1) {
					ShowAd ();
				}
				showed = true;
			} else {
				PlayerPrefs.SetInt ("FirstRun", 1);
				showed = true;
			}
		} else {
			tryuntilready ();
		}
	}

	void tryuntilready(){
		if (!showed) {
			Debug.Log ("Not shown, trying again");
			StarterAd ();
		}
	}

	public void ShowAd()
	{
		Debug.Log ("Showing Ads");
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

}
