using Battle;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities
{
    public class EntityTests
    {
        public class TestEvent : IPersonalEvent { }

        [Test]
        public void TestHandleEvent()
        {
            var entity = new Entity();
            var mockBehaviour = new Mock<IBehaviour<TestEvent>>();
            entity.AddBehaviour(mockBehaviour.Object);
            
            
            entity.HandleEvent(new TestEvent());
            
            
            mockBehaviour.Verify(beh => beh.Handle(It.IsAny<TestEvent>()), Times.Once());
        }
    }
}