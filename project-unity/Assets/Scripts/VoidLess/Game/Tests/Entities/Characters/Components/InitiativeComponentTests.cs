using Moq;
using NUnit.Framework;
using VoidLess.Core.Entities;
using VoidLess.Core.Stats;
using VoidLess.Game.Entities.Characters.Components;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;
using VoidLess.Game.Entities.Characters.Components.InitiativeComponent;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;

namespace VoidLess.Game.Tests.Entities.Characters.Components
{
    public class InitiativeComponentTests
    {
        [Test]
        public void TestWaitActionBehaviour()
        {
            var minInitiative = 10;
            var maxInitiative = 20;

            var character = new Entity();
            character.AddComponent(new InitiativeComponent(minInitiative, maxInitiative));
            
            var mockEventEmitter = new Mock<IGlobalEventEmitter>();
            character.AddWithAssociation<IGlobalEventEmitter>(mockEventEmitter.Object);
            
            
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

            var character = new Entity();
            
            
            character.AddComponent(new InitiativeComponent(minInitiative, maxInitiative));

            
            var stats = character.Stats();
            Assert.AreEqual(minInitiative, stats?.Get(StatType.MinInitiative)?.BaseValue);
            Assert.AreEqual(maxInitiative, stats?.Get(StatType.MaxInitiative)?.BaseValue);
        }
    }
}