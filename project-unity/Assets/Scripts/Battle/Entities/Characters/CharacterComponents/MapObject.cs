using System.Collections;
using UnityEngine;

/*
namespace Battle
{
    public class MapObject : MonoBehaviour
    {
        protected MapCell cell;
        public MapCell Cell => cell;

        protected EditableMapMono MapMono;
        public EditableMapMono MapMono => map;

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

        public void SyncWithMap(EditableMapMono mapMono)
        {
            if (cell == null) return;

            var (x, y) = cell.XY;
            SyncWithMap(mapMono, x, y);
        }

        public void SyncWithMap(EditableMapMono mapMono, int x, int y)
        {
            var pos = new Vector2Int(x, y);
            transform.position = mapMono.MapObjectsBindingsLayer.MapToGlobal(pos);
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

        public void BindMap(EditableMapMono mapMono, MapCell cell)
        {
            if (mapMono != null)
            {
                DetachMap();
            }

            this.map = mapMono;
            SetCell(cell);
            SyncWithMap(mapMono);
        }
    }
}
*/