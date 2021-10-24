using System.Collections.Generic;
using System.Threading.Tasks;

namespace Battle
{
    public interface ISpell
    {
        List<MapCell> GetTargetArea(IEntity caster);
        List<MapCell> GetEffectArea(IEntity caster, MapCell target);
        bool CastAvailable(IEntity caster, MapCell target);
        bool CastAvailable(IEntity caster);
        Task ApplyEffect(IEntity caster, MapCell target);
        string GetDescription(IEntity caster);
    }
}