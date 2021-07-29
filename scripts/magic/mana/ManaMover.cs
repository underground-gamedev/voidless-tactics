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
				MapCell sourceMapCell = originMap.CellBy(x, y);
				ManaCell sourceManaCell = sourceMapCell.Mana;

				if (!sourceMapCell.Solid && sourceManaCell.ManaType != ManaType.None)
				{
					List<MapCell> solidMapCells = originMap.DirectNeighboursFor(x, y);
					solidMapCells.RemoveAll(MapCell.IsSolid);

					float manaSpreadOut = sourceManaCell.Density / (solidMapCells.Count + 1);
					if (manaSpreadOut > 0f)
					{

					}
					// spreading mana changes to the map of changes
					foreach (var targetCell in solidMapCells)
					{
						mapWithChanges.AddChange(new Cords2D(targetCell.X, targetCell.Y), new Cords2D(sourceMapCell.X, sourceMapCell.Y), new ManaCell(sourceManaCell.ManaType, manaSpreadOut));
						GDPrint.Print($"spread {sourceMapCell.X} {sourceMapCell.Y} to {targetCell.X} {targetCell.Y} {sourceManaCell.Density} {manaSpreadOut} {solidMapCells.Count}");
					}

				}

			}
		}

		return mapWithChanges;
	}

	public void ApplyChangesMap(MapWithChanges mapWithChanges)
	{
		GDPrint.Print($"applying changes {originMap.Width} {originMap.Height}");
		for (int x = 0; x < originMap.Width; x++)
		{
			for (int y = 0; y < originMap.Height; y++)
			{
				MapCell mapCell = originMap.CellBy(x, y);
				ManaCell manaCell = mapCell.Mana;
				double startDensity = manaCell.Density;
				ManaType startManaType = manaCell.ManaType;

				GDPrint.Print($"{x} {y} cycle");
				if (!mapCell.Solid) // if this mapCell can receive changes
				{
					CellChanges cellChanges = mapWithChanges.GetCellWithChanges(x, y);
					if (cellChanges == null)
					{
						GDPrint.Print($"{x} {y} no changes");
						break;
					}

					cellChanges.SortByHiest();


					foreach (ManaCell changeMana in cellChanges.manaCells)
					{
						Cords2D sourceCords2D = cellChanges.cordsByManaCellChange[changeMana];
						ManaCell sourceManaCell = originMap.CellBy(sourceCords2D).Mana;

						GDPrint.Print($"compare {x} {y} {manaCell.ManaType} {sourceCords2D.x} {sourceCords2D.y} {sourceManaCell.ManaType}");
						if (manaCell.ManaType == ManaType.None || manaCell.ManaType == sourceManaCell.ManaType)
						{

							float diff = sourceManaCell.Density - manaCell.Density;
							if (diff > 0f)
							{
								GDPrint.Print($"{x} {y} change {changeMana.Density} {changeMana.ManaType} source {sourceCords2D.x} {sourceCords2D.y}");
								
								// if source sell have few mana
								changeMana.Density = Mathf.Clamp(changeMana.Density, 0f, sourceManaCell.Density);
								//if target cell have too much mana
								changeMana.Density = Mathf.Clamp(changeMana.Density, 0f, 1f - manaCell.Density);

								var newDensity = manaCell.Density + changeMana.Density;
								manaCell.ManaType = sourceManaCell.ManaType;
								manaCell.Density = newDensity;

								sourceManaCell.Density -= changeMana.Density;
							}
							else
							{
								GDPrint.Print("source have less density");
							}
						}
					}

				}
				GDPrint.Print($"{x} {y} from {startDensity} {startManaType} to {manaCell.Density} {manaCell.ManaType}");
			}
		}

	}

}

