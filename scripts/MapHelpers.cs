using System;
using System.Collections.Generic;
using System.Linq;
public static class MapHelpers
{
    public static List<MapCell> GetAvailableMoveCells(this TacticMap map, Character character)
    {
        var availablePositions = map.PathfindLayer.GetAllAvailablePathDest(character.Cell, character.MovePoints);
        var availableCells = availablePositions.Select(pos => map.CellBy(pos.Item1, pos.Item2)).Where(cell => cell.Character == null).ToList();
        return availableCells;
    }
}