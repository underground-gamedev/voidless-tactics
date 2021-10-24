using Battle;
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
            
            var character = new Character();
            character.AddComponent<IGlobalEventEmitter>(mockEmitter.Object);
            character.AddComponent(new HealthComponent(10));


            character.HandleEvent(new TakeHitPersonalEvent(10));

            
            mockEmitter.Verify(emitter => emitter.Emit(It.IsAny<DeathCharacterGameEvent>()), Times.Once());
        }

        [Test]
        public void TestDamagedEmitOnTakeHitSend()
        {
            var character = new Character();
            var mockEmitter = new Mock<IGlobalEventEmitter>();
            character.AddComponent<IGlobalEventEmitter>(mockEmitter.Object);
            character.AddComponent(new HealthComponent(10));
            
            
            character.HandleEvent(new TakeHitPersonalEvent(5));

            
            mockEmitter.Verify(emitter => emitter.Emit(It.IsAny<DamagedGameEvent>()), Times.Once());
        }

        [Test]
        public void TestCurrentHealthChangedOnTakeHitSend()
        {
            var character = new Character();
            character.AddComponent(new HealthComponent(10));
            
            var expectedHealth = 5;
            
            
            character.HandleEvent(new TakeHitPersonalEvent(5));
            var actualHealth = character.Stats.Get(StatType.CurrentHealth).ModifiedValue;

            
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}