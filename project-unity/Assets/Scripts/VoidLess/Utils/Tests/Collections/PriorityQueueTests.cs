using NUnit.Framework;
using VoidLess.Utils.Collections;

namespace VoidLess.Utils.Tests.Collections
{
    public class PriorityQueueTests
    {
        [Test]
        public void TestBasicLogic()
        {
            var queue = new PriorityQueue<string>();
            
            Assert.AreEqual(null, queue.Active);
            
            queue.Set("test1", 1);
            Assert.AreEqual("test1", queue.Active);
            
            queue.Set("test2", 2);
            Assert.AreEqual("test2", queue.Active);
            
            queue.Set("test1", 2);
            Assert.AreEqual("test2", queue.Active);
            
            queue.Set("test1", 3);
            Assert.AreEqual("test1", queue.Active);
            
            Assert.AreEqual(2, queue.Elements.Count);
            
            queue.Remove("test1");
            Assert.AreEqual(1, queue.Elements.Count);
            Assert.AreEqual("test2", queue.Active);
        }
    }
}