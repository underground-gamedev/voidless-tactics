using Battle.Systems.RandomSystem;

namespace Battle.Components.RandomGeneratorComponent
{
    public class ExternalRandomGenerator : IRandomGeneratorComponent
    {
        private IRandomGenerator rand;
        
        public ExternalRandomGenerator(IRandomGenerator rand)
        {
            this.rand = rand;
        }
        
        public uint NextUInt()
        {
            return rand.NextUInt();
        }
    }
}