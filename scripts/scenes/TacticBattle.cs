using Godot;
using System;
using System.Collections.Generic;

public class TacticBattle : Node
{
	private TacticMap tacticMap;
	private Character player;
	private Vector2 FindStartPosition(TacticMap map)
	{
		for (int x = 0; x < map.Width; x++)
		{
			for (int y = 0; y < map.Height; y++)
			{
				if (!map.GetSolid(x, y)) {
					return new Vector2(x, y);
				}
			}
		}

		return Vector2.NegOne;
	}
	
	public override void _Input(InputEvent inputEvent)
	{
		if (!inputEvent.IsActionPressed("map_move")) {
			return;
		}

		var tilemap = tacticMap.TileMap;
		var camera2D = GetNode<Camera2D>("Camera2D");
		var mousePos = camera2D.GetGlobalMousePosition();
		mousePos -= tilemap.GlobalPosition;
		var tilePos = mousePos / tilemap.CellSize;
		var x = (int)tilePos.x;
		var y = (int)tilePos.y;


		if (x < 0 || x >= tacticMap.Width || y < 0 || y >= tacticMap.Height)
		{
			// Out of bounds
			return;
		}

		player.MoveTo(x, y);
	}

	public override void _Ready()
	{
		tacticMap = GetNode<TacticMap>("Map");
		player = GetNode<Character>("Player");

		tacticMap.Generate();
		player.BindMap(tacticMap, FindStartPosition(tacticMap));
	}
}
