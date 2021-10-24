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
            var rawCom = entity.GetComponent<TestComponent>();
            entity.Associate<ITestAssociation, TestComponent>();
            var associatedCom = entity.GetComponent<ITestAssociation>();

            entity.Associate<ITestNonAssociation, TestComponent>();
            var secondAssociatedCom = entity.GetComponent<ITestAssociation>();
            entity.RemoveAssociation<ITestNonAssociation>();
            var secondAfterRemove = entity.GetComponent<ITestNonAssociation>();

            entity.RemoveComponent(rawCom);
            var comAfterRemove = entity.GetComponent<TestComponent>();


            Assert.AreSame(testComponent, rawCom);
            Assert.AreSame(testComponent, associatedCom);
            Assert.AreSame(testComponent, secondAssociatedCom);
            Assert.IsNull(secondAfterRemove);
            Assert.IsNull(comAfterRemove);
        }

        [Test]
        public void TestEntityArchtype()
        {
            var entity = new Entity();
            entity.AddWithAssociation<ITestAssociation>(new TestComponent());


            Assert.IsTrue(entity.Correspond(
                Archtype.New()
                    .With<ITestAssociation>()
                    .Build()
                ));
            
            Assert.IsFalse(entity.Correspond(
                Archtype.New()
                    .With<ITestNonAssociation>()
                    .Build()
                ));
        }

        public class TestEvent : IPersonalEvent
        {
        }

        public interface ITestAssociation
        {
        }

        public interface ITestNonAssociation
        {
        }

        public class TestComponent : IComponent, ITestAssociation, ITestNonAssociation
        {
        }
    }
}