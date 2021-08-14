using System.Collections.Generic;
using Godot;

public class TakeDamageSpellEffect : Node, ISpellEffect
{
    [Export]
    private int damage;

    private Stat GetPowerStat(Character caster)
    {
        return caster.BasicStats.SpellPower;
    }

    public void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea, ConsumeTag tag)
    {
        foreach (var cell in effectArea)
        {
            var targetCharacter = cell.MapObject as Character;
            if (targetCharacter is null) continue;

            var targetComponent = targetCharacter.Components.GetComponent<ITargetComponent>();
            if (targetComponent is null) continue;

            targetComponent.TakeDamage(damage + GetPowerStat(ctx.Caster).ActualValue);
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
        var powerStat = GetPowerStat(caster);
        return $"{TextHelpers.GetIconBBCode("1_68")} take damage: {damage}+{TextHelpers.ColorizeStat(powerStat.Name, powerStat.ActualValue)}";
    }
}