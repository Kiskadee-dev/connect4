using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVermelho : Block {
	public BlockVermelho()
		:base()
	{
		blocktipe = "Vermelho";
		useocclusion = true;


	}
	public override Tile TexturePosition(Direction direction)
	{
		//não pode trocar, é grama uai;
		Tile tile = new Tile ();

		tile.x = 0;
		tile.y = 0;
		return tile;
	}
}
