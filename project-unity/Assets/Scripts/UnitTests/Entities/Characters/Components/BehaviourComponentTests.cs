using System;
using Battle;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
{
    public class BehaviourComponentTests
    {
        [Test]
        public void TestBehaviourEventReaction()
        {
            var behCom = new BehaviourComponent();

            var behaviourTriggered = false;
            var mockBeh = new Mock<IBehaviour>();
            mockBeh
                .Setup(beh => beh.Handle(It.IsAny<IPersonalEvent>()))
                .Callback(() => behaviourTriggered = true);

            mockBeh
                .Setup(beh => beh.RespondTo(It.IsAny<Type>()))
                .Returns(true);
            
            behCom.Add(mockBeh.Object);

            
            var mockEvent = new Mock<IPersonalEvent>();
            behCom.Handle(mockEvent.Object);
            
            
            Assert.IsTrue(behaviourTriggered);
        }
    }
}