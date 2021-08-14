public interface IResourceConsumer
{
    bool ConsumeAvailable(SpellComponentContext ctx);
    ConsumeTag GetConsumeTags(SpellComponentContext ctx);
    void Consume(SpellComponentContext ctx);
    string GetDescription(Character caster);
}