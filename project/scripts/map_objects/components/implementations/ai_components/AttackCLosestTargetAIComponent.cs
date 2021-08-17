using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class AttackClosestTargetAIComponent : AIComponent
{
    public override async Task MakeTurn()
    {
        Character target = GetClosestTarget();

        if (target == null) return;

        List<MoveCell> moveAreaMoveCells = moveComponent.MoveAvailable() ? moveComponent.GetMoveArea() : new List<MoveCell>();
        List<MapCell> attackCircleMapCells = map.AllNeighboursFor(target.Cell.X, target.Cell.Y);
        List<MapCell> moveAreaMapCells = moveAreaMoveCells.Select(cell => cell.MapCell).ToList();

        moveAreaMapCells.Add(character.Cell);

        List<MapCell> cellsAttackCanOccurFrom = attackCircleMapCells.Where(cell => moveAreaMapCells.Contains(cell)).ToList();

        if (cellsAttackCanOccurFrom.Count > 0)
        {
            if (!cellsAttackCanOccurFrom.Contains(character.Cell))
            {
                await moveComponent.MoveTo(GetClosestCellFromList(cellsAttackCanOccurFrom));
            }

            if (attackComponent.AttackAvailable())
            {
                await attackComponent.Attack(target.GetTargetComponent());
            }
        }
        else if (moveComponent.MoveAvailable()) {
            MapCell farthestCell = null;

            var path = map.PathfindLayer.Pathfind(character.Cell, target.Cell);
            foreach(var cell in path.Skip(1))
            {
                if (!moveAreaMapCells.Contains(cell))
                {
                    break;
                }
                farthestCell = cell;
            }

            if (farthestCell != null)
            {
                await moveComponent.MoveTo(farthestCell);
            }
        }
    }

    private int CellDistance(MapCell a, MapCell b)
    {
        return Mathf.RoundToInt((new Vector2(b.X, b.Y) - new Vector2(a.X, a.Y)).Length());
    }

    Character GetClosestTarget()
    {
        Character result = null;
        int minDistance = int.MaxValue;

        foreach(Character enemy in enemyCharacters)
        {
            int distance = CellDistance(enemy.Cell, character.Cell);
            if (distance < minDistance)
            {
                result = enemy;
                minDistance = distance;
            }
        }

        return result;
    }

    MapCell GetClosestCellFromList(List<MapCell> cells)
    {
        MapCell result = null;

        int minDistance = int.MaxValue;
        foreach(MapCell cell in cells)
        {
            int distance = CellDistance(cell, character.Cell);
            if (distance < minDistance)
            {
                result = cell;
                minDistance = distance;
            }
        }

        if (result == null) GD.PrintErr("Closest position don't found. Something went wrong!");

        return result;
    }
}