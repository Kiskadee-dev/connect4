using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calcrotationtest : MonoBehaviour {
	public Vector3 vertpos = new Vector3(4,0.0f,3);
	public Quaternion rotTo = new Quaternion(90,90,90,0);
	// Use this for initialization
	void Start () {
		  vertpos = new Vector3(0.5f,0.5f,0.5f);
		  rotTo = new Quaternion(90,90,90,0);

		vertpos = new Vector3(rotTo.x*(vertpos.x + (vertpos.x+0.5f)) - 0.0f,rotTo.y*(vertpos.y + 0.0f) - 0.0f,rotTo.z*(vertpos.z + (vertpos.z+0.5f)) - 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
