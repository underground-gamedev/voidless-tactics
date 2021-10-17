using Battle;
using Moq;
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

        [Test]
        public void TestStatAddition()
        {
            var baseValue = 10;
            var stat = new EntityStat(baseValue);

            Assert.AreEqual(baseValue * 2, (stat + baseValue).BaseValue);
            Assert.AreEqual(baseValue * 3, (stat + baseValue * 2).BaseValue);
        }

        [Test]
        public void TestStatModification()
        {
            var baseValue = 10;
            var stat = new EntityStat(baseValue);

            var mockModifier = new Mock<EntityStatModifier>();
            mockModifier.Setup(modifier => modifier.ModifyValue(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0);
            
            stat.AddModifier(StatModifierSource.Test, mockModifier.Object);
            
            Assert.AreEqual(baseValue, stat.BaseValue);
            Assert.AreEqual(0, stat.Value);
        }
    }
}