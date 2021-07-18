using Godot;

public class FullActionResourceConsumer : Node, IResourceConsumer
{
    [Export]
    private int actionCount; 
    public void Consume(SpellComponentContext ctx)
    {
        ctx.Caster.BasicStats.FullActions.ActualValue -= actionCount;
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return ctx.Caster.BasicStats.FullActions.ActualValue >= actionCount;
    }

    public ConsumeTag GetConsumeTags(SpellComponentContext ctx)
    {
        return ConsumeAvailable(ctx) ? ConsumeTag.FullAction : ConsumeTag.None;
    }

    public string GetDescription(Character caster)
    {
        return $"full action: {actionCount}";
    }
}