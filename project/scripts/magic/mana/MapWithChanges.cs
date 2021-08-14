using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

public struct MapWithChanges
{
	public CellChanges[,] cellChanges;

	public MapWithChanges(int wight, int height)
	{
		cellChanges = new CellChanges[wight, height];
	}

	public void AddChange(Cords2D target, Cords2D source, ManaCell manaCell)
	{
		if (cellChanges[target.x, target.y] == null)
			cellChanges[target.x, target.y] = new CellChanges();
		cellChanges[target.x, target.y].AddChange(manaCell, source);
	}

	public CellChanges GetCellWithChanges(int x, int y)
	{
		if ((x < 0 || x >= cellChanges.GetLength(0)) && (y < 0 || y >= cellChanges.GetLength(1)))
			return null;
		else
			return cellChanges[x, y];
	}
}
