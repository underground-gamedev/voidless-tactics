using Battle;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
{
    public class BehaviourComponentTests
    {
        public class TestPersonalEvent : IPersonalEvent { }

        public class SecondTestPersonalEvent : IPersonalEvent { }


        [Test]
        public void TestBehaviourEventReaction()
        {
            var behCom = new BehaviourComponent();
            var mockBeh = new Mock<IBehaviour<TestPersonalEvent>>();
            behCom.Add(mockBeh.Object);

            
            behCom.Handle(new TestPersonalEvent());
            
            
            mockBeh.Verify(beh => beh.Handle(It.IsAny<TestPersonalEvent>()), Times.Once());
        }

        [Test]
        public void TestMultipleBehavioursImplementation()
        {
            var behCom = new BehaviourComponent();
            var mockBeh = new Mock<IBehaviour>();
            var firstTestBeh = mockBeh.As<IBehaviour<TestPersonalEvent>>();
            var secondTestBeh = mockBeh.As<IBehaviour<SecondTestPersonalEvent>>();
            
            behCom.Add(mockBeh.Object);
            
            
            behCom.Handle(new TestPersonalEvent());
            behCom.Handle(new SecondTestPersonalEvent());
            
            
            firstTestBeh.Verify(beh => beh.Handle(It.IsAny<TestPersonalEvent>()), Times.Once());
            secondTestBeh.Verify(beh => beh.Handle(It.IsAny<SecondTestPersonalEvent>()), Times.Once());
        }
    }
}