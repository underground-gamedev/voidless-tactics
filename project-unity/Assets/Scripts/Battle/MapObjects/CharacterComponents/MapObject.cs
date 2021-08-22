using System.Collections;
using UnityEngine;

namespace Battle
{
    public class MapObject : MonoBehaviour
    {
        protected MapCell cell;
        public MapCell Cell => cell;

        protected TacticMap map;
        public TacticMap Map => map;

        private Character character;
        public Character AsCharacter => character;

        private void Start()
        {
            character = GetComponent<Character>();
            if (character != null)
            {
                character.DeathTrigger.AddListener(OnCharacterDeath);
            }
        }

        private void OnCharacterDeath()
        {
            SetCell(null);
        }
        //public void SyncWithMap(TacticMap map)
        //{
        //    var (x, y) = cell.XY;
        //    SyncWithMap(map, x, y);
        //}

        //public void SyncWithMap(TacticMap map, int x, int y)
        //{
        //    var tilemap = map.VisualLayer.TileMap;
        //    var offset = tilemap.CellSize * (new Vector2(x, y) + new Vector2(0.5f, 0.5f));
        //    GlobalPosition = tilemap.GlobalPosition + offset;
        //    if (!map.IsOutOfBounds(x, y))
        //    {
        //        ZIndex = map.VisualLayer.GetZ(map, map.CellBy(x, y), 0);
        //    }
        //}

        public void SetCell(MapCell cell)
        {
            if (this.cell != null)
            {
                this.cell.MapObject = null;
            }

            this.cell = cell;

            if (cell != null)
            {
                cell.MapObject = this;
            }
        }

        public void DetachMap()
        {
            if (map != null)
            {
                SetCell(null);
                map = null;
            }
        }

        public void BindMap(TacticMap map, MapCell cell)
        {
            if (map != null)
            {
                DetachMap();
            }

            this.map = map;
            SetCell(cell);
        }
    }
}