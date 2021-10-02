using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public interface IActiveSkillComponent: IComponent
    {
        void Add(ActiveSkillType skillType, IActiveSkill skill);
        void Remove(ActiveSkillType skillType);
        [CanBeNull] IActiveSkill Get(ActiveSkillType skillType);
    }
}