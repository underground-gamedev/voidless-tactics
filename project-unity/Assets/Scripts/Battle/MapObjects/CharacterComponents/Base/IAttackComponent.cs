using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Battle
{
    public interface IAttackComponent
    {
        bool AttackAvailable();
        List<MapCell> GetAttackArea(MapCell from);
        IEnumerator Attack(ITargetComponent target);
    }
}