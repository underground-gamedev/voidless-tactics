using Battle;
using Battle.Map.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTests.Map.Layers
{
    public class CharacterLayerTests
    {
        private ILayeredMap GetTestMap()
        {
            var mapWidth = 3;
            var mapHeight = 3;

            var mockMap = new Mock<ILayeredMap>();
            mockMap.SetupGet(m => m.Height).Returns(mapWidth);
            mockMap.SetupGet(m => m.Width).Returns(mapHeight);

            return mockMap.Object;
        }
        
        [Test]
        public void TestCharacterAddOnLayer()
        {
            var characterLayer = new CharacterMapLayer();
            characterLayer.OnAttached(GetTestMap());

            var mockCharacter = new Mock<ICharacter>();
            var spawnPosition = new MapCell(0, 0);

            var characterAddTriggered = false;
            characterLayer.OnCharacterAdded += character => characterAddTriggered |= character == mockCharacter.Object;
            
            
            characterLayer.AddCharacter(mockCharacter.Object, spawnPosition);

            
            Assert.IsTrue(characterAddTriggered);
            Assert.AreSame(mockCharacter.Object, characterLayer.GetCharacter(spawnPosition));
            Assert.AreEqual(spawnPosition, characterLayer.GetPosition(mockCharacter.Object));
        }

        [Test]
        public void TestCharacterRemoveFromLayer()
        {
            var characterLayer = new CharacterMapLayer();
            characterLayer.OnAttached(GetTestMap());

            var mockCharacter = new Mock<ICharacter>();
            var spawnPosition = new MapCell(0, 0);

            var characterRemoveTriggered = false;
            characterLayer.OnCharacterRemoved += character => characterRemoveTriggered |= character == mockCharacter.Object;
            
            
            characterLayer.AddCharacter(mockCharacter.Object, spawnPosition);
            characterLayer.RemoveCharacter(mockCharacter.Object);


            Assert.IsTrue(characterRemoveTriggered);
            Assert.IsNull(characterLayer.GetCharacter(spawnPosition));
            Assert.IsNull(characterLayer.GetPosition(mockCharacter.Object));
        }

        [Test]
        public void TestCharacterRelocateOnLayer()
        {
            var map = GetTestMap();
            var characterLayer = new CharacterMapLayer();
            characterLayer.OnAttached(map);

            var mockCharacter = new Mock<ICharacter>();
            var spawnPosition = new MapCell(0, 0);
            var relocatePosition = new MapCell(map.Width - 1, map.Height - 1);

            var characterRelocateTriggered = false;
            characterLayer.OnCharacterRelocated += 
                (character, cell) => characterRelocateTriggered |= character == mockCharacter.Object && cell == relocatePosition;
            
            
            characterLayer.AddCharacter(mockCharacter.Object, spawnPosition);
            characterLayer.RelocateCharacter(mockCharacter.Object, relocatePosition);
            
            
            Assert.IsTrue(characterRelocateTriggered);
            Assert.IsNull(characterLayer.GetCharacter(spawnPosition));
            Assert.AreSame(mockCharacter.Object, characterLayer.GetCharacter(relocatePosition));
            Assert.AreEqual(relocatePosition, characterLayer.GetPosition(mockCharacter.Object));
        }
    }
}