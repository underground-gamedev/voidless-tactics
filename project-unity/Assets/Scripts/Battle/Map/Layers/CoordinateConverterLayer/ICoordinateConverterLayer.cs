using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle.Map.Layers.PresentationLayers
{
    public interface ICoordinateConverterLayer : IMapLayer
    {
        public Vector3 MapToGlobal(MapCell cell);
        public MapCell GlobalToMap(Vector3 position);
    }
}