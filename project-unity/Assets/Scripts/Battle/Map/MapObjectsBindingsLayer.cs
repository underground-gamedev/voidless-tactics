using System.Collections;
using UnityEngine;

namespace Battle
{
    public class MapObjectsBindingsLayer : MonoBehaviour
    {
        [SerializeField]
        private Vector2 offset;
        [SerializeField]
        private Vector2 cellSize;
        [SerializeField]
        private TacticMap map;

        public Vector3 MapToGlobal(Vector2Int position)
        {
            var basePos = new Vector2(position.x, position.y);
            var result2d = offset + basePos * cellSize + cellSize/2;
            return result2d;
        }

        public Vector2Int GlobalToMap(Vector3 position)
        {
            var pos2d = new Vector2(position.x, position.z);
            var fixedPos = pos2d - offset;
            var local = fixedPos / cellSize;
            return new Vector2Int((int)local.x, (int)local.y);
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