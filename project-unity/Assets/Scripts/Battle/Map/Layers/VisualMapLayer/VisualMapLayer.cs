using System;
using Battle.Map.Extensions;
using Battle.Map.Interfaces;
using Core.Components;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Battle
{
    [Require(typeof(ISolidMapLayer))]
    public class VisualMapLayer : MonoBehaviour, IVisualMapLayer
    {
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

        private ILayeredMap map;
        private ISolidMapLayer solid;

        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
            this.solid = map.GetLayer<ISolidMapLayer>();
            RedrawAll();
        }

        public void OnDeAttached()
        {
            this.map = null;
            this.solid = null;
        }

        public void RedrawSingle(MapCell cell)
        {
            var (x, y) = cell.XY;
            var cellSolid = solid.IsSolid(cell);
            var tile = cellSolid ? wallTile : floorTile;
            tilemap.SetTile(new Vector3Int(x, y, 0), tile);

            if (cellSolid) return;

            bool NeedBorder(int offsetX, int offsetY)
            {
                var target = MapCell.FromVector(cell.Pos + new Vector2Int(offsetX, offsetY));
                return map.IsOutOfBounds(target) || solid.IsSolid(target);
            }

            if (NeedBorder(-1, 0))
            {
                xborder.SetTile(new Vector3Int(x, y, 0), borderTile);
            }

            if (NeedBorder(1, 0))
            {
                xborder.SetTile(new Vector3Int(x+1, y, 0), borderTile);
            }

            if (NeedBorder(0, -1))
            {
                yborder.SetTile(new Vector3Int(x, y, 0), borderTile);
            }

            if (NeedBorder(0, 1))
            {
                yborder.SetTile(new Vector3Int(x, y+1, 0), borderTile);
            }
        }

        public void RedrawAll()
        {
            tilemap.ClearAllTiles();
            yborder.ClearAllTiles();
            xborder.ClearAllTiles();

            foreach(var cell in map.Positions())
            {
                RedrawSingle(cell);
            }
        }

    }
}