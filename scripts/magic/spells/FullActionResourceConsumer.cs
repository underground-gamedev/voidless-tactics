using Godot;

public class FullActionResourceConsumer : Node, IResourceConsumer
{
    [Export]
    private int actionCount; 
    public void Consume(SpellComponentContext ctx)
    {
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return true;
    }

    public ConsumeTag GetConsumeTags(SpellComponentContext ctx)
    {
        return ConsumeAvailable(ctx) ? ConsumeTag.FullAction : ConsumeTag.None;
    }

    public string GetDescription(Character caster)
    {
        return $"{TextHelpers.GetIconBBCode("4_57")} full action: {actionCount}";
    }
}