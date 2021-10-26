using Moq;
using NUnit.Framework;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;
using VoidLess.Game.Entities.Characters.Components.MinimalControllerComponent;
using VoidLess.Game.Entities.Characters.PersonalEvents;
using VoidLess.Game.EventSystem.GlobalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;

namespace VoidLess.Game.Tests.Entities.Characters.Components
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