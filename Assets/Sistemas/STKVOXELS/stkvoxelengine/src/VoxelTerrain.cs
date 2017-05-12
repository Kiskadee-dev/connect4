using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelTerrain {
	public static WorldPos GetBlockPos(Vector3 pos)
	{
		WorldPos blockPos = new WorldPos(
			Mathf.RoundToInt(pos.x),
			Mathf.RoundToInt(pos.y),
			Mathf.RoundToInt(pos.z)
		);

		return blockPos;
	}
	public static WorldPos GetBlockPos(RaycastHit hit, bool adjacent = false)
	{
		Vector3 pos = new Vector3(
			MoveWithinBlock(hit.point.x, hit.normal.x, adjacent),
			MoveWithinBlock(hit.point.y, hit.normal.y, adjacent),
			MoveWithinBlock(hit.point.z, hit.normal.z, adjacent)
		);

		return GetBlockPos(pos);
	}
	static float MoveWithinBlock(float pos, float norm, bool adjacent = false)
	{
		if (pos - (int)pos == 0.5f || pos - (int)pos == -0.5f)
		{
			if (adjacent)
			{
				pos += (norm / 2);
			}
			else
			{
				pos -= (norm / 2);
			}
		}

		return (float)pos;
	}



	public static Block SetBlock(RaycastHit hit, Block block, bool adjacent = false,Vector3 checkifchunkbounds = default(Vector3))
	{
		Chunk chunk = hit.collider.GetComponent<Chunk>();
		if (chunk == null)
			return null;
		/*
			if (chunk.gameObject.transform.position.z + checkifchunkbounds.z == 16) {
			Debug.Log ("Final do chunk atingido " + chunk.gameObject.transform.position.z + checkifchunkbounds.z.ToString ());
			Debug.Log (checkifchunkbounds.x.ToString ());
			return null;
		}
		Debug.Log (checkifchunkbounds.z.ToString ());
*/


		WorldPos pos = GetBlockPos(hit, adjacent);

		chunk.world.SetBlock(pos.x, pos.y, pos.z, block);

		return block;
	}
	public static WorldPos GetBP(RaycastHit hit, bool adjacent = false){
		WorldPos pos = GetBlockPos(hit, adjacent);
		return pos;
	}
	//verifique se a coordenada é correta antes de usar, caso contrário blocos podem substituir outros.







	public static void paintblock(RaycastHit hit, string newcolor){
		Chunk chunk = hit.collider.GetComponent<Chunk> ();
		if (chunk == null)
			return;
		Block block = GetBlock (hit);
		Debug.Log (block.blocktipe);
		chunk.paintblock (newcolor,block);
	}
	public static Block GetBlock(RaycastHit hit, bool adjacent = false)
	{
		Chunk chunk = hit.collider.GetComponent<Chunk>();
		if (chunk == null) {
//			Debug.Log ("NO CHUNK!");
			return null;
		}
		WorldPos pos = GetBlockPos(hit, adjacent);

		Block block = chunk.world.GetBlock(pos.x, pos.y, pos.z);


		return block;
	}
	public static bool GetValidPositiontoPlaceBlock(RaycastHit hit){
		{
			Chunk chunk = hit.collider.GetComponent<Chunk>();
			if (chunk == null) {
				Debug.Log ("NO CHUNK!");
				return false;
			}
			WorldPos pos = GetBlockPos(new Vector3(hit.transform.position.x - 1,hit.transform.position.y,hit.transform.position.z));
			Block block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

			pos = GetBlockPos(new Vector3(hit.transform.position.x + 1,hit.transform.position.y,hit.transform.position.z));
			 block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

				pos = GetBlockPos(new Vector3(hit.transform.position.x,hit.transform.position.y + 1,hit.transform.position.z));
			block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

				pos = GetBlockPos(new Vector3(hit.transform.position.x,hit.transform.position.y - 1,hit.transform.position.z));
			block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

				pos = GetBlockPos(new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z + 1));
			block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

				pos = GetBlockPos(new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z + 1));
			block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

			pos = GetBlockPos(hit,true);
			block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

			bool stuckonsomeone = false;
			if (block.IsSolid (Block.Direction.up) | block.IsSolid (Block.Direction.down) | block.IsSolid (Block.Direction.north) | block.IsSolid (Block.Direction.south) | block.IsSolid (Block.Direction.east) | block.IsSolid (Block.Direction.west)) {
				stuckonsomeone = true;
			}
				
			if (stuckonsomeone) {
				Debug.Log ("Stuck inside someone");
				return false;
			} else {
				return true;
			}

		}
	}
	public static Vector3 GetPrevPosBlock(RaycastHit hit, bool adjacent = false)
	{
		Chunk chunk = hit.collider.GetComponent<Chunk>();
		if (chunk == null)
			return new Vector3(0,0,0);

		WorldPos pos = GetBlockPos(hit, adjacent);



		return new Vector3(pos.x,pos.y,pos.z);
	}

}
