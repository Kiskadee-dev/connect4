using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrass : Block {
	
	public BlockGrass()
		: base()
	{
		useocclusion = true;
		ignorethisblockforocclusion = false;
		color = "grass";
		blocktipe = "grass";
		canchangecolor = false;

		canplaceup = true;
		canplacedown = true;
		canplaceeast = true;
		canplacenorth = true;
		canplacesouth = true;
		canplacewest = true;
	}



	public override Tile TexturePosition(Direction direction)
	{
		//não pode trocar, é grama uai;
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
		tile.x = 3;
		tile.y = 0;
		return tile;
	}
}
