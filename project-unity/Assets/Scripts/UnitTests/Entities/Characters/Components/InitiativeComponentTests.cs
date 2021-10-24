using System;
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

            var character = new Character();
            character.AddComponent<InitiativeComponent>(new InitiativeComponent(minInitiative, maxInitiative));
            
            var mockEventEmitter = new Mock<IGlobalEventEmitter>();
            character.AddComponent<IGlobalEventEmitter>(mockEventEmitter.Object);
            
            
            character.HandleEvent(new StartRoundGameEvent());
            
            
            mockEventEmitter.Verify(
                emitter => emitter.Emit(It.Is<WaitTurnGameEvent>(
                    e => minInitiative <= e.Initiative && e.Initiative <= maxInitiative)), 
                Times.Once());
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