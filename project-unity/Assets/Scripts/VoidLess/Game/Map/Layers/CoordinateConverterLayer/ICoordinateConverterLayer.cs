using UnityEngine;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.CoordinateConverterLayer
{
    public interface ICoordinateConverterLayer : IMapLayer
    {
        public Vector3 MapToGlobal(MapCell cell);
        public MapCell GlobalToMap(Vector3 position);
    }
}