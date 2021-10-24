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
            
            var character = new Entity();
            character.AddComponent(new MinimalControllerComponent());
            character.AddWithAssociation<IGlobalEventEmitter>(mockEmitter.Object);
            
            
            character.HandleEvent(new TakeTurnPersonalEvent());
            
            
            Assert.IsTrue(endTurnTriggered);
        }
    }
}