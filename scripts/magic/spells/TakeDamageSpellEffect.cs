using System.Collections.Generic;
using Godot;

public class TakeDamageSpellEffect : Node, ISpellEffect
{
    [Export]
    private int damage;

    public void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea, ConsumeTag tag)
    {
        foreach (var cell in effectArea)
        {
            var targetCharacter = cell.MapObject as Character;
            if (targetCharacter is null) continue;

            var targetComponent = targetCharacter.Components.FindChild<ITargetComponent>();
            if (targetComponent is null) continue;

            targetComponent.TakeDamage(damage);
        }
    }

    public bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea, ConsumeTag tag)
    {
        foreach (var effectTarget in effectArea)
        {
            var targetCharacter = effectTarget.MapObject as Character;
            if (targetCharacter is null) continue;
            if (targetCharacter.Controller == ctx.Caster.Controller) continue;
            return true;
        }

        return false;
    }

    public string GetDescription(Character caster)
    {
        return $"take damage: {damage}";
    }
}