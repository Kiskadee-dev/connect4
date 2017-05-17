using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void LoadConnect4Play2Players(){
		SceneManager.LoadScene (1);
	}
	public void LoadMainMenu(){
		SceneManager.LoadScene (0);
	}
	public void QuitGame(){
		//Application.Quit ();
		System.Diagnostics.Process.GetCurrentProcess().Kill();
	}
}
