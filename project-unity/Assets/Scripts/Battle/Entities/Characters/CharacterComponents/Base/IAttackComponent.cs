using System.Collections;
using System.Collections.Generic;
using Battle.Algorithms.AreaPatterns;

namespace Battle
{
    public interface IAttackComponent
    {
        IAreaPattern AttackAreaPattern();
        IAreaPattern AttackTargetPattern();
        
        bool AttackAvailable();
        List<MapCell> GetAttackArea(MapCell from);
        IEnumerator Attack(ITargetComponent target);
    }
}