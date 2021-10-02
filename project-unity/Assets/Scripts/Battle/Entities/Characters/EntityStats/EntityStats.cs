using System.Collections.Generic;

namespace Battle
{
    public class EntityStats: IStatComponent
    {
        private Dictionary<StatType, EntityStat> stats = new Dictionary<StatType, EntityStat>();
        
        public void Add(StatType statType, EntityStat stat)
        {
            stats.Add(statType, stat);
        }

        public void Remove(StatType statType)
        {
            stats.Remove(statType);
        }

        public EntityStat Get(StatType statType)
        {
            return stats.TryGetValue(statType, out var stat) ? stat : null;
        }
    }
}