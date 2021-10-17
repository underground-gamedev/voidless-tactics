using Battle;
using NUnit.Framework;

namespace UnitTests.Entities
{
    public class EntityStatTests
    {
        [Test]
        public void TestUnmodifiedStat()
        {
            var baseValue = 10;
            var stat = new EntityStat(baseValue);

            Assert.AreEqual(baseValue, stat.Value);
            Assert.AreEqual(baseValue, stat.BaseValue);
        }
    }
}