using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using VoidLess.Core.Components.BehaviourComponent;

namespace VoidLess.Core.Tests.Components
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


        [Test]
        public void TestBehaviourSequence()
        {
            var behCom = new BehaviourComponent();

            var actualCallSequence = new List<IBehaviour>();
            
            var firstMockBeh = new Mock<IBehaviour<FirstTestPersonalEvent>>();
            firstMockBeh.SetupGet(beh => beh.HandlePriority).Returns(0);
            firstMockBeh.Setup(beh => beh.Handle(It.IsAny<FirstTestPersonalEvent>()))
                .Callback(() => actualCallSequence.Add(firstMockBeh.Object));
            
            var secondMockBeh = new Mock<IBehaviour<FirstTestPersonalEvent>>();
            secondMockBeh.SetupGet(beh => beh.HandlePriority).Returns(1);
            secondMockBeh.Setup(beh => beh.Handle(It.IsAny<FirstTestPersonalEvent>()))
                .Callback(() => actualCallSequence.Add(secondMockBeh.Object));
            
            behCom.Add(firstMockBeh.Object);
            behCom.Add(secondMockBeh.Object);

            var expectedCallSequence = new List<IBehaviour>
            {
                secondMockBeh.Object,
                firstMockBeh.Object,
            };

            
            behCom.Handle(new FirstTestPersonalEvent());
            
            
            Assert.AreEqual(expectedCallSequence, actualCallSequence);
        }
    }
}