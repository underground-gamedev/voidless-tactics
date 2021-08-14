
using System.Collections.Generic;
using System.Linq;
using Godot;

public class ResourceConsumerChain : Node, IResourceConsumer
{
    private List<IResourceConsumer> consumers;

    public override void _Ready()
    {
        consumers = this.GetChilds<IResourceConsumer>(".");
    }
    public void Consume(SpellComponentContext ctx)
    {
        foreach(var consumer in consumers)
        {
            consumer.Consume(ctx);
        }
    }

    public bool ConsumeAvailable(SpellComponentContext ctx)
    {
        return consumers.TrueForAll(consumer => consumer.ConsumeAvailable(ctx));
    }

    public ConsumeTag GetConsumeTags(SpellComponentContext ctx)
    {
        var tags = ConsumeTag.None;
        foreach(var consumer in consumers)
        {
            tags |= consumer.GetConsumeTags(ctx);
        }
        return tags;
    }

    public string GetDescription(Character caster)
    {
        return System.String.Join("\n", consumers.Select(consumer => consumer.GetDescription(caster)));
    }
}