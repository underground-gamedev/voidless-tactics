using Battle;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
{
    public class BehaviourComponentTests
    {
        public class FirstTestPersonalEvent : IPersonalEvent { }

        public class SecondTestPersonalEvent : IPersonalEvent { }


        [Test]
        public void TestMultipleBehavioursImplementation()
        {
            var behCom = new BehaviourComponent();
            var mockBeh = new Mock<IBehaviour>();
            var firstTestBeh = mockBeh.As<IBehaviour<FirstTestPersonalEvent>>();
            var secondTestBeh = mockBeh.As<IBehaviour<SecondTestPersonalEvent>>();
            
            behCom.Add(mockBeh.Object);
            
            
            behCom.Handle(new FirstTestPersonalEvent());
            behCom.Handle(new SecondTestPersonalEvent());
            
            
            firstTestBeh.Verify(beh => beh.Handle(It.IsAny<FirstTestPersonalEvent>()), Times.Once());
            secondTestBeh.Verify(beh => beh.Handle(It.IsAny<SecondTestPersonalEvent>()), Times.Once());
        }
    }
}