using Battle;
using Moq;
using NUnit.Framework;

namespace UnitTests.Entities
{
    public class StatTests
    {
        [Test]
        public void TestUnmodifiedStat()
        {
            var baseValue = 10;
            var stat = new Stat(baseValue);

            Assert.AreEqual(baseValue, stat.ModifiedValue);
            Assert.AreEqual(baseValue, stat.BaseValue);
        }

        [Test]
        public void TestStatBasicOperations()
        {
            var baseValue = 10;
            var stat = new Stat(baseValue);

            Assert.AreEqual(new Stat(baseValue * 2), stat + baseValue);
            Assert.AreEqual(new Stat(baseValue * 3), stat + baseValue * 2);
            Assert.AreEqual(new Stat(0), stat - baseValue);
        }

        [Test]
        public void TestStatModification()
        {
            var baseValue = 10;
            var stat = new Stat(baseValue);

            var mockModifier = new Mock<StatModifier>();
            mockModifier
                .Setup(modifier => modifier.ModifyValue(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0);
            
            
            var modifiedStat = stat.AddModifier(StatModifierSource.Test, mockModifier.Object);
            var unmodifiedStat = modifiedStat.RemoveModifier(StatModifierSource.Test);
            
            
            Assert.AreEqual(baseValue, modifiedStat.BaseValue);
            Assert.AreEqual(0, modifiedStat.ModifiedValue);
            Assert.AreEqual(stat.ModifiedValue, unmodifiedStat.ModifiedValue);
            Assert.AreEqual(baseValue, stat.ModifiedValue);
        }

        [Test]
        public void TestStatModificationAndAddition()
        {
            var baseValue = 10;
            var modificationAddition = 5;
            var stat = new Stat(baseValue);
            
            var mockModifier = new Mock<StatModifier>();
            mockModifier
                .Setup(modifier => modifier.ModifyValue(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((baseValue, modified) => baseValue + modificationAddition);

            
            var statAddition = stat + baseValue;
            var modifiedStat = statAddition.AddModifier(StatModifierSource.Test, mockModifier.Object);
            var statSecondAddition = modifiedStat + baseValue;
            
            
            Assert.AreEqual(baseValue, stat.ModifiedValue);
            Assert.AreEqual(baseValue * 2, statAddition.ModifiedValue);
            Assert.AreEqual(baseValue * 2 + modificationAddition, modifiedStat.ModifiedValue);
            Assert.AreEqual(baseValue * 3 + modificationAddition, statSecondAddition.ModifiedValue);
        }

        [Test]
        public void TestEquality()
        {
            Assert.AreEqual(new Stat(10), new Stat(5) + 5);
            Assert.AreNotEqual(new Stat(10), new Stat(11));
        }
    }
}