﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAzul : Block {
	public BlockAzul()
	:base()
	{
		blocktipe = "Azul";
		useocclusion = true;

	}
	public override Tile TexturePosition(Direction direction)
	{
		//não pode trocar, é grama uai;
		Tile tile = new Tile ();
		switch (direction) {
		case Direction.up:
			tile.x = 1;
			tile.y = 0;
			return tile;
		case Direction.down:
			tile.x = 1;
			tile.y = 0;
			return tile;
		}
		tile.x = 1;
		tile.y = 0;
		return tile;
	}
}
