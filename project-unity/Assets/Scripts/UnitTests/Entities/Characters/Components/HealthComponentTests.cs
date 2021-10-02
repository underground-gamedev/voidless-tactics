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
            var deathTriggered = false;
            
            var mockEmitter = new Mock<IGlobalEventEmitter>();
            mockEmitter.Setup(emitter => emitter.Emit(It.IsAny<DeathCharacterGlobalEvent>()))
                .Callback<IGlobalEvent>(globalEvent => deathTriggered |= globalEvent != null);
            
            var character = new Character();
            character.AddComponent<IGlobalEventEmitter>(mockEmitter.Object);
            character.AddComponent<HealthComponent>(new HealthComponent(10));
            
            
            character.Behaviours.Handle(new TakeHitPersonalEvent(10));

            
            Assert.IsTrue(deathTriggered);
        }

        [Test]
        public void TestDamagedEmitOnTakeHitSend()
        {
            var damagedTriggered = false;
            
            var mockEmitter = new Mock<IGlobalEventEmitter>();
            mockEmitter.Setup(emitter => emitter.Emit(It.IsAny<DamagedGlobalEvent>()))
                .Callback<IGlobalEvent>(globalEvent => damagedTriggered |= globalEvent != null);
            
            var character = new Character();
            character.AddComponent<IGlobalEventEmitter>(mockEmitter.Object);
            character.AddComponent<HealthComponent>(new HealthComponent(10));
            
            
            character.Behaviours.Handle(new TakeHitPersonalEvent(5));

            
            Assert.IsTrue(damagedTriggered);
        }

        [Test]
        public void TestCurrentHealthChangedOnTakeHitSend()
        {
            var character = new Character();
            character.AddComponent<HealthComponent>(new HealthComponent(10));
            
            var expectedHealth = 5;
            
            
            character.Behaviours.Handle(new TakeHitPersonalEvent(5));
            var actualHealth = character.Stats.Get(StatType.CurrentHealth).Value;

            
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}