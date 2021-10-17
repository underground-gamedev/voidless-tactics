using Battle;
using Moq;
using NUnit.Framework;
using UnityEditor.VersionControl;

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
            
            var modifiedStat = stat.AddModifier(StatModifierSource.Test, mockModifier.Object);
            var unmodifiedStat = modifiedStat.RemoveModifier(StatModifierSource.Test);
            
            Assert.AreEqual(baseValue, modifiedStat.BaseValue);
            Assert.AreEqual(0, modifiedStat.Value);
            Assert.AreEqual(stat.Value, unmodifiedStat.Value);
            Assert.AreEqual(baseValue, stat.Value);
        }

        [Test]
        public void TestStatModificationAndAddition()
        {
            var baseValue = 10;
            var modificationAddition = 5;
            var stat = new EntityStat(baseValue);
            
            var mockModifier = new Mock<EntityStatModifier>();
            mockModifier
                .Setup(modifier => modifier.ModifyValue(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((baseValue, modified) => baseValue + modificationAddition);

            
            var statAddition = stat + baseValue;
            var modifiedStat = statAddition.AddModifier(StatModifierSource.Test, mockModifier.Object);
            var statSecondAddition = modifiedStat + baseValue;
            
            
            Assert.AreEqual(baseValue, stat.Value);
            Assert.AreEqual(baseValue * 2, statAddition.Value);
            Assert.AreEqual(baseValue * 2 + modificationAddition, modifiedStat.Value);
            Assert.AreEqual(baseValue * 3 + modificationAddition, statSecondAddition.Value);
        }
    }
}