using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nBlockMetade : Block {

	public nBlockMetade()
		:base(){
		useocclusion = false;
		ignorethisblockforocclusion = true;
		blocktipe = "metcube";
		canchangecolor = true;
	}
	public override bool IsSolid(Block.Direction direction)
	{
		/*
		
		switch (direction) {
		case(Direction.west):
			canplacewest = true;
			return false;
		case(Direction.down):
			canplacedown = true;
			return true;
		case(Direction.up):
			return true;
		case(Direction.north):
			return true;
		case(Direction.south):
			return true;
		case(Direction.east):
			return false;
		}
		canplacenorth = false;
		canplacesouth = false;
		canplaceeast = false;
		canplaceup = false;
*/
		switch (direction) {
		case(Direction.down):
			return true;
		}
		return false;
	}

	protected override MeshData FaceDataUp (Chunk chunk, int x, int y, int z, MeshData meshData)
	{
		Vector3 vert1 = new Vector3 (x - 0.5f, y + 0.5f, z + 0.5f);
		Vector3 vert2 = new Vector3 (x - 0.5f, y + 0.5f, z + 0.5f);
		Vector3 vert3 = new Vector3 (x - 0.5f, y + 0.5f, z - 0.5f);
		Vector3 vert4 = new Vector3 (x - 0.5f, y + 0.5f, z - 0.5f);

		Vector3 centro = new Vector3 (x, y, z);

		List<Vector3> tverts = new List<Vector3> ();

		tverts.Add (vert1);
		tverts.Add (vert2);
		tverts.Add (vert3);
		tverts.Add (vert4);


		for (int n = 0; n < tverts.Count; n++) {
			tverts [n] = quatcasorodarbloco * (tverts [n] - centro) + centro;
		}


		meshData.AddVertex(vert1);
		meshData.AddVertex(vert2);
		meshData.AddVertex(vert3);
		meshData.AddVertex(vert4);

		meshData.AddQuadTriangles();
		meshData.uv.AddRange(FaceUVs(Direction.up));
		return meshData;
	}
	protected override MeshData FaceDataDown (Chunk chunk, int x, int y, int z, MeshData meshData)
	{
		List<Vector3> tverts = new List<Vector3> ();
		Vector3 x1 = new Vector3 (x-0.5f,y-0.5f,z-0.5f);
		Vector3 x2 = new Vector3 (x+0.5f,y-0.5f,z-0.5f);
		Vector3 x3 = new Vector3 (x+0.5f,y-0.5f,z+0.5f);
		Vector3 x4 = new Vector3 (x-0.5f,y-0.5f,z+0.5f);

		/*
		Vector3 vert1 = new Vector3 (x - 0.5f, y - 0.5f, z - 0.5f);
		Vector3 vert2 = new Vector3 (x + 0.5f, y - 0.5f, z - 0.5f);
		Vector3 vert3 = new Vector3 (x + 0.5f, y - 0.5f, z + 0.5f);
		Vector3 vert4 = new Vector3 (x - 0.5f, y - 0.5f, z + 0.5f);
*/



		Vector3 centro = new Vector3 (x, y, z);


		//tverts.Add (vert1);
		//tverts.Add (vert2);
		//tverts.Add (vert3);
		//tverts.Add (vert4);



		x1 = quatcasorodarbloco * (x1 - centro) + centro;
		x2 = quatcasorodarbloco * (x2 - centro) + centro;
		x3 = quatcasorodarbloco * (x3 - centro) + centro;
		x4 = quatcasorodarbloco * (x4 - centro) + centro;
		Vector3 vert1 = new Vector3 (x1.x,x1.y,x1.z);
		Vector3 vert2 = new Vector3 (x2.x,x2.y,x2.z);
		Vector3 vert3 = new Vector3 (x3.x,x3.y,x3.z);
		Vector3 vert4 = new Vector3 (x4.x,x4.y,x4.z);


		/*
		for (int n = 0; n < tverts.Count; n++) {
			tverts [n] = quatcasorodarbloco * (tverts [n] - centro) + centro;
		}*/
		tverts.Add (vert1);
		tverts.Add (vert2);
		tverts.Add (vert3);
		tverts.Add (vert4);

		meshData.AddVertex(vert1);
		meshData.AddVertex(vert2);
		meshData.AddVertex(vert3);
		meshData.AddVertex(vert4);

		meshData.AddQuadTriangles();
		meshData.uv.AddRange(FaceUVs(Direction.down));
		return meshData;
	}
	protected override MeshData FaceDataEast (Chunk chunk, int x, int y, int z, MeshData meshData)
	{
		Vector3 x1 = new Vector3 (x + 0.5f, y - 0.5f, z - 0.5f);
		Vector3 x2 = new Vector3 (x - 0.5f, y + 0.5f, z - 0.5f);
		Vector3 x3 = new Vector3 (x - 0.5f, y + 0.5f, z + 0.5f);
		Vector3 x4 = new Vector3 (x + 0.5f, y - 0.5f, z + 0.5f);

		Vector3 centro = new Vector3 (x, y, z);
		x1 = quatcasorodarbloco * (x1 - centro) + centro;
		x2 = quatcasorodarbloco * (x2 - centro) + centro;
		x3 = quatcasorodarbloco * (x3 - centro) + centro;
		x4 = quatcasorodarbloco * (x4 - centro) + centro;
		Vector3 vert1 = new Vector3 (x1.x,x1.y,x1.z);
		Vector3 vert2 = new Vector3 (x2.x,x2.y,x2.z);
		Vector3 vert3 = new Vector3 (x3.x,x3.y,x3.z);
		Vector3 vert4 = new Vector3 (x4.x,x4.y,x4.z);

		List<Vector3> tverts = new List<Vector3> ();

		/*
		for (int n = 0; n < tverts.Count; n++) {
			tverts [n] = quatcasorodarbloco * (tverts [n] - centro) + centro;
		}*/
		tverts.Add (vert1);
		tverts.Add (vert2);
		tverts.Add (vert3);
		tverts.Add (vert4);

		meshData.AddVertex(vert1);
		meshData.AddVertex(vert2);
		meshData.AddVertex(vert3);
		meshData.AddVertex(vert4);

		meshData.AddQuadTriangles();
		meshData.uv.AddRange(FaceUVs(Direction.down));
		return meshData;
	}
	protected override MeshData FaceDataWest (Chunk chunk, int x, int y, int z, MeshData meshData)
	{
		Vector3 x1 = new Vector3 (x - 0.5f, y - 0.5f, z + 0.5f);
		Vector3 x2 = new Vector3 (x - 0.5f, y + 0.5f, z + 0.5f);
		Vector3 x3 = new Vector3 (x - 0.5f, y + 0.5f, z - 0.5f);
		Vector3 x4 = new Vector3 (x - 0.5f, y - 0.5f, z - 0.5f);

		Vector3 centro = new Vector3 (x, y, z);
		x1 = quatcasorodarbloco * (x1 - centro) + centro;
		x2 = quatcasorodarbloco * (x2 - centro) + centro;
		x3 = quatcasorodarbloco * (x3 - centro) + centro;
		x4 = quatcasorodarbloco * (x4 - centro) + centro;
		Vector3 vert1 = new Vector3 (x1.x,x1.y,x1.z);
		Vector3 vert2 = new Vector3 (x2.x,x2.y,x2.z);
		Vector3 vert3 = new Vector3 (x3.x,x3.y,x3.z);
		Vector3 vert4 = new Vector3 (x4.x,x4.y,x4.z);

//		Debug.Log (new Vector3(x - x1.x,y - x1.y,z - x1.z).ToString());
		List<Vector3> tverts = new List<Vector3> ();


		/*
		for (int n = 0; n < tverts.Count; n++) {
			tverts [n] = quatcasorodarbloco * (tverts [n] - centro) + centro;
		}*/
		tverts.Add (vert1);
		tverts.Add (vert2);
		tverts.Add (vert3);
		tverts.Add (vert4);

		meshData.AddVertex(vert1);
		meshData.AddVertex(vert2);
		meshData.AddVertex(vert3);
		meshData.AddVertex(vert4);

		meshData.AddQuadTriangles();
		meshData.uv.AddRange(FaceUVs(Direction.down));


		if ( x - x1.x > 0 && y - x1.y > 0 && z - x1.z < 0) {
			
			return meshData;
		} else {
			if (x - x1.x < 0 && y - x1.y > 0 && z - x1.z < 0) {
				
				return meshData;

			} else {
				if (x - x1.x > 0 && y - x1.y > 0 && z - x1.z > 0) {
					
					return meshData;

				} else {
					if (x - x1.x < 0 && y - x1.y > 0 && z - x1.z > 0) {
						
						return meshData;

					}


				}
			}


			return meshData;





		}
	}
	protected override MeshData FaceDataNorth (Chunk chunk, int x, int y, int z, MeshData meshData)
	{

		Vector3 x1 = new Vector3 (x + 0.5f, y - 0.5f, z + 0.5f);
		Vector3 x2 = new Vector3 (x - 0.5f, y + 0.5f, z + 0.5f);
		Vector3 x3 = new Vector3 (x - 0.5f, y + 0.5f, z + 0.5f);
		Vector3 x4 = new Vector3 (x - 0.5f, y - 0.5f, z + 0.5f);

		Vector3 centro = new Vector3 (x, y, z);
		x1 = quatcasorodarbloco * (x1 - centro) + centro;
		x2 = quatcasorodarbloco * (x2 - centro) + centro;
		x3 = quatcasorodarbloco * (x3 - centro) + centro;
		x4 = quatcasorodarbloco * (x4 - centro) + centro;
		Vector3 vert1 = new Vector3 (x1.x,x1.y,x1.z);
		Vector3 vert2 = new Vector3 (x2.x,x2.y,x2.z);
		Vector3 vert3 = new Vector3 (x3.x,x3.y,x3.z);
		Vector3 vert4 = new Vector3 (x4.x,x4.y,x4.z);

		List<Vector3> tverts = new List<Vector3> ();

		/*
		for (int n = 0; n < tverts.Count; n++) {
			tverts [n] = quatcasorodarbloco * (tverts [n] - centro) + centro;
		}*/
		tverts.Add (vert1);
		tverts.Add (vert2);
		tverts.Add (vert3);
		tverts.Add (vert4);

		meshData.AddVertex(vert1);
		meshData.AddVertex(vert2);
		meshData.AddVertex(vert3);
		meshData.AddVertex(vert4);

		meshData.AddQuadTriangles();
		meshData.uv.AddRange(FaceUVs(Direction.down));
		return meshData;
	}
	protected override MeshData FaceDataSouth (Chunk chunk, int x, int y, int z, MeshData meshData)
	{

		Vector3 x1 = new Vector3 (x - 0.5f, y - 0.5f, z - 0.5f);
		Vector3 x2 = new Vector3 (x - 0.5f, y + 0.5f, z - 0.5f);
		Vector3 x3 = new Vector3 (x - 0.5f, y + 0.5f, z - 0.5f);
		Vector3 x4 = new Vector3 (x + 0.5f, y - 0.5f, z - 0.5f);

		Vector3 centro = new Vector3 (x, y, z);
		x1 = quatcasorodarbloco * (x1 - centro) + centro;
		x2 = quatcasorodarbloco * (x2 - centro) + centro;
		x3 = quatcasorodarbloco * (x3 - centro) + centro;
		x4 = quatcasorodarbloco * (x4 - centro) + centro;
		Vector3 vert1 = new Vector3 (x1.x,x1.y,x1.z);
		Vector3 vert2 = new Vector3 (x2.x,x2.y,x2.z);
		Vector3 vert3 = new Vector3 (x3.x,x3.y,x3.z);
		Vector3 vert4 = new Vector3 (x4.x,x4.y,x4.z);

		List<Vector3> tverts = new List<Vector3> ();

		/*
		for (int n = 0; n < tverts.Count; n++) {
			tverts [n] = quatcasorodarbloco * (tverts [n] - centro) + centro;
		}*/
		tverts.Add (vert1);
		tverts.Add (vert2);
		tverts.Add (vert3);
		tverts.Add (vert4);

		meshData.AddVertex(vert1);
		meshData.AddVertex(vert2);
		meshData.AddVertex(vert3);
		meshData.AddVertex(vert4);

		meshData.AddQuadTriangles();
		meshData.uv.AddRange(FaceUVs(Direction.down));
		return meshData;
	}
	
}
