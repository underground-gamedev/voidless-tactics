using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class MoveComponent : MonoBehaviour, IMoveComponent
    {
        private MapObject mapObject;
        private BasicCharacterStats basicStats;
        private bool moved;

        public void Start()
        {
            mapObject = GetComponent<MapObject>();
            basicStats = GetComponent<BasicCharacterStats>();
        }

        public List<MapCell> GetMoveArea()
        {
            var map = mapObject.Map;
            var pathfinder = mapObject.Map.PathfindLayer;

            var normalMoveCells = pathfinder
                .GetAreaByDistance(mapObject.Cell, basicStats.Speed.Value)
                .Where(cell => cell.MapObject == null);

            return normalMoveCells.ToList();
        }

        public bool MoveAvailable()
        {
            return true;
        }

        public IEnumerator MoveTo(MapCell target)
        {
            if (moved) yield break;
            if (!MoveAvailable()) yield break;
            var currentCell = mapObject.Cell;
            if (currentCell == target) yield break;

            int x = currentCell.X;
            int y = currentCell.Y;

            var map = mapObject.Map;
            var path = map.PathfindLayer.Pathfind(currentCell, target);

            if (!path.IsSuccess) yield break;

            var cost = path.Cost;
            if (cost <= 0 || cost > basicStats.Speed.Value) yield break;

            moved = true;
            mapObject.SetCell(target);
            foreach(var cell in path.Path)
            {
                mapObject.SyncWithMap(map, cell.X, cell.Y);
                yield return new WaitForSeconds(0.1f);
            }
            moved = false;
        }
    }
}