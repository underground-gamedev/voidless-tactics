using Battle.Algorithms.AreaPatterns;

namespace Battle
{
    public interface IActiveSkill
    {
        IAreaPattern UsagePattern { get; }
        IAreaPattern TargetPattern { get; }
    }
}