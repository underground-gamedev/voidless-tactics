using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Battle
{
    public class VisualMapLayer : MonoBehaviour
    {
        [SerializeField]
        private TacticMap map;
        [SerializeField]
        private Tilemap tilemap;

        [SerializeField]
        private Tile wallTile;
        [SerializeField]
        private Tile floorTile;
        private void Start()
        {
            Redraw();
        }

        public void Redraw()
        {
            tilemap.ClearAllTiles();
            foreach(var cell in map)
            {
                var (x, y) = cell.XY;
                var tile = cell.Solid ? wallTile : floorTile;
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}