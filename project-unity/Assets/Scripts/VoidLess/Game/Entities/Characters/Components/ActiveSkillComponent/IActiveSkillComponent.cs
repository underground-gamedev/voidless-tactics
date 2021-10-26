using JetBrains.Annotations;
using VoidLess.Core.Components;

namespace VoidLess.Game.Entities.Characters.Components.ActiveSkillComponent
{
    public interface IActiveSkillComponent: IComponent
    {
        void Add(ActiveSkillType skillType, IActiveSkill skill);
        void Remove(ActiveSkillType skillType);
        [CanBeNull] IActiveSkill Get(ActiveSkillType skillType);
    }
}