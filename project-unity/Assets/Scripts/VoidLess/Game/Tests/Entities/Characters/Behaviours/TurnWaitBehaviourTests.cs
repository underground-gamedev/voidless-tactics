using NUnit.Framework;
using VoidLess.Game.Entities.Characters.Behaviours;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;

namespace VoidLess.Game.Tests.Entities.Characters.Behaviours
{
    public class TurnWaitBehaviourTests
    {
        [Test]
        public void TestWaitBehaviour()
        {
            var minInitiative = 10;
            var maxInitiative = 20;
            
            var waitBehaviour = new TurnWaitBehaviour(minInitiative, maxInitiative);
            
            var waitTriggered = false;
            waitBehaviour.OnWait += initiative => waitTriggered = minInitiative <= initiative && initiative <= maxInitiative;
            
            
            waitBehaviour.Handle(new StartRoundGameEvent());
            
            
            Assert.IsTrue(waitTriggered);
        }
    }
}