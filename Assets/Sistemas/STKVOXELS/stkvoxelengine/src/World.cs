﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
    public GameObject chunkPrefab;

    void Start()
    {
		/*
        for (int x = -2; x < 2; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                for (int z = -1; z < 1; z++)
                {
                    CreateChunk(x * 16, y * 16, z * 16); //1 chunk a cada 16m^3 
                }
            }
        }
    }
*/

		//CreateChunk (0, 0, 0); // apenas 1 chunk
	}
	// Update is called once per frame
	void Update () {
	
	}

	public Chunk CreateChunk(int x, int y, int z)
    {
        WorldPos worldPos = new WorldPos(x, y, z);

        //Instantiate the chunk at the coordinates using the chunk prefab
        GameObject newChunkObject = Instantiate(
                        chunkPrefab, new Vector3(x, y, z),
                        Quaternion.Euler(Vector3.zero)
                    ) as GameObject;
		newChunkObject.transform.SetParent (this.transform);
        Chunk newChunk = newChunkObject.GetComponent<Chunk>();

        newChunk.pos = worldPos;
        newChunk.world = this;

        //Add it to the chunks dictionary with the position as the key
        chunks.Add(worldPos, newChunk);

        for (int xi = 0; xi < 16; xi++)
        {
            for (int yi = 0; yi < 16; yi++)
            {
                for (int zi = 0; zi < 16; zi++)
                {
					
					if (xi == 8 & yi == 8 & zi == 8) {
						SetBlock (x + xi, y + yi, z + zi, new BlockGrass ()); // 1 bloco no meio do chunk (se 16m^3)
					
					} else {
						SetBlock (x + xi, y + yi, z + zi, new BlockAir ()); // o resto é ar

					}


					/*
                    if (yi <= 7)
                    {
						SetBlock(x + xi, y + yi, z + zi, new BlockGrass());
					
				
					}
                    else
                    {
                      SetBlock(x + xi, y + yi, z + zi, new BlockAir());
                    }*/

                }
            }

		}
		return newChunk;
    }

    public void DestroyChunk(int x, int y, int z)
    {
        Chunk chunk = null;
        if (chunks.TryGetValue(new WorldPos(x, y, z), out chunk))
        {
            Object.Destroy(chunk.gameObject);
            chunks.Remove(new WorldPos(x, y, z));
        }
    }

    public Chunk GetChunk(int x, int y, int z)
    {
        WorldPos pos = new WorldPos();
        float multiple = Chunk.chunkSize;
        pos.x = Mathf.FloorToInt(x / multiple) * Chunk.chunkSize;
        pos.y = Mathf.FloorToInt(y / multiple) * Chunk.chunkSize;
        pos.z = Mathf.FloorToInt(z / multiple) * Chunk.chunkSize;

        Chunk containerChunk = null;

        chunks.TryGetValue(pos, out containerChunk);

        return containerChunk;
    }

    public Block GetBlock(int x, int y, int z)
    {
        Chunk containerChunk = GetChunk(x, y, z);

        if (containerChunk != null)
        {
            Block block = containerChunk.GetBlock(
                x - containerChunk.pos.x,
                y - containerChunk.pos.y,
                z - containerChunk.pos.z);

            return block;
        }
        else
        {
            return new BlockAir();
        }

    }
	void UpdateIfEqual(int value1, int value2, WorldPos pos)
	{
		if (value1 == value2)
		{
			Chunk chunk = GetChunk(pos.x, pos.y, pos.z);
			if (chunk != null)
				chunk.update = true;
		}
	}

	public Block SetBlock(int x, int y, int z, Block block)
    {
        Chunk chunk = GetChunk(x, y, z);

		if (chunk != null) {
			Block bloco = chunk.SetBlock (x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
			chunk.update = true;

			UpdateIfEqual (x - chunk.pos.x, 0, new WorldPos (x - 1, y, z));
			UpdateIfEqual (x - chunk.pos.x, Chunk.chunkSize - 1, new WorldPos (x + 1, y, z));
			UpdateIfEqual (y - chunk.pos.y, 0, new WorldPos (x, y - 1, z));
			UpdateIfEqual (y - chunk.pos.y, Chunk.chunkSize - 1, new WorldPos (x, y + 1, z));
			UpdateIfEqual (z - chunk.pos.z, 0, new WorldPos (x, y, z - 1));
			UpdateIfEqual (z - chunk.pos.z, Chunk.chunkSize - 1, new WorldPos (x, y, z + 1));
			return bloco;
		} else {
			return null;
		}
    }
}
