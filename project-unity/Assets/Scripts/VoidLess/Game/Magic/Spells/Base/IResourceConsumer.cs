using VoidLess.Core.Entities;

namespace VoidLess.Game.Magic.Spells.Base
{
    public interface IResourceConsumer
    {
        bool ConsumeAvailable(SpellComponentContext ctx);
        void Consume(SpellComponentContext ctx);
        string GetDescription(IEntity caster);
    }
}