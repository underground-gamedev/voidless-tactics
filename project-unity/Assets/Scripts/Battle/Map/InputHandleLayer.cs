using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Battle
{
    public class InputHandleLayer : MonoBehaviour
    {
        [SerializeField]
        private TacticMap map;
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private Collider rayCollider;

        public UnityAction<MapCell, Vector2> OnCellClick;
        public UnityAction OnClickOutOfBounds;

        public UnityAction<MapCell, Vector2> OnCellHover;
        public UnityAction OnHoverOutOfBounds;

        private bool lastFramePressed;


        private void Start()
        {
            map ??= GetComponent<TacticMap>();
        }

        private void FixedUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!rayCollider.Raycast(ray, out hit, int.MaxValue)) return;
            var cellPosition = grid.WorldToCell(hit.point);
            var worldCell = grid.CellToWorld(cellPosition);
            var offset = hit.point - worldCell;
            var offsetRelative = new Vector2(offset.x / grid.cellSize.x, offset.y / grid.cellSize.y);

            var mousePressed = Input.GetMouseButtonDown(0);
            var outOfBounds = map.IsOutOfBounds(cellPosition.x, cellPosition.y);
            var isClick = mousePressed && !lastFramePressed;

            if (outOfBounds)
            {
                OnHoverOutOfBounds?.Invoke();
                if (isClick) OnClickOutOfBounds?.Invoke();
            }
            else
            {
                var cell = map.CellBy(cellPosition.x, cellPosition.y);
                OnCellHover?.Invoke(cell, offsetRelative);
                if (isClick) OnCellClick?.Invoke(cell, offsetRelative);
            }
            lastFramePressed = mousePressed;
        }
    }
}