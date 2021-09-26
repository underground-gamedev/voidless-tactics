using System;
using Battle.Map.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    public class InputHandleLayer : SerializedMonoBehaviour, IInputMapLayer
    {
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private Collider rayCollider;
        [SerializeField]
        private Camera mainCamera;

        private ILayeredMap map;
        public event Action<MapCell, Vector2> OnCellClick;
        public event Action<MapCell, Vector2> OnCellHover;

        private bool lastFramePressed;

        private void Start()
        {
            mainCamera ??= Camera.main;
        }

        private void FixedUpdate()
        {
            if (map == null || mainCamera == null) return;
            if (OnCellClick == null || OnCellHover == null) return;
            
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!rayCollider.Raycast(ray, out var hit, int.MaxValue)) return;
            var cellPosition = grid.WorldToCell(hit.point);
            var worldCell = grid.CellToWorld(cellPosition);
            var offset = hit.point - worldCell;
            var cellSize = grid.cellSize;
            var offsetRelative = new Vector2(offset.x / cellSize.x, offset.y / cellSize.y);
            var cell = MapCell.FromXY(cellPosition.x, cellPosition.y);

            OnCellHover?.Invoke(cell, offsetRelative);
            
            var mousePressed = Input.GetMouseButtonDown(0);
            var isClick = mousePressed && !lastFramePressed;
            if (isClick) OnCellClick?.Invoke(cell, offsetRelative);
            
            lastFramePressed = mousePressed;
        }

        private void OnDestroy()
        {
            OnCellHover = null;
            OnCellClick = null;
        }

        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
        }

        public void OnDeAttached()
        {
            this.map = null;
            OnCellClick = null;
            OnCellHover = null;
        }
    }
}