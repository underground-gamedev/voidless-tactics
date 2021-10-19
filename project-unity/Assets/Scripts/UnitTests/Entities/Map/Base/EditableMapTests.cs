#define CHECK_COM_REQUIREMENTS

using System.ComponentModel;
using System.IO;
using Battle;
using Battle.Map.Interfaces;
using Core.Components;
using Moq;
using NUnit.Framework;

namespace UnitTests.Map.Base
{
    public class EditableMapTests
    {
        [Test]
        public void TestOnAttachedCall()
        {
            var map = new EditableMap(3, 3);
            var mapLayer = new Mock<ISolidMapLayer>();
            
            ILayeredMap attachedMap = null;
            mapLayer
                .Setup(l => l.OnAttached(It.IsAny<ILayeredMap>()))
                .Callback<ILayeredMap>(newMap => { attachedMap = newMap; });
            
            
            map.AddLayer<ISolidMapLayer>(mapLayer.Object);
            
            
            Assert.AreSame(map, attachedMap);
        }

        [Test]
        public void TestOnDeAttachedCall()
        {
            var map = new EditableMap(3, 3);
            var mapLayer = new Mock<ISolidMapLayer>();
            
            var isDeAttached = false;
            mapLayer
                .Setup(l => l.OnDeAttached())
                .Callback(() => isDeAttached = true);
            

            map.AddLayer<ISolidMapLayer>(mapLayer.Object);
            map.RemoveLayer<ISolidMapLayer>();
            
            
            Assert.IsTrue(isDeAttached);
        }

        [Test]
        public void TestGetLayerOnUnExistsLayer()
        {
            var map = new EditableMap(3, 3);


            var solidLayer = map.GetLayer<ISolidMapLayer>();
            
            
            Assert.IsNull(solidLayer);
        }

        [Test]
        public void TestGetLayerOnExistsLayer()
        {
            var map = new EditableMap(3, 3);
            var mockSolidLayer = new Mock<ISolidMapLayer>();
            var expectedSolidLayer = mockSolidLayer.Object;

            
            map.AddLayer<ISolidMapLayer>(expectedSolidLayer);
            var actualSolidLayer = map.GetLayer<ISolidMapLayer>();
            
            
            Assert.AreSame(expectedSolidLayer, actualSolidLayer);
        }

        [Test]
        public void TestGetRemovedLayer()
        {
            var map = new EditableMap(3, 3);
            var mapLayer = new Mock<ISolidMapLayer>();
            
            
            map.AddLayer<ISolidMapLayer>(mapLayer.Object);
            map.RemoveLayer<ISolidMapLayer>();
            var solidLayer = map.GetLayer<ISolidMapLayer>();

            
            Assert.IsNull(solidLayer);
        }
        
        [Test]
        public void TestRequireUnExistsLayer()
        {
            var map = new EditableMap(3, 3);
            var mockMapLayer = new Mock<IPathfindMapLayer>();
            var mockType = mockMapLayer.Object.GetType();
            var requirements = new RequireAttribute(typeof(ISolidMapLayer));
            var provider = TypeDescriptor.AddAttributes(mockType, requirements);
            
            Assert.Throws<InvalidDataException>(() =>
            {
                map.AddLayer<IPathfindMapLayer>(mockMapLayer.Object);
            });
            
            TypeDescriptor.RemoveProvider(provider, mockType);
        }
    }
}
