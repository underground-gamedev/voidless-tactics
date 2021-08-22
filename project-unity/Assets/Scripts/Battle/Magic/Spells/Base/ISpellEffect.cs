using System.Collections.Generic;

namespace Battle
{
    public interface ISpellEffect
    {
        bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea);
        void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea);
        string GetDescription(Character caster);
    }
}
