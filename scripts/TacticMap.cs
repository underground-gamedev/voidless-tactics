using Godot;
using System;
using System.Collections.Generic;

public class TacticMap: Node
{
	private bool[,] solid;

    [Export]
	private int width = 20;
    [Export]
	private int height = 20;
    private AStar2D aStar;

	public int Width { get => width; }
	public int Height { get => height; }
	public int TileCount { get => width * height; }

    public TileMap TileMap => GetNode<TileMap>("TileMap");

	public void SetSolid(int x, int y, bool solid) 
	{
		this.solid[x, y] = solid;
	}

    private void Visualize()
    {
		var destMap = TileMap;
        destMap.Clear();

        var tileset = destMap.TileSet;
        var wall = tileset.FindTileByName("wall");
        var floor = tileset.FindTileByName("floor");

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var isSolid = GetSolid(x, y);
                var targetTile = isSolid ? wall : floor;
                destMap.SetCell(x, y, targetTile);
            }
        }

        destMap.UpdateBitmaskRegion(
            new Vector2(),
            OS.WindowSize
        );
    }

    private int xyToIdx(int x, int y)
    {
        return y * width + x;
    }

    public Vector2[] Pathfind(Vector2 src, Vector2 dest)
    {
        int srcIdx = xyToIdx((int)src.x, (int)src.y);
        int destIdx = xyToIdx((int)dest.x, (int)dest.y);
        return aStar.GetPointPath(srcIdx, destIdx);
    }

    private void PreparePathfind()
    {
        aStar = new AStar2D();
        for (int idx = 0; idx < TileCount; idx++)
        {
            int x = idx % width;
            int y = idx / width;
            aStar.AddPoint(idx, new Vector2(x, y));
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (GetSolid(x, y))
                {
                    continue;
                }

                var childs = new List<Tuple<int, int>>() {
                    new Tuple<int, int>(x+1, y),
                    new Tuple<int, int>(x-1, y),
                    new Tuple<int, int>(x, y+1),
                    new Tuple<int, int>(x, y-1),
                };

                foreach (var child in childs)
                {
                    if (!GetSolid(child.Item1, child.Item2))
                    {
                        aStar.ConnectPoints(xyToIdx(x, y),
                                            xyToIdx(child.Item1, child.Item2));
                    }
                }
            }
        }
    }
	public void Sync()
	{
        Visualize();
        PreparePathfind();
	}

	public bool GetSolid(int x, int y)
	{
        if (x < 0 || x > width || y < 0 || y > height)
        {
            return true;
        }
		return this.solid[x, y];
	}

    public void Generate()
    {
        var map = this;
		this.solid = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            map.SetSolid(x, 0, true);
            map.SetSolid(x, height-1, true);
        }

        for (int y = 0; y < height; y++)
        {
            map.SetSolid(0, y, true);
            map.SetSolid(width-1, y, true);
        }

        map.SetSolid(2, height/2, true);

        Sync();
    }
}
