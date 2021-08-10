using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AttackComponent : Node, IAttackComponent
{
    private MapObject mapObject;
    private BasicStats basicStats;

    public async Task Attack(ITargetComponent target)
    {
        await target.TakeDamage(basicStats.Damage.ActualValue);
    }

    public bool AttackAvailable()
    {
        return true;
    }

    public List<MapCell> GetAttackArea(MapCell from)
    {
        var (x, y) = from.Position;
        var attackArea = mapObject.Map.AllNeighboursFor(x, y);
        return attackArea;
    }

    public override void _Ready()
    {
        mapObject = this.FindParent<MapObject>();
        basicStats = (mapObject as IBasicStatsPresenter).BasicStats;
    }
}