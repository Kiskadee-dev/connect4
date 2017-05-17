using UnityEngine;
using System.Collections;

public class Block
{
    public enum Direction { north, east, south, west, up, down };
	public bool useocclusion;
	public bool ignorethisblockforocclusion;

    public struct Tile { public int x; public int y;}
    const float tileSize = 0.25f;

	public string blocktipe { get; set; }
	public string color = "default";
	public bool canchangecolor = false;

	public bool canplaceup;
	public bool canplacedown;
	public bool canplaceeast;
	public bool canplacewest;
	public bool canplacenorth;
	public bool canplacesouth;

	public Quaternion quatcasorodarbloco { get; set; }
	public bool ischanging { get; set; }

	public Vector3 blockposition { get; set; }
	public Quaternion blockrotation { get; set; }

	public GameObject ThrusterObj;

	public GameObject Turret { get; set; }
	public GameObject InstanceTurret;

    //Base block constructor
    public Block()
    {

    }


    public virtual MeshData Blockdata
     (Chunk chunk, int x, int y, int z, MeshData meshData)
	{

		meshData.useRenderDataForCol = true;
		if (useocclusion) {
			if (chunk.GetBlock (x, y + 1, z).ignorethisblockforocclusion) {
				meshData = FaceDataUp (chunk, x, y, z, meshData);

			} else {
				if (!chunk.GetBlock (x, y + 1, z).IsSolid (Direction.down)) {
					meshData = FaceDataUp (chunk, x, y, z, meshData);
				}
			}

			if (chunk.GetBlock (x, y - 1, z).ignorethisblockforocclusion) {
				meshData = FaceDataDown (chunk, x, y, z, meshData);


			} else {
				if (!chunk.GetBlock (x, y - 1, z).IsSolid (Direction.up)) {
					meshData = FaceDataDown (chunk, x, y, z, meshData);
				}
			}

			if (chunk.GetBlock (x, y, z + 1).ignorethisblockforocclusion) {
				meshData = FaceDataNorth (chunk, x, y, z, meshData);

			} else {

				if (!chunk.GetBlock (x, y, z + 1).IsSolid (Direction.south)) {
					meshData = FaceDataNorth (chunk, x, y, z, meshData);
				}
			}

			if (chunk.GetBlock (x, y, z - 1).ignorethisblockforocclusion) {
				meshData = FaceDataSouth (chunk, x, y, z, meshData);


			} else {
				if (!chunk.GetBlock (x, y, z - 1).IsSolid (Direction.north)) {
					meshData = FaceDataSouth (chunk, x, y, z, meshData);
				}
			}

			if (chunk.GetBlock (x + 1, y, z).ignorethisblockforocclusion) {
				meshData = FaceDataEast (chunk, x, y, z, meshData);
			}
				 else {
					if (!chunk.GetBlock (x + 1, y, z).IsSolid (Direction.west)) {
						meshData = FaceDataEast (chunk, x, y, z, meshData);
					}
				}


			if (chunk.GetBlock (x - 1, y, z).ignorethisblockforocclusion) {
				meshData = FaceDataWest (chunk, x, y, z, meshData);


				} else {
				if (!chunk.GetBlock (x - 1, y, z).IsSolid (Direction.east)) {
					meshData = FaceDataWest (chunk, x, y, z, meshData);
				}
			}


			return meshData;

		} else {
			meshData = FaceDataUp (chunk, x, y, z, meshData);
			meshData = FaceDataDown (chunk, x, y, z, meshData);
			meshData = FaceDataNorth (chunk, x, y, z, meshData);
			meshData = FaceDataSouth (chunk, x, y, z, meshData);
			meshData = FaceDataEast (chunk, x, y, z, meshData);
			meshData = FaceDataWest (chunk, x, y, z, meshData);
			return meshData;
		}
	}

