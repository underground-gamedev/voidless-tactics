using System.Collections.Generic;
using VoidLess.Core.Stats;

namespace VoidLess.Core.Components.StatComponent
{
    public class StatComponent: IStatComponent
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

        public void Set(StatType test, Stat stat)
        {
            stats[test] = stat;
        }
    }
}