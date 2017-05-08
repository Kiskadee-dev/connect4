using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : Block{
	public BuildingBlock()
		: base()
	{
		    useocclusion = true;
			blocktipe = "BuildingBlock";
			canchangecolor = true;
	}
	/*
	public override Tile TexturePosition (Direction direction)
	{
		
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
			tile.x = 3;
			tile.y = 0;
			return tile;
		} else {
			if (color == "default") {
				Tile tile = new Tile ();
				tile.x = 0;
				tile.y = 0;

				return tile;
			} else {
				Tile tile = new Tile ();
				tile.x = 0;
				tile.y = 0;
				return tile;
			}
		}

}
*/
}

