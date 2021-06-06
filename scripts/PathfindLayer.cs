using System.Collections.Generic;
using System.Linq;
using Godot;

public class PathfindLayer: Node, IMapLayer
{
    private int width;
    private int height;
	private AStar2D aStar;

	private int xyToIdx(int x, int y)
	{
		return y * width + x;
	}

	private (int, int) idxToXy(int idx)
	{
		int x = idx % width;
		int y = idx / width;
		return (x, y);
	}

	public (int, int)[] Pathfind(Vector2 src, Vector2 dest)
	{
		int srcIdx = xyToIdx((int)src.x, (int)src.y);
		int destIdx = xyToIdx((int)dest.x, (int)dest.y);
		return aStar.GetPointPath(srcIdx, destIdx).Select(pos => ((int)pos.x, (int)pos.y)).ToArray();
	}

	public (int, int)[] GetAllAvailablePathDest(MapCell cell, int range)
	{
		var results = new List<(int, int)>();

		var pointsToCheck = new HashSet<(int, int)>() {(cell.X, cell.Y)};
		var checkedPoints = new HashSet<(int, int)>();

		var srcIdx = xyToIdx((int)cell.X, (int)cell.Y);

		while (pointsToCheck.Count > 0)
		{
			var pointToCheck = pointsToCheck.First();
			pointsToCheck.Remove(pointToCheck);
			checkedPoints.Add(pointToCheck);

			var targetIdx = xyToIdx((int)pointToCheck.Item1, (int)pointToCheck.Item2);
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

	private void PreparePathfind(TacticMap map)
	{
        width = map.Width;
        height = map.Height;
		aStar = new AStar2D();

		for (int idx = 0; idx < width * height; idx++)
		{
            var (x, y) = idxToXy(idx);
			aStar.AddPoint(idx, new Vector2(x, y));
		}

        foreach (var cell in map.Where(cell => !cell.Solid))
        {
            var neighbours = map.DirectNeighboursFor(cell.X, cell.Y);

            foreach (var neighCell in neighbours.Where(neigh => !neigh.Solid))
            {
                aStar.ConnectPoints(xyToIdx(cell.X, cell.Y),
                                    xyToIdx(neighCell.X, neighCell.Y));
            }

        }
	}
    public void OnSync(TacticMap map)
    {
        PreparePathfind(map);
    }
}