using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public bool Team = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray,out hit)){
				Block bloco = VoxelTerrain.GetBlock (hit);
				if (bloco != null) {
					if (Team) {
						if (bloco.blocktipe == "Neutro") {
							VoxelTerrain.SetBlock (hit, new BlockAzul ());
						}
					} else {
						if (bloco.blocktipe == "Neutro") {
							VoxelTerrain.SetBlock (hit, new BlockVermelho ());
						}
					}
				}
			}
		}
	}
}
