using Battle;
using Core.Components;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities
{
    public class EntityTests
    {

        [Test]
        public void TestHandleEvent()
        {
            IEntity entity = new Entity();
            var mockBehaviour = new Mock<IBehaviour<TestEvent>>();
            
            
            entity.AddBehaviour(mockBehaviour.Object);
            entity.HandleEvent(new TestEvent());
            entity.RemoveBehaviour(mockBehaviour.Object);
            entity.HandleEvent(new TestEvent());
            
            
            mockBehaviour.Verify(beh => beh.Handle(It.IsAny<TestEvent>()), Times.Once());
        }

       
        [Test]
        public void TestComponents()
        {
            var entity = new Entity();
            var testComponent = new TestComponent();
            
            
            entity.AddComponent(testComponent);
            entity.AssociateComponent(typeof(TestComponent), typeof(ITestAssociation));
            
            
            Assert.AreSame(testComponent, entity.GetComponent<TestComponent>());
            Assert.AreSame(testComponent, entity.GetComponent<ITestAssociation>());
            Assert.IsNull(entity.GetComponent<ITestNonAssociation>());
        }
        
        public class TestEvent : IPersonalEvent { }
        public interface ITestAssociation { }
        public interface ITestNonAssociation { }
        public class TestComponent : IComponent, ITestAssociation, ITestNonAssociation { }
                
    }
}