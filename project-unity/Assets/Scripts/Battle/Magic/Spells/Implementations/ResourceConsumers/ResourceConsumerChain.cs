
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class ResourceConsumerChain : ScriptableObject, IResourceConsumer
    {
        [SerializeField]
        private List<IResourceConsumer> consumers;

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

        public string GetDescription(Character caster)
        {
            return string.Join("\n", consumers.Select(consumer => consumer.GetDescription(caster)));
        }
    }
}