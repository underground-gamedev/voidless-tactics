using Battle;
using Battle.Map.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests.Map.Layers
{
    public class CharacterLayerTests
    {
        [Test]
        public void TestCharacterAddOnLayer()
        {
            var mapWidth = 3;
            var mapHeight = 3;

            var mockMap = new Mock<ILayeredMap>();
            mockMap.SetupGet(m => m.Height).Returns(mapWidth);
            mockMap.SetupGet(m => m.Width).Returns(mapHeight);
            
            var characterLayer = new CharacterMapLayer();
            characterLayer.OnAttached(mockMap.Object);

            var mockCharacter = new Mock<ICharacter>();
            var spawnPosition = new MapCell(0, 0);
            
            
            characterLayer.AddCharacter(mockCharacter.Object, spawnPosition);


            Assert.AreSame(mockCharacter.Object, characterLayer.GetCharacter(spawnPosition));
            Assert.AreEqual(spawnPosition, characterLayer.GetPosition(mockCharacter.Object));
        }

        [Test]
        public void TestCharacterRemoveFromLayer()
        {
            var mapWidth = 3;
            var mapHeight = 3;

            var mockMap = new Mock<ILayeredMap>();
            mockMap.SetupGet(m => m.Height).Returns(mapWidth);
            mockMap.SetupGet(m => m.Width).Returns(mapHeight);
            
            var characterLayer = new CharacterMapLayer();
            characterLayer.OnAttached(mockMap.Object);

            var mockCharacter = new Mock<ICharacter>();
            var spawnPosition = new MapCell(0, 0);
            
            
            characterLayer.AddCharacter(mockCharacter.Object, spawnPosition);
            characterLayer.RemoveCharacter(mockCharacter.Object);


            Assert.IsNull(characterLayer.GetCharacter(spawnPosition));
        }
    }
}