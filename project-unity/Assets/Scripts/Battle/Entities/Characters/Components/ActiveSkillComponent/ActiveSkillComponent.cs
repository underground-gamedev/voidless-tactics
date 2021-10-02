using System.Collections.Generic;

namespace Battle
{
    public class ActiveSkillComponent: IActiveSkillComponent
    {
        private Dictionary<ActiveSkillType, IActiveSkill> skills = new Dictionary<ActiveSkillType, IActiveSkill>();
        
        public void Add(ActiveSkillType skillType, IActiveSkill skill)
        {
            skills.Add(skillType, skill);
        }

        public void Remove(ActiveSkillType skillType)
        {
            skills.Remove(skillType);
        }

        public IActiveSkill Get(ActiveSkillType skillType)
        {
            return skills.TryGetValue(skillType, out var skill) ? skill : null;
        }
    }
}