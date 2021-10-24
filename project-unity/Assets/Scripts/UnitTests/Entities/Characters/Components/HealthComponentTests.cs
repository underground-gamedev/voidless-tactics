using Battle;
using Battle.Components;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
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
            var stats = character.GetStatComponent();
            var actualHealth = stats.Get(StatType.CurrentHealth)?.ModifiedValue;

            
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}