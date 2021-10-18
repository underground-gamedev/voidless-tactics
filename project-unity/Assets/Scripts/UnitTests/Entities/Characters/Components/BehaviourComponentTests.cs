using System;
using System.Linq.Expressions;
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

            var mockBeh = new Mock<IBehaviour>();
            mockBeh
                .Setup(beh => beh.Handle(It.IsAny<IPersonalEvent>()))
                .Verifiable();
            
            behCom.Add(mockBeh.Object);

            
            var mockEvent = new Mock<IPersonalEvent>();
            behCom.Handle(mockEvent.Object);
            
            
            mockBeh.Verify();
        }
    }
}