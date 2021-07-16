using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class ManaMover
{
	public TacticMap originMap;

	public ManaMover(TacticMap tacticMap)
	{
		originMap = tacticMap;
	}

	public MapWithChanges GetChangesMap()
	{
		MapWithChanges mapWithChanges = new MapWithChanges(originMap.Width, originMap.Height);

		for (int x = 0; x < originMap.Width; x++)
		{
			for (int y = 0; y < originMap.Height; y++)
			{
				MapCell mapCell = originMap.CellBy(x, y);
				ManaCell manaCell = mapCell.Mana;

				if (!mapCell.Solid && manaCell.ManaType != ManaType.None)
				{
					List<MapCell> solidMapCells = originMap.DirectNeighboursFor(x, y);
					solidMapCells.RemoveAll(MapCell.IsSolid);

					double manaSpreadOut = manaCell.Density / (solidMapCells.Count + 1);
					if (manaSpreadOut > 0f)
					{

					}
					// spreading mana changes to the map of changes
					foreach (var cell in solidMapCells)
					{
						mapWithChanges.AddChange(new Cords2D(mapCell.X, mapCell.Y), new Cords2D(cell.X, cell.Y), new ManaCell(manaCell.ManaType, manaSpreadOut));
						//GD.Print($"spread {x} {y} to {cell.X} {cell.Y} {manaCell.Density} {manaSpreadOut} {solidMapCells.Count}");
					}

				}

			}
		}

		return mapWithChanges;
	}

	public void ApplyChangesMap(MapWithChanges mapWithChanges)
	{
		for (int x = 0; x < originMap.Width; x++)
		{
			for (int y = 0; y < originMap.Height; y++)
			{
				MapCell mapCell = originMap.CellBy(x, y);
				ManaCell manaCell = mapCell.Mana;
				double startDensity = manaCell.Density;
				ManaType startManaType = manaCell.ManaType;

				if (!mapCell.Solid) // if this mapCell can receive changes
				{
					CellChanges cellChanges = mapWithChanges.GetCellWithChanges(x, y);
					if (cellChanges == null)
						break;

					cellChanges.SortByHiest();
					foreach (ManaCell changeManaCell in cellChanges.manaCells)
					{
						Cords2D sourceCords2D = cellChanges.cordsByManaCellChange[changeManaCell];
						ManaCell sourceManaCell = originMap.CellBy(sourceCords2D).Mana;
						
						//GD.Print($"compare {x} {y} {manaCell.ManaType} {sourceCords2D.x} {sourceCords2D.y} {sourceManaCell.ManaType}");
						if (manaCell.ManaType == ManaType.None || manaCell.ManaType == sourceManaCell.ManaType)
						{

							double diff = sourceManaCell.Density - manaCell.Density;
							if (diff > 0f)
							{
								//GD.Print($"{x} {y} change {changeManaCell.Density} {changeManaCell.ManaType} source {sourceCords2D.x} {sourceCords2D.y}");
								manaCell.Density += changeManaCell.Density;
								manaCell.ManaType = sourceManaCell.ManaType;

								sourceManaCell.Density -= changeManaCell.Density;
							}
							else
							{
								//GD.Print("source have less density");
							}
						}
					}

				}
				//GD.Print($"{x} {y} from {startDensity} {startManaType} to {manaCell.Density} {manaCell.ManaType}");
			}
		}

	}

}

