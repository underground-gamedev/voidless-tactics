using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;
using VoidLess.Game.EventSystem.GlobalEvents.UtilityEvents;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.InputEmitterLayer
{
    public class InputEmitterLayer : MonoBehaviour, IMapLayer
    {
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private Collider rayCollider;
        [SerializeField]
        private Camera mainCamera;

        private ILayeredMap map;

        private bool lastFramePressed;
        private Vector3 lastHoveredPoint;

        private void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (map == null || mainCamera == null) return;
            
            var emitter = map.GetComponent<IGlobalEventEmitter>();
            if (emitter == null) return;
            
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!rayCollider.Raycast(ray, out var hit, int.MaxValue)) return;
            
            var gridCellPosition = grid.WorldToCell(hit.point);
            var gridCellOrigin = grid.CellToWorld(gridCellPosition);
            
            var absoluteOffset = hit.point - gridCellOrigin;
            var gridCellSize = grid.cellSize;
            var relativeOffset = new Vector2(absoluteOffset.x / gridCellSize.x, absoluteOffset.z / gridCellSize.z);
            
            var targetMapCell = MapCell.FromXY(gridCellPosition.x, gridCellPosition.y);

            if (lastHoveredPoint != hit.point)
            {
                emitter.Emit(new HoverOnCellUtilityEvent(map, targetMapCell, relativeOffset));
                lastHoveredPoint = hit.point;
            }
            
            var mousePressed = Input.GetMouseButtonDown(0);
            var isClick = !mousePressed && lastFramePressed;
            if (isClick)
            {
                emitter.Emit(new ClickOnCellUtilityEvent(map, targetMapCell, relativeOffset));
            }
            
            lastFramePressed = mousePressed;
        }

        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
        }

        public void OnDeAttached()
        {
            this.map = null;
        }
    }
}