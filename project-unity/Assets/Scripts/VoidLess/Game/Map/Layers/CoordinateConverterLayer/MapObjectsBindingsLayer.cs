using UnityEngine;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.CoordinateConverterLayer
{
    public class MapObjectsBindingsLayer : MonoBehaviour, ICoordinateConverterLayer
    {
        [SerializeField]
        private Grid grid;
        
        private ILayeredMap map;

        public Vector3 MapToGlobal(MapCell position)
        {
            return grid.CellToWorld(new Vector3Int(position.X, position.Y, 0)) + new Vector3(grid.cellSize.x, 0, grid.cellSize.y)/2;
        }

        public MapCell GlobalToMap(Vector3 position)
        {
            var cell = grid.WorldToCell(position);
            return new MapCell(cell.x, cell.z);
        }

        public int GetZOrder(Vector2Int position, int layer)
        {
            var totalCellCount = map.Width * map.Height;
            var layerOffset = totalCellCount * layer;
            var yOffset = position.y * map.Width;
            return layerOffset + yOffset + position.x;
        }

        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
        }

        public void OnDeAttached()
        {
            this.map = null;
        }
    }
}