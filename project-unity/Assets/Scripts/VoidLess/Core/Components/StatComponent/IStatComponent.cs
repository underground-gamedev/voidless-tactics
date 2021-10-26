using JetBrains.Annotations;
using VoidLess.Core.Stats;

namespace VoidLess.Core.Components.StatComponent
{
    public interface IStatComponent: IComponent
    {
        void Add(StatType statType, Stat stat);
        void Remove(StatType statType);
        [CanBeNull] Stat Get(StatType statType);
    }
}