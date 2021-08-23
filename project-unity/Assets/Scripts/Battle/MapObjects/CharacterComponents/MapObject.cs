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

        public void SyncWithMap(TacticMap map)
        {
            if (cell == null) return;

            var (x, y) = cell.XY;
            SyncWithMap(map, x, y);
        }

        public void SyncWithMap(TacticMap map, int x, int y)
        {
            var pos = new Vector2Int(x, y);
            transform.position = map.MapObjectsBindingsLayer.MapToGlobal(pos);
        }

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
            SyncWithMap(map);
        }
    }
}