using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CellChanges
{
	public List<ManaCell> manaCells;

	public Dictionary<ManaCell, Cords2D> cordsByManaCellChange;

	public CellChanges()
	{
		manaCells = new List<ManaCell>();
		cordsByManaCellChange = new Dictionary<ManaCell, Cords2D>();
	}

	public void AddChange(ManaCell manaCell, Cords2D sourceCords)
	{
		manaCells.Add(manaCell);
		cordsByManaCellChange.Add(manaCell, sourceCords);
	}

	public void SortByHiest()
	{
		manaCells.Sort((x, y) => x.ActualValue.CompareTo(y.ActualValue));
	}
}

