using Battle;
using Battle.Components.InitiativeComponent;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
{
    public class InitiativeComponentTests
    {
        [Test]
        public void TestWaitActionBehaviour()
        {
            var minInitiative = 10;
            var maxInitiative = 20;

            var validWaitEvent = false;
            
            var mockEventEmitter = new Mock<IGlobalEventEmitter>();
            mockEventEmitter
                .Setup(emitter => emitter.Emit(It.IsAny<WaitTurnGameEvent>()))
                .Callback<IGlobalEvent>(globalEvent =>
                    validWaitEvent |= globalEvent is WaitTurnGameEvent waitEvent 
                                   && minInitiative <= waitEvent.Initiative && waitEvent.Initiative <= maxInitiative);
            
            var character = new Character();
            character.AddComponent<InitiativeComponent>(new InitiativeComponent(minInitiative, maxInitiative));
            character.AddComponent<IGlobalEventEmitter>(mockEventEmitter.Object);
            
            
            character.Behaviours.Handle(new StartRoundGameEvent());
            
            
            Assert.IsTrue(validWaitEvent);
        }

        [Test]
        public void TestInitiativeStatsExists()
        {
            var minInitiative = 10;
            var maxInitiative = 20;

            var character = new Character();
            
            
            character.AddComponent<InitiativeComponent>(new InitiativeComponent(minInitiative, maxInitiative));

            
            var stats = character.Stats;
            Assert.AreEqual(minInitiative, stats?.Get(StatType.MinInitiative)?.BaseValue);
            Assert.AreEqual(maxInitiative, stats?.Get(StatType.MaxInitiative)?.BaseValue);
        }
    }
}