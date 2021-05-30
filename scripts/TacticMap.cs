using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class TacticMap: Node
{
	[Export]
	private int width = 20;
	[Export]
	private int height = 20;
    [Export]
    private int generationCicleCount = 7;
    [Export]
    private float fillPrecent = 0.45f;

	private bool[,] solid;
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
        var fall = tileset.FindTileByName("fall");

		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				var isSolid = GetSolid(x, y);
				var targetTile = isSolid ? wall : floor;
                if (!GetSolid(x, y-1) && isSolid) { targetTile = fall; }
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

    private Vector2 idxToXy(int idx)
    {
        int x = idx % width;
        int y = idx / width;
        return new Vector2(x, y);
    }

	public Vector2[] Pathfind(Vector2 src, Vector2 dest)
	{
		int srcIdx = xyToIdx((int)src.x, (int)src.y);
		int destIdx = xyToIdx((int)dest.x, (int)dest.y);
		return aStar.GetPointPath(srcIdx, destIdx);
	}

    public Vector2[] GetAllAvailablePathDest(Vector2 src, int range)
    {
        var results = new List<Vector2>();

        var pointsToCheck = new HashSet<Vector2>() {src};
        var checkedPoints = new HashSet<Vector2>();
        //var calculatedCosts = new Dictionary<Vector2, int>();
        var srcIdx = xyToIdx((int)src.x, (int)src.y);
        while (pointsToCheck.Count > 0)
        {
            var pointToCheck = pointsToCheck.First();
            pointsToCheck.Remove(pointToCheck);
            checkedPoints.Add(pointToCheck);

            var targetIdx = xyToIdx((int)pointToCheck.x, (int)pointToCheck.y);
            var path = aStar.GetPointPath(srcIdx, targetIdx);
            if (path == null || path.Count() == 0)
            {
                continue;
            }
            var cost = path.Count() - 1;

            if (cost <= range)
            {
                results.Add(pointToCheck);
                if (cost == range) continue;
            }

            foreach(var neighIdx in aStar.GetPointConnections(targetIdx))
            {
                var neighPos = idxToXy(neighIdx);
                if (checkedPoints.Contains(neighPos) || pointsToCheck.Contains(neighPos)) {
                    continue;
                }

                pointsToCheck.Add(neighPos);
            }
        }

        return results.ToArray();
    }

	private void PreparePathfind()
	{
		aStar = new AStar2D();
		for (int idx = 0; idx < TileCount; idx++)
		{
			aStar.AddPoint(idx, idxToXy(idx));
		}

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (GetSolid(x, y))
				{
					continue;
				}

				var neighbours = new List<(int, int)>() {
					(x+1, y), (x-1, y), (x, y+1), (x, y-1),
				};

				foreach (var (neighX, neighY) in neighbours)
				{
					if (!GetSolid(neighX, neighY))
					{
						aStar.ConnectPoints(xyToIdx(x, y),
											xyToIdx(neighX, neighY));
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
		if (x < 0 || x >= width || y < 0 || y >= height)
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

		var rand = new Random();
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var roll = rand.NextDouble();
				if (roll > fillPrecent)
				{
					map.SetSolid(x, y, true);
				}
			}
		}

		bool[,] buffMap = solid.Clone() as bool[,];
		for (int _i = 0; _i < generationCicleCount; _i++)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					var neighbourCount = 0;
					var neighbourCells = new (int, int)[] {
						(0, 1), (0, -1), (1, 0), (-1, 0), 
						(-1, -1), (-1, 1), (1, -1), (1, 1),
					};

					foreach (var (neighX, neighY) in neighbourCells)
					{
						if (map.GetSolid(x + neighX, y + neighY)) 
						{
							neighbourCount += 1;
						}
					}

					buffMap[x, y] = neighbourCount > 4 || neighbourCount == 0;
				}
			}

			var tmp = buffMap;
			buffMap = solid;
			solid = tmp;
		}

		Sync();
	}
}
