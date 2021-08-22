using System.Collections.Generic;
using System.Threading.Tasks;

namespace Battle
{
    public interface ISpell
    {
        List<MapCell> GetTargetArea(Character caster);
        List<MapCell> GetEffectArea(Character caster, MapCell target);
        bool CastAvailable(Character caster, MapCell target);
        bool CastAvailable(Character caster);
        Task ApplyEffect(Character caster, MapCell target);
        string GetDescription(Character caster);
    }
}