using System;
using Godot;

public class ManaResourceConsumer : Node, IResourceConsumer
{
    [Export]
    private int manaRequired; 

    private SpellComponent GetSpellComponent(SpellComponentContext ctx)
    {
        return ctx.Caster.Components.GetComponent<SpellComponent>();
    }
    public void Consume(SpellComponentContext ctx)
    {
        GetSpellComponent(ctx).Consume(manaRequired);
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return GetSpellComponent(ctx).ManaCount >= manaRequired;
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