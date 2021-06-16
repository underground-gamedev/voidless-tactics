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

    private int NormalMoveBaseRange()
    {
        return basicStats.Speed.ActualValue;
    }

    private int SprintBaseRange()
    {
        return basicStats.Speed.ActualValue / 2;
    }

    private int TotalActionCount()
    {
        return basicStats.MoveActions.ActualValue + basicStats.FullActions.ActualValue;
    }

    private int SprintMoveRange()
    {
        return NormalMoveRange() + basicStats.FullActions.ActualValue * SprintBaseRange();
    }

    private int NormalMoveRange()
    {
        return basicStats.MoveActions.ActualValue * NormalMoveBaseRange();
    }

    private int MoveActionsUse(int range)
    {
        var normalRange = NormalMoveRange();
        if (range >= normalRange) return basicStats.MoveActions.ActualValue;
        var actionUse = Mathf.CeilToInt(normalRange / (float)NormalMoveBaseRange());
        return actionUse;
    }

    private int FullActionUse(int range)
    {
        var normalRange = NormalMoveRange();
        if (range <= normalRange) return 0;
        var diff = range - normalRange;
        var actionUse = Mathf.CeilToInt(diff / (float)SprintBaseRange());
        return actionUse;
    }

    public override void _Ready()
    {
        mapObject = this.FindParent<MapObject>();
        basicStats = (mapObject as IBasicStatsHandler).BasicStats;
    }

    public List<MoveCell> GetMoveAvailableCells()
    {
        var map = mapObject.Map;
        var pathfinder = mapObject.Map.PathfindLayer;

        var normalMoveCells = pathfinder
            .GetAllAvailablePathDest(mapObject.Cell, NormalMoveRange())
            .Select(pos => map.CellBy(pos.Item1, pos.Item2))
            .Where(cell => cell.MapObject == null)
            .Select(cell => new MoveCell(cell, 1)); // TODO set real action cost

        var sprintMoveCells = pathfinder
            .GetAllAvailablePathDest(mapObject.Cell, SprintMoveRange())
            .Select(pos => map.CellBy(pos.Item1, pos.Item2))
            .Where(cell => cell.MapObject == null && normalMoveCells.All(n => n.MapCell != cell))
            .Select(cell => new MoveCell(cell, 2)); // TODO set real action cost

        return normalMoveCells.Concat(sprintMoveCells).ToList();
    }

    public bool MoveAvailable()
    {
        return TotalActionCount() > 0;
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
        var path = map.PathfindLayer.Pathfind(new Vector2(x, y), new Vector2(target.X, target.Y));

        if (path == null) return;

        var cost = path.Length - 1;

        var sprintCost = SprintMoveRange();
        var normalCost = NormalMoveRange();

        if (cost <= 0 || cost > sprintCost) return;

        var moveActionUse = MoveActionsUse(cost);
        var fullActionUse = FullActionUse(cost);
        basicStats.MoveActions.ActualValue -= moveActionUse;
        basicStats.FullActions.ActualValue -= fullActionUse;

        moved = true;
        mapObject.SetCell(target);
        foreach(var (posX, posY) in path)
        {
            mapObject.SyncWithMap(map.VisualLayer.TileMap, posX, posY);
            await this.Wait(0.1f);
        }
        moved = false;
    }

    /*
    public Character parent;

    public HashSet<CharacterAction> CanInitiate()
    {
        if (parent.BasicStats.MoveActions.ActualValue <= 0) return new HashSet<CharacterAction>();
        return new HashSet<CharacterAction>() { CharacterAction.Move };
    }

    public HashSet<CharacterAction> CanResponse()
    {
        return new HashSet<CharacterAction>() {};
    }

    public async Task InitiateAction(CharacterAction action, object arg)
    {
        if (action != CharacterAction.Move) { return; }

        var targetPosition = arg as MapCell;
        await MoveTo(targetPosition.X, targetPosition.Y);
    }

    public Task TakeResponse(CharacterAction action, object arg)
    {
        return new Task(() => {});
    }
    private async Task MoveTo(int targetX, int targetY)
    {
    }
    */
}