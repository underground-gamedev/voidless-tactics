using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAttackComponent
{
    bool AttackAvailable();
    List<MapCell> GetAttackArea(MapCell from);
    Task Attack(ITargetComponent target);
}