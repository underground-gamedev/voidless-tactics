using System.Collections;
using UnityEngine;

namespace Battle
{
    public class MapObjectsBindingsLayer : MonoBehaviour
    {
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private TacticMap map;

        public Vector3 MapToGlobal(Vector2Int position)
        {
            return grid.CellToWorld(new Vector3Int(position.x, position.y, 0)) + new Vector3(grid.cellSize.x, 0, grid.cellSize.y)/2;
        }

        public Vector2Int GlobalToMap(Vector3 position)
        {
            var cell = grid.WorldToCell(position);
            return new Vector2Int(cell.x, cell.z);
        }

        public int GetZOrder(Vector2Int position, int layer)
        {
            var totalCellCount = map.Width * map.Height;
            var layerOffset = totalCellCount * layer;
            var yOffset = position.y * map.Width;
            return layerOffset + yOffset + position.x;
        }
    }
}