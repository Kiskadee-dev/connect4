using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botacubo : MonoBehaviour {
	public GameObject local_voxelmanager;
	public GameObject worldprefab;
	public GameObject modify;




	World world;
	// Use this for initialization
	void Start () {
		StartCoroutine (poe ());
		StartCoroutine (poe2 ());
		StartCoroutine (poe3 ());

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public IEnumerator poe(){
		
		GameObject instance = (GameObject)Instantiate (worldprefab, this.transform.position, Quaternion.identity);
		world = instance.GetComponent<World> ();

		GameObject instance2 = (GameObject)Instantiate (local_voxelmanager, new Vector3 (0, 0, 0), Quaternion.identity);
		instance2.GetComponent<local_voxelmanager> ().containerWorld = world;

		instance.transform.SetParent (instance2.transform);

		instance = (GameObject)Instantiate (modify, new Vector3 (0, 0, 0), Quaternion.identity);
		instance.GetComponent<Modify> ().workingworld = instance2.GetComponent<local_voxelmanager>().containerWorld;

		Chunk workingchunk = voxelload.CreateChunk (world,new Vector3(0,0,0));
		Vector3 pos = new Vector3 (8, 9, 8);
		Block bloco = voxelload.LoadVoxel (world,pos, new BuildingBlock {color = 4.ToString()});



		if (bloco != null) {
			Debug.Log (bloco.blocktipe);
		} else {
			Debug.Log ("Sem bloco retornado");
		}
		yield return new WaitForSeconds (0);
	}
	public IEnumerator poe2(){

		GameObject instance = (GameObject)Instantiate (worldprefab, this.transform.position, Quaternion.identity);
		world = instance.GetComponent<World> ();

		GameObject instance2 = (GameObject)Instantiate (local_voxelmanager, new Vector3 (0, 0, 0), Quaternion.identity);
		instance2.GetComponent<local_voxelmanager> ().containerWorld = world;

		instance.transform.SetParent (instance2.transform);



		Chunk workingchunk = voxelload.CreateChunk (world,new Vector3(0,0,0));
		Vector3 pos = new Vector3 (8, 9, 8);
		Block bloco = voxelload.LoadVoxel (world,pos, new BuildingBlock {color = 4.ToString()});



		if (bloco != null) {
			Debug.Log (bloco.blocktipe);
		} else {
			Debug.Log ("Sem bloco retornado");
		}
		yield return new WaitForSeconds (0);
	}
	public IEnumerator poe3(){

		GameObject instance = (GameObject)Instantiate (worldprefab, this.transform.position, Quaternion.identity);
		world = instance.GetComponent<World> ();

		GameObject instance2 = (GameObject)Instantiate (local_voxelmanager, new Vector3 (0, 0, 0), Quaternion.identity);
		instance2.GetComponent<local_voxelmanager> ().containerWorld = world;

		instance.transform.SetParent (instance2.transform);



		Chunk workingchunk = voxelload.CreateChunk (world,new Vector3(0,0,0));
		Vector3 pos = new Vector3 (8, 9, 8);
		Block bloco = voxelload.LoadVoxel (world,pos, new BuildingBlock {color = 4.ToString()});



		if (bloco != null) {
			Debug.Log (bloco.blocktipe);
		} else {
			Debug.Log ("Sem bloco retornado");
		}
		yield return new WaitForSeconds (0);
	}
}
