using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
	public World world;
	public int size;
	// Use this for initialization
	void Start () {
		Chunk schunk = voxelload.CreateChunk (world, new Vector3 (1, 1, 1));
		for (int x = -size; x < size; x++) {
			for (int z = -size; z < size; z++) {
				world.SetBlock ((int)(schunk.transform.position.x + x),(int)(schunk.transform.position.y),(int)(schunk.transform.position.z + z), new BlockNeutro());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
