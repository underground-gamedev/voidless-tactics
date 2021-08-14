using System.Collections.Generic;

public interface ISpellEffect
{
    bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea, ConsumeTag tag);
    void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea, ConsumeTag tag);
    string GetDescription(Character caster);
}