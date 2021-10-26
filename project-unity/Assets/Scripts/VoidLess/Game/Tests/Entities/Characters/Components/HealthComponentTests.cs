using Moq;
using NUnit.Framework;
using VoidLess.Core.Entities;
using VoidLess.Core.Stats;
using VoidLess.Game.Entities.Characters.Components;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;
using VoidLess.Game.Entities.Characters.Components.HealthComponent;
using VoidLess.Game.Entities.Characters.PersonalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;

namespace VoidLess.Game.Tests.Entities.Characters.Components
{
    public class HealthComponentTests
    {
        [Test]
        public void TestDeathEmitOnZeroHealth()
        {
            var mockEmitter = new Mock<IGlobalEventEmitter>();
            
            var character = new Entity();
            character.AddWithAssociation<IGlobalEventEmitter>(mockEmitter.Object);
            character.AddComponent(new HealthComponent(10));


            character.HandleEvent(new TakeHitPersonalEvent(10));

            
            mockEmitter.Verify(emitter => emitter.Emit(It.IsAny<DeathCharacterGameEvent>()), Times.Once());
        }

        [Test]
        public void TestDamagedEmitOnTakeHitSend()
        {
            var character = new Entity();
            var mockEmitter = new Mock<IGlobalEventEmitter>();
            character.AddWithAssociation<IGlobalEventEmitter>(mockEmitter.Object);
            character.AddComponent(new HealthComponent(10));
            
            
            character.HandleEvent(new TakeHitPersonalEvent(5));

            
            mockEmitter.Verify(emitter => emitter.Emit(It.IsAny<DamagedGameEvent>()), Times.Once());
        }

        [Test]
        public void TestCurrentHealthChangedOnTakeHitSend()
        {
            var character = new Entity();
            character.AddComponent(new HealthComponent(10));
            
            var expectedHealth = 5;
            
            
            character.HandleEvent(new TakeHitPersonalEvent(5));
            var stats = character.Stats();
            var actualHealth = stats.Get(StatType.CurrentHealth)?.ModifiedValue;

            
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}