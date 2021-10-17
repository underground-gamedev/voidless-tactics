using System;
using Battle;
using NUnit.Framework;

namespace UnitTests.Entities.Characters.Components
{
    public class StatComponentTests
    {
        [Test]
        public void TestAddStat()
        {
            var statCom = new EntityStats();
            var stat = new Stat(10);
            
            
            statCom.Add(StatType.Test, stat);
            
            
            Assert.AreEqual(stat, statCom.Get(StatType.Test));
        }

        [Test]
        public void TestDoubleAddStat()
        {
            var statCom = new EntityStats();
            var stat = new Stat(10);
            
            
            statCom.Add(StatType.Test, stat);
            Assert.Throws<ArgumentException>(() => statCom.Add(StatType.Test, stat));
        }

        [Test]
        public void TestSetStat()
        {
            var statCom = new EntityStats();
            var stat = new Stat(10);

            
            statCom.Set(StatType.Test, stat);
            
            
            Assert.AreEqual(stat, statCom.Get(StatType.Test));
        }

        [Test]
        public void TestRemoveStat()
        {
            var statCom = new EntityStats();
            var stat = new Stat(10);

            
            statCom.Add(StatType.Test, stat);
            statCom.Remove(StatType.Test);
            
            
            Assert.IsNull(statCom.Get(StatType.Test));
        }
    }
}