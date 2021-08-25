using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Battle
{
    public class AttackClosestTargetAIComponent : AIComponent
    {
        protected override IEnumerator ChooseAction()
        {
            Character target = GetClosestTarget();

            if (target == null) yield break;

            var moveCom = character.MoveComponent;
            var attackCom = character.AttackComponent;

            List<MapCell> moveAreaMapCells = moveCom.MoveAvailable() ? moveCom.GetMoveArea() : new List<MapCell>();
            var targetCell = target.MapObject.Cell;
            List<MapCell> attackCircleMapCells = map.AllNeighboursFor(targetCell.X, targetCell.Y);



            moveAreaMapCells.Add(mapObj.Cell);

            List<MapCell> cellsAttackCanOccurFrom = attackCircleMapCells.Where(cell => moveAreaMapCells.Contains(cell)).ToList();

            if (cellsAttackCanOccurFrom.Count > 0)
            {
                if (!cellsAttackCanOccurFrom.Contains(mapObj.Cell))
                {
                    yield return moveCom.MoveTo(GetClosestCellFromList(cellsAttackCanOccurFrom));
                }

                if (attackCom.AttackAvailable())
                {
                    yield return attackCom.Attack(target.TargetComponent);
                }
            }
            else if (moveCom.MoveAvailable())
            {
                MapCell farthestCell = null;

                var path = map.PathfindLayer.Pathfind(mapObj.Cell, target.MapObject.Cell);

                if (!path.IsSuccess) yield break;

                foreach (var cell in path.Path.Skip(1))
                {
                    if (!moveAreaMapCells.Contains(cell))
                    {
                        break;
                    }
                    farthestCell = cell;
                }

                if (farthestCell != null)
                {
                    yield return moveCom.MoveTo(farthestCell);
                }
            }
        }

        private int CellDistance(MapCell a, MapCell b)
        {
            return Mathf.RoundToInt((new Vector2(b.X, b.Y) - new Vector2(a.X, a.Y)).magnitude);
        }

        Character GetClosestTarget()
        {
            Character result = null;
            int minDistance = int.MaxValue;

            foreach (Character enemy in enemyCharacters)
            {
                int distance = CellDistance(enemy.MapObject.Cell, mapObj.Cell);
                if (distance < minDistance)
                {
                    result = enemy;
                    minDistance = distance;
                }
            }

            return result;
        }

        MapCell GetClosestCellFromList(List<MapCell> cells)
        {
            MapCell result = null;

            int minDistance = int.MaxValue;
            foreach (MapCell cell in cells)
            {
                int distance = CellDistance(cell, mapObj.Cell);
                if (distance < minDistance)
                {
                    result = cell;
                    minDistance = distance;
                }
            }

            if (result == null) Debug.LogError("Closest position don't found. Something went wrong!");

            return result;
        }
    }
}