using System.Collections;
using System.Collections.Generic;
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
        private Tilemap yborder;
        [SerializeField]
        private Tilemap xborder;

        [SerializeField]
        private TileBase wallTile;
        [SerializeField]
        private TileBase floorTile;
        [SerializeField]
        private TileBase borderTile;

        private void Start()
        {
            Redraw();
        }

        public void Redraw()
        {
            tilemap.ClearAllTiles();
            yborder.ClearAllTiles();
            xborder.ClearAllTiles();

            foreach(var cell in map)
            {
                var (x, y) = cell.XY;
                var tile = cell.Solid ? wallTile : floorTile;
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);

                if (cell.Solid) continue;

                if (map.IsOutOfBounds(x-1, y) || map.CellBy(x-1, y).Solid)
                {
                    xborder.SetTile(new Vector3Int(x, y, 0), borderTile);
                }

                if (map.IsOutOfBounds(x+1, y) || map.CellBy(x+1, y).Solid)
                {
                    xborder.SetTile(new Vector3Int(x+1, y, 0), borderTile);
                }

                if (map.IsOutOfBounds(x, y-1) || map.CellBy(x, y-1).Solid)
                {
                    yborder.SetTile(new Vector3Int(x, y, 0), borderTile);
                }

                if (map.IsOutOfBounds(x, y+1) || map.CellBy(x, y+1).Solid)
                {
                    yborder.SetTile(new Vector3Int(x, y+1, 0), borderTile);
                }
            }
        }
    }
}