    protected virtual MeshData FaceDataUp
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.up));
        return meshData;
    }

    protected virtual MeshData FaceDataDown
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.down));
        return meshData;
    }

    protected virtual MeshData FaceDataNorth
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.north));
        return meshData;
    }

    protected virtual MeshData FaceDataEast
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.east));
        return meshData;
    }

    protected virtual MeshData FaceDataSouth
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.south));
        return meshData;
    }

    protected virtual MeshData FaceDataWest
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.west));
        return meshData;
    }

    public virtual Tile TexturePosition(Direction direction)
	{
		if (color == "default") {
			Tile tile = new Tile ();
			tile.x = 0;
			tile.y = 0;

			return tile;
		} else {
			if (color == "grass") {
				Tile tile = new Tile ();
				switch (direction) {
				case Direction.up:
					tile.x = 2;
					tile.y = 0;
					return tile;
				case Direction.down:
					tile.x = 1;
					tile.y = 0;
					return tile;
				
				}
			} else {

				if (color == "0") {
					Tile tile = new Tile ();
					tile.x = 0;
					tile.y = 0;
					return tile;
				} else {

					if (color == "1") {
						Tile tile = new Tile ();
						tile.x = 1;
						tile.y = 0;
						return tile;
					} else {

						if (color == "2") {
							Tile tile = new Tile ();
							tile.x = 2;
							tile.y = 0;
							return tile;
						} else {

							if (color == "3") {
								Tile tile = new Tile ();
								tile.x = 3;
								tile.y = 0;
								return tile;
							} else {

								if (color == "4") {
									Tile tile = new Tile ();
									tile.x = 4;
									tile.y = 0;
									return tile;
								} else {

									if (color == "5") {
										Tile tile = new Tile ();
										tile.x = 5;
										tile.y = 0;
										return tile;
									} else {

										if (color == "6") {
											Tile tile = new Tile ();
											tile.x = 6;
											tile.y = 0;
											return tile;
										} else {

											if (color == "7") {
												Tile tile = new Tile ();
												tile.x = 0;
												tile.y = 1;
												return tile;
											} else {

												if (color == "8") {
													Tile tile = new Tile ();
													tile.x = 1;
													tile.y = 1;
													return tile;
												} else { 
													Tile tile = new Tile ();

													tile.x = 0;
													tile.y = 0;

													return tile;
												}

											}
										}
									}
								}
							}
						}
					}
				}
			}
			Tile tilee = new Tile ();
			tilee.x = 0;
			tilee.y = 0;

			return tilee;
		}
	}
			
		
	
	public virtual void RebuildUV(){
		FaceUVs (Direction.up);
		FaceUVs (Direction.down);
		FaceUVs (Direction.north);
		FaceUVs (Direction.east);
		FaceUVs (Direction.south);
		FaceUVs (Direction.west);





	}

    public virtual Vector2[] FaceUVs(Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);

        UVs[0] = new Vector2(tileSize * tilePos.x + tileSize,
            tileSize * tilePos.y);
        UVs[1] = new Vector2(tileSize * tilePos.x + tileSize,
            tileSize * tilePos.y + tileSize);
        UVs[2] = new Vector2(tileSize * tilePos.x,
            tileSize * tilePos.y + tileSize);
        UVs[3] = new Vector2(tileSize * tilePos.x,
            tileSize * tilePos.y);

        return UVs;
    }

    public virtual bool IsSolid(Direction direction)
    {
        switch (direction)
        {
		case Direction.north:
			canplacenorth = true;

			return true;
		case Direction.east:
			canplaceeast = true;

			return true;
		case Direction.south:
			canplacesouth = true;

			return true;
		case Direction.west:
			canplacewest = true;

			return true;
		case Direction.up:
			canplaceup = true;

			return true;
		case Direction.down:
			canplacedown = true;

			return true;
        }

        return false;

	}

	public virtual void DestroyInstances(){
		MonoBehaviour.Destroy (InstanceTurret);
	}
	public virtual GameObject SpawnTurret(GameObject turret){
		Vector3 Eulers = blockrotation.eulerAngles;
			Debug.Log (Eulers);
			GameObject turr = MonoBehaviour.Instantiate (turret, new Vector3 (blockposition.x, blockposition.y, blockposition.z), blockrotation);
			turr.GetComponent<Rigidbody> ().isKinematic = true;	
			this.InstanceTurret = turr;
			return turr;
		}
	}



