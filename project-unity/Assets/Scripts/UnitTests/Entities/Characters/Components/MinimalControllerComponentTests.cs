using Battle;
using Battle.Components.MinimalControllerComponent;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
{
    public class MinimalControllerComponentTests
    {
        [Test]
        public void TestSkipTurnAfterStart()
        {
            var endTurnTriggered = false;
            
            var mockEmitter = new Mock<IGlobalEventEmitter>();
            mockEmitter.Setup(emitter => emitter.Emit(It.IsAny<EndTurnGameEvent>()))
                .Callback<IGlobalEvent>(globalEvent => endTurnTriggered |= globalEvent != null);
            
            var character = new Character();
            character.AddComponent<MinimalControllerComponent>(new MinimalControllerComponent());
            character.AddComponent<IGlobalEventEmitter>(mockEmitter.Object);
            
            
            character.Behaviours.Handle(new TakeTurnPersonalEvent());
            
            
            Assert.IsTrue(endTurnTriggered);
        }
    }
}