namespace Battle
{
    public interface IResourceConsumer
    {
        bool ConsumeAvailable(SpellComponentContext ctx);
        void Consume(SpellComponentContext ctx);
        string GetDescription(Character caster);
    }
}