using System.Collections.Generic;
using VoidLess.Core.Entities;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Base
{
    public interface ISpellEffect
    {
        bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea);
        void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea);
        string GetDescription(IEntity caster);
    }
}
