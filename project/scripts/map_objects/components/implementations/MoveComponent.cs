using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MoveComponent : Node, IMoveComponent
{
    private MapObject mapObject;
    private BasicStats basicStats;
    private bool moved;


    public override void _Ready()
    {
        mapObject = this.FindParent<MapObject>();
        basicStats = (mapObject as IBasicStatsPresenter).BasicStats;
    }

    public List<MoveCell> GetMoveArea()
    {
        var map = mapObject.Map;
        var pathfinder = mapObject.Map.PathfindLayer;

        var normalMoveCells = pathfinder
            .GetAllAvailablePathDest(mapObject.Cell, basicStats.Speed.ModifiedActualValue/2)
            .Select(pos => map.CellBy(pos.Item1, pos.Item2))
            .Where(cell => cell.MapObject == null)
            .Select(cell => new MoveCell(cell, false));

        var sprintMoveCells = pathfinder
            .GetAllAvailablePathDest(mapObject.Cell, basicStats.Speed.ModifiedActualValue)
            .Select(pos => map.CellBy(pos.Item1, pos.Item2))
            .Where(cell => cell.MapObject == null && normalMoveCells.All(n => n.MapCell != cell))
            .Select(cell => new MoveCell(cell, true));

        return normalMoveCells.Concat(sprintMoveCells).ToList();
    }

    public bool MoveAvailable()
    {
        return true;
    }

    public async Task MoveTo(MapCell target)
    {
        if (moved) return;
        if (!MoveAvailable()) return;
        var currentCell = mapObject.Cell;
        if (currentCell == target) return;

        int x = currentCell.X;
        int y = currentCell.Y;

        var map = mapObject.Map;
        var path = map.PathfindLayer.Pathfind(currentCell, target);

        if (path == null) return;

        var cost = path.Length - 1;

        if (cost <= 0 || cost > basicStats.Speed.ModifiedActualValue) return;

        moved = true;
        mapObject.SetCell(target);
        foreach(var cell in path)
        {
            mapObject.SyncWithMap(map, cell.X, cell.Y);
            await this.Wait(0.1f);
        }
        moved = false;
    }
}