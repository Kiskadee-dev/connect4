﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNeutro : Block {
	public BlockNeutro()
		:base()
	{
		blocktipe = "Neutro";
		useocclusion = true;


	}
	public override Tile TexturePosition(Direction direction)
	{
		//não pode trocar, é grama uai;
		Tile tile = new Tile ();
		switch (direction) {
		case Direction.up:
			tile.x = 3;
			tile.y = 0;
			return tile;
		case Direction.down:
			tile.x = 3;
			tile.y = 0;
			return tile;
		}
		tile.x = 3;
		tile.y = 0;
		return tile;
	}
}
