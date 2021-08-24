using UnityEngine;

namespace Battle
{
    public class ManaResourceConsumer : ScriptableObject, IResourceConsumer
    {
        [SerializeField]
        private int manaRequired; 

        public void Consume(SpellComponentContext ctx)
        {
            var manaContainer = ctx.Caster.ManaContainerComponent;
            manaContainer.ConsumeMana(manaContainer.ManaCount);
        }

        public bool ConsumeAvailable(SpellComponentContext ctx)
        {
            return ctx.Caster.ManaContainerComponent.ManaCount >= manaRequired;
        }

        public string GetDescription(Character caster)
        {
            return $"mana: {manaRequired}";
        }
    }
}