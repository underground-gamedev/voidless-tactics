using System;
using Godot;

public class ManaResourceConsumer : Node, IResourceConsumer
{
    [Export]
    private float manaRequired; 
    public void Consume(SpellComponentContext ctx)
    {
        ctx.TargetCell.Mana.Density -= manaRequired;
        ctx.Map.ManaLayer.OnSync(ctx.Map);
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return ctx.TargetCell.Mana.Density >= manaRequired;
    }

    public ConsumeTag GetConsumeTags(SpellComponentContext ctx)
    {
        switch(ctx.TargetCell.Mana.ManaType)
        {
            case ManaType.Fire: return ConsumeTag.FireMana;
            case ManaType.Nature: return ConsumeTag.NatureMana;
            case ManaType.Water: return ConsumeTag.WaterMana;
            default: return ConsumeTag.None;
        }
    }

    public string GetDescription(Character caster)
    {
        return $"{TextHelpers.GetIconBBCode("1_06")} mana: {Math.Ceiling(manaRequired*100)}";
    }
}