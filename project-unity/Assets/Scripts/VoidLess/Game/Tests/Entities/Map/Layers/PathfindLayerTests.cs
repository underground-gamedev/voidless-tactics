using System.Linq;
using Moq;
using NUnit.Framework;
using UnityEngine;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.PathfindLayer;
using VoidLess.Game.Map.Layers.SolidMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Tests.Entities.Map.Layers
{
    public class PathfindLayerTests
    {
        [Test]
        public void TestPathfindOnFreeMap()
        {
            /*
             * Map:
             * [s] [ ] [ ]
             * [ ] [ ] [ ]
             * [ ] [ ] [d]
             * s - src
             * d - dest
             */
            
            const int mapWidth = 3;
            const int mapHeight = 3;
            
            var srcPos = new MapCell(0, 0);
            var destPos = new MapCell(2, 2);

            var expected = PathfindResult.Success(
                new[] { new MapCell(0, 0), new MapCell(1, 1), new MapCell(2, 2) },
                2 * Mathf.Sqrt(2));
            
            var mockNonSolidLayer = new Mock<ISolidMapLayer>();
            mockNonSolidLayer
                .Setup(l => l.IsSolid(It.IsAny<MapCell>()))
                .Returns(false);
            
            var mockMap = new Mock<ILayeredMap>();
            mockMap.Setup(m => m.GetLayer<ISolidMapLayer>())
                .Returns(mockNonSolidLayer.Object);
            mockMap.SetupGet(m => m.Height).Returns(mapWidth);
            mockMap.SetupGet(m => m.Width).Returns(mapHeight);
            
            var pathfindLayer = new PathfindLayer();
            pathfindLayer.OnAttached(mockMap.Object);
            
            
            var actual = pathfindLayer.Pathfind(srcPos, destPos);
            

            AssertPathfindResult(expected, actual);
        }
        
        [Test]
        public void TestPathfindOnMapWithWalls()
        {
            /*
             * Map:
             * [s] [*] [ ]
             * [ ] [*] [ ]
             * [ ] [ ] [d]
             * s - src
             * d - dest
             * star - solid
             */
            
            const int mapWidth = 3;
            const int mapHeight = 3;
            
            var solidCells = new[]
            {
                new MapCell(1, 0), new MapCell(1, 1)
            };
            
            var srcPos = new MapCell(0, 0);
            var destPos = new MapCell(2, 2);

            var expected = PathfindResult.Success(
                new[] { new MapCell(0, 0), new MapCell(0, 1), new MapCell(1, 2), new MapCell(2, 2) },
                2 + Mathf.Sqrt(2));
            
            var mockSolidMapLayer = new Mock<ISolidMapLayer>();
            mockSolidMapLayer
                .Setup(l => l.IsSolid(It.IsAny<MapCell>()))
                .Returns<MapCell>(cell => solidCells.Contains(cell));
            
            var mockMap = new Mock<ILayeredMap>();
            mockMap.Setup(m => m.GetLayer<ISolidMapLayer>())
                .Returns(mockSolidMapLayer.Object);
            mockMap.SetupGet(m => m.Height).Returns(mapWidth);
            mockMap.SetupGet(m => m.Width).Returns(mapHeight);

            var pathfindLayer = new PathfindLayer();
            pathfindLayer.OnAttached(mockMap.Object);
            
            
            var actual = pathfindLayer.Pathfind(srcPos, destPos);
            

            AssertPathfindResult(expected, actual);
        }

        [Test]
        public void TestPathfindOnBlockedMap()
        {
            /*
             * Map:
             * [s] [*] [ ]
             * [ ] [*] [ ]
             * [ ] [*] [d]
             * s - src
             * d - dest
             * star - solid
             */
            
            const int mapWidth = 3;
            const int mapHeight = 3;
            
            var solidCells = new[]
            {
                new MapCell(1, 0), new MapCell(1, 1), new MapCell(1, 2),
            };
            
            var srcPos = new MapCell(0, 0);
            var destPos = new MapCell(2, 2);

            var expected = PathfindResult.Fail();
            
            var mockSolidMapLayer = new Mock<ISolidMapLayer>();
            mockSolidMapLayer
                .Setup(l => l.IsSolid(It.IsAny<MapCell>()))
                .Returns<MapCell>(cell => solidCells.Contains(cell));
            
            var mockMap = new Mock<ILayeredMap>();
            mockMap.Setup(m => m.GetLayer<ISolidMapLayer>())
                .Returns(mockSolidMapLayer.Object);
            mockMap.SetupGet(m => m.Height).Returns(mapWidth);
            mockMap.SetupGet(m => m.Width).Returns(mapHeight);

            var pathfindLayer = new PathfindLayer();
            pathfindLayer.OnAttached(mockMap.Object);
            
            
            var actual = pathfindLayer.Pathfind(srcPos, destPos);
            

            AssertPathfindResult(expected, actual);
        }

        [Test]
        public void TestPathAreaOnMapWithWalls()
        {
            /*
             * Map:
             * [s] [*] [ ]
             * [+] [*] [ ]
             * [+] [+] [ ]
             * Distance: 3
             * s - src
             * plus - expected area
             * star - solid
             */
            
            const int mapWidth = 3;
            const int mapHeight = 3;
            
            var solidCells = new[]
            {
                new MapCell(1, 0), new MapCell(1, 1),
            };
            
            var srcPos = new MapCell(0, 0);
            var distance = 3f;

            var expected = new[]
            {
                new MapCell(0, 1), new MapCell(0, 2), new MapCell(1, 2),
            };
            
            var mockSolidMapLayer = new Mock<ISolidMapLayer>();
            mockSolidMapLayer
                .Setup(l => l.IsSolid(It.IsAny<MapCell>()))
                .Returns<MapCell>(cell => solidCells.Contains(cell));
            
            var mockMap = new Mock<ILayeredMap>();
            mockMap.Setup(m => m.GetLayer<ISolidMapLayer>())
                .Returns(mockSolidMapLayer.Object);
            mockMap.SetupGet(m => m.Height).Returns(mapWidth);
            mockMap.SetupGet(m => m.Width).Returns(mapHeight);

            var pathfindLayer = new PathfindLayer();
            pathfindLayer.OnAttached(mockMap.Object);
            
            
            var actual = pathfindLayer.GetAreaByDistance(srcPos, distance);
            
            Assert.AreEqual(expected, actual);
        }
        
        private static void AssertPathfindResult(PathfindResult expected, PathfindResult actual)
        {
            Assert.AreEqual(expected.IsSuccess, actual.IsSuccess, 
                "Success status not equals");
            
            if (expected.IsSuccess)
            {
                Assert.AreEqual(expected.Path, actual.Path, 
                    "Pathes not equals");
                
                Assert.IsTrue(Mathf.Abs(expected.Cost - actual.Cost) < Mathf.Epsilon, 
                    $"Costs not equals. Expected: {expected.Cost:0.###} | Actual: {actual.Cost:0.###}");
            }
        }
    }
}