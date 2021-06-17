using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAttackComponent
{
    bool AttackAvailable();
    List<MapCell> GetAttackArea();
    Task Attack(ITargetComponent target);
}