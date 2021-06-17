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
        basicStats.FullActions.ActualValue -= 1;
        await target.TakeDamage(basicStats.Damage.ActualValue);
    }

    public bool AttackAvailable()
    {
        return basicStats.FullActions.ActualValue > 0;
    }

    public List<MapCell> GetAttackArea()
    {
        var (x, y) = mapObject.Cell.Position;
        var attackArea = mapObject.Map.DirectNeighboursFor(x, y);
        return attackArea;
    }

    public override void _Ready()
    {
        mapObject = this.FindParent<MapObject>();
        basicStats = (mapObject as IBasicStatsPresenter).BasicStats;
    }
}