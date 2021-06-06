using System.Collections.Generic;
using System.Linq;
using Godot;

public class VisualLayer: Node, IMapLayer
{
    private int width;
    private int height;
	public TileMap TileMap => GetNode<TileMap>("TileMap");
    [Signal]
    public delegate void OnCellClick(int x, int y);

	public override void _Input(InputEvent inputEvent)
	{
		if (!inputEvent.IsActionPressed("map_move")) { return; }

		var tilemap = TileMap;
		var camera2D = GetNode<Camera2D>("/root/TacticBattle/Camera2D");
		var mousePos = camera2D.GetGlobalMousePosition();
		mousePos -= tilemap.GlobalPosition;
		var tilePos = mousePos / tilemap.CellSize;
		var x = (int)tilePos.x;
		var y = (int)tilePos.y;

        EmitSignal(nameof(OnCellClick), x, y);
	}

	private void Visualize(TacticMap map)
	{
		var destMap = TileMap;
		destMap.Clear();

		var tileset = destMap.TileSet;
		var wall = tileset.FindTileByName("wall");
		var floor = tileset.FindTileByName("floor");
		var fall = tileset.FindTileByName("fall");

		for (int x = 0; x < map.Width; x++)
		{
			for (int y = 0; y < map.Height; y++)
			{
				var isSolid = map.GetSolid(x, y);
				var targetTile = isSolid ? wall : floor;
				if (!map.IsOutOfBounds(x, y-1) && !map.GetSolid(x, y-1) && isSolid) { targetTile = fall; }
				destMap.SetCell(x, y, targetTile);
			}
		}

		destMap.UpdateBitmaskRegion(
			new Vector2(),
			OS.WindowSize
		);
	}

    public void OnSync(TacticMap map)
    {
        Visualize(map);
    }
}