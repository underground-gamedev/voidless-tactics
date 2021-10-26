using System.Collections.Generic;
using System.Threading.Tasks;
using VoidLess.Core.Entities;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Base
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