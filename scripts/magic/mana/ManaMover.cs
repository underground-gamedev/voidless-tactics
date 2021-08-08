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

					int manaSpreadOut = sourceManaCell.ActualValue / (solidMapCells.Count + 1);
					if (manaSpreadOut > 0f)
					{

					}
					// spreading mana changes to the map of changes
					foreach (var targetCell in solidMapCells)
					{
						mapWithChanges.AddChange(new Cords2D(targetCell.X, targetCell.Y), new Cords2D(sourceMapCell.X, sourceMapCell.Y), new ManaCell(sourceManaCell.ManaType, manaSpreadOut));
						GDPrint.Print($"spread {sourceMapCell.X} {sourceMapCell.Y} to {targetCell.X} {targetCell.Y} {sourceManaCell.ActualValue} {manaSpreadOut} {solidMapCells.Count}");
					}

				}

			}
		}

		return mapWithChanges;
	}

	public void MoveMana()
	{
		ApplyChangesMap(GetChangesMap());
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
				double startDensity = manaCell.ActualValue;
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

							float diff = sourceManaCell.ActualValue - manaCell.ActualValue;
							if (diff > 0f)
							{
								GDPrint.Print($"{x} {y} change {changeMana.ActualValue} {changeMana.ManaType} source {sourceCords2D.x} {sourceCords2D.y}");
								
								// if source sell have few mana
								if (changeMana.ActualValue > sourceManaCell.ActualValue) {
									changeMana.Consume(changeMana.ActualValue - sourceManaCell.ActualValue);
								}
								//if target cell have too much mana
								// changeMana.Density = Mathf.Clamp(changeMana.Density, 0, 1 - manaCell.Density);

								var newDensity = manaCell.ActualValue + changeMana.ActualValue;
								manaCell.Set(sourceManaCell.ManaType, newDensity);

								sourceManaCell.Consume(changeMana.ActualValue);
							}
							else
							{
								GDPrint.Print("source have less density");
							}
						}
					}

				}
				GDPrint.Print($"{x} {y} from {startDensity} {startManaType} to {manaCell.ActualValue} {manaCell.ManaType}");
			}
		}

	}

}

