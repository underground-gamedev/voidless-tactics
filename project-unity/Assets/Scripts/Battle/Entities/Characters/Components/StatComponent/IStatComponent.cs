using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public interface IStatComponent: IComponent
    {
        void Add(StatType statType, Stat stat);
        void Remove(StatType statType);
        [CanBeNull] Stat Get(StatType statType);
    }
}