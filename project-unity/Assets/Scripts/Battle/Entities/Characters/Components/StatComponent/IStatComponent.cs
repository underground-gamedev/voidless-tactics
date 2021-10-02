using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public interface IStatComponent: IComponent
    {
        void Add(StatType statType, EntityStat stat);
        void Remove(StatType statType);
        [CanBeNull] EntityStat Get(StatType statType);
    }
}