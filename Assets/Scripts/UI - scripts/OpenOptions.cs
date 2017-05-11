using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour {
	public bool opened = false;
	public bool open = false;
	public GameObject OptionsMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!opened && open) {
			if (OptionsMenu != null) {
				OptionsMenu.SetActive (true);
				opened = true;
			}
		}
		if (opened && !open) {
			if (OptionsMenu != null) {
				OptionsMenu.SetActive (false);
				opened = false;
			}
		}
	}
	public void OpenClose(){
		open = !open;
	}
}
