using System.Collections.Generic;

namespace Battle
{
    public class EntityStats: IStatComponent
    {
        private Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
        
        public void Add(StatType statType, Stat stat)
        {
            stats.Add(statType, stat);
        }

        public void Remove(StatType statType)
        {
            stats.Remove(statType);
        }

        public Stat Get(StatType statType)
        {
            return stats.TryGetValue(statType, out var stat) ? stat : null;
        }
    }
}