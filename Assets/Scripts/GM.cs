using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
	[Header("Scripts necessários")]
	public World world;
	[Header("Instâncias")]
	public Chunk WorkChunk;
	[Header("Opções")]
	public int size;
	// Use this for initialization
	void Start () {
		CriarTabuleiro ();
	}
	
	public void CriarTabuleiro(){
		WorkChunk = voxelload.CreateChunk (world, new Vector3 (1, 1, 1));
		for (int x = -size; x < size; x++) {
			for (int z = -size; z < size; z++) {
				world.SetBlock ((int)(WorkChunk.transform.position.x + x),(int)(WorkChunk.transform.position.y),(int)(WorkChunk.transform.position.z + z), new BlockNeutro());
			}
		}
	}
	public void RecriarTabuleiro(){
		Destroy (WorkChunk.gameObject);
		world.chunks.Clear ();
		CriarTabuleiro ();
	}
}
