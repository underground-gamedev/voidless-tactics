using System;
using System.Linq.Expressions;
using Battle;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
{
    public class BehaviourComponentTests
    {
        public class TestPersonalEvent : IPersonalEvent { }
        
        [Test]
        public void TestBehaviourEventReaction()
        {
            var behCom = new BehaviourComponent();
            var mockBeh = new Mock<IBehaviour<TestPersonalEvent>>();
            behCom.Add(mockBeh.Object);


            var testEvent = new TestPersonalEvent();
            behCom.Handle(testEvent);
            
            
            mockBeh.Verify(beh => beh.Handle(It.IsAny<TestPersonalEvent>()), Times.Once());
        }
    }
}