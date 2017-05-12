using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voxelload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static Block LoadVoxel(World world,Vector3 pos,Block block){
		Block bloco = world.SetBlock ((int)pos.x, (int)pos.y, (int)pos.z, block);
		if (bloco != null) {
			return bloco;
		} else {
			return null;
		}
	}
	public static Chunk CreateChunk(World world,Vector3 pos){
		Chunk chunk = world.CreateChunk ((int)pos.x * 16, (int)pos.y * 16, (int)pos.z * 16);
		return chunk;
	}
	public static List<Chunk> convertchunkstolist(World world){
		List<Chunk> lchunks = new List<Chunk> ();
		foreach(KeyValuePair<WorldPos,Chunk> chunks in world.chunks){
			Chunk chunk = null;
			world.chunks.TryGetValue(chunks.Key, out chunk);
			if (chunk != null) {
				lchunks.Add (chunk);
			}
		}
		return lchunks;
	}

	public static Chunk verifychunk(World world,int x,int y,int z){
		if (world == null) {
			Debug.LogError ("VoxelTerrain não encontrou o objeto World, não é possível verificar se existe um chunk antes de colocar o bloco");
		}
		Chunk checkifexist = world.GetChunk (x,y,z);
		if (checkifexist != null) {
			return checkifexist;
		} else {
			return null;
		}
	}
}
