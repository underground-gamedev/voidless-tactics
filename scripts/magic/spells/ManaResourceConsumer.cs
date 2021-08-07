using System;
using Godot;

public class ManaResourceConsumer : Node, IResourceConsumer
{
    [Export]
    private int manaRequired; 
    public void Consume(SpellComponentContext ctx)
    {
        ctx.TargetCell.Mana.Consume(manaRequired);
        ctx.Map.ManaLayer.OnSync(ctx.Map);
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return ctx.TargetCell.Mana.ActualValue >= manaRequired;
    }

    public ConsumeTag GetConsumeTags(SpellComponentContext ctx)
    {
        switch(ctx.TargetCell.Mana.ManaType)
        {
            case ManaType.Magma: return ConsumeTag.MagmaMana;
            case ManaType.Nature: return ConsumeTag.NatureMana;
            case ManaType.Water: return ConsumeTag.WaterMana;
            default: return ConsumeTag.None;
        }
    }

    public string GetDescription(Character caster)
    {
        return $"{TextHelpers.GetIconBBCode("1_06")} mana: {manaRequired}";
    }
}