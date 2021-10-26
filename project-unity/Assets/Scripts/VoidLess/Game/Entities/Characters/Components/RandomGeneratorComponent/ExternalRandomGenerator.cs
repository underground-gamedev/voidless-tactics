using VoidLess.Utils.Random;

namespace VoidLess.Game.Entities.Characters.Components.RandomGeneratorComponent
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