using UnityEngine;
using System.Collections;

public class BlockAir : Block
{
    public BlockAir()
        : base()
    {
		blocktipe = "ar";

    }


    public override MeshData Blockdata
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        return meshData;
    }

    public override bool IsSolid(Block.Direction direction)
    {
		/*
		canplaceup = false;
		canplacedown = false;
		canplaceeast = false;
		canplacewest = false;
		canplacenorth = false;
		canplacesouth = false;

        return false;
*/
		return false;
}

}