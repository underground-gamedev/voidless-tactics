using System;
using Godot;

public class ManaResourceConsumer : Node, IResourceConsumer
{
    [Export]
    private int manaRequired; 

    public void Consume(SpellComponentContext ctx)
    {
        var manaContainer = ctx.Caster.GetManaContainerComponent();
        manaContainer.ConsumeMana(manaContainer.ManaCount);
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return ctx.Caster.GetManaContainerComponent().ManaCount >= manaRequired;
    }

    public ConsumeTag GetConsumeTags(SpellComponentContext ctx)
    {
        return ConsumeTag.None;
    }

    public string GetDescription(Character caster)
    {
        return $"{TextHelpers.GetIconBBCode("1_06")} mana: {manaRequired}";
    }
}