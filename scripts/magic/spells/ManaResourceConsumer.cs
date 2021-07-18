using System;
using Godot;

public class ManaResourceConsumer : Node, IResourceConsumer
{
    [Export]
    private float manaRequired; 
    public void Consume(SpellComponentContext ctx)
    {
        ctx.BaseCell.Mana.Density -= manaRequired;
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return ctx.BaseCell.Mana.Density >= manaRequired;
    }

    public ConsumeTag GetConsumeTags(SpellComponentContext ctx)
    {
        switch(ctx.BaseCell.Mana.ManaType)
        {
            case ManaType.Fire: return ConsumeTag.FireMana;
            case ManaType.Nature: return ConsumeTag.NatureMana;
            case ManaType.Water: return ConsumeTag.WaterMana;
            default: return ConsumeTag.None;
        }
    }

    public string GetDescription(Character caster)
    {
        return $"mana: {Math.Ceiling(manaRequired*100)}";
    }
}