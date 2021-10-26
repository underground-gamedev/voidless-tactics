using VoidLess.Game.Map.Algorithms.AreaPatterns;

namespace VoidLess.Game.Entities.Characters.Components.ActiveSkillComponent
{
    public interface IActiveSkill
    {
        IAreaPattern UsagePattern { get; }
        IAreaPattern TargetPattern { get; }
    }
}