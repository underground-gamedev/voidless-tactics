using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class AttackTargetInMoveAreaAIComponent : AIComponent
{
    public override async Task MakeTurn()
    {
        var moveCellsArea = moveComponent.MoveAvailable() ? moveComponent.GetMoveArea() : new List<MoveCell>();
        var attackTargets = GetAttackTargets(moveCellsArea);
        // skip turn if targets not found
        if (attackTargets.Count == 0) return;

        var targetItem = attackTargets.First();
        var targetEnemy = targetItem.Key;
        var targetPosition = targetItem.Value.First();

        if (targetPosition.MapCell != character.Cell)
        {
            await moveComponent.MoveTo(targetPosition.MapCell);
        }

        await attackComponent.Attack(targetEnemy.GetTargetComponent());
    }

    /// <returns>
    /// key is the target for the attack
    /// value is a list of positions from which you can make this attack 
    /// </returns>
    private Dictionary<Character, List<MoveCell>> GetAttackTargets(List<MoveCell> moveArea)
    {
        var result = new Dictionary<Character, List<MoveCell>>();
        if (!attackComponent.AttackAvailable()) return result;

        foreach(var moveCell in moveArea.Append(new MoveCell(character.Cell, false)))
        {
            var mapCell = moveCell.MapCell;
            var attackArea = attackComponent.GetAttackArea(mapCell);
            var intermediate = attackArea .Select(cell => cell.MapObject as Character).ToList();
            var targets = intermediate.Where(ch => ch != null && ch.GetTargetComponent() != null && enemyCharacters.Contains(ch)).ToList();
            
            foreach (var target in targets)
            {
                if (!result.ContainsKey(target))
                {
                    result.Add(target, new List<MoveCell>());
                }

                result[target].Add(moveCell);
            }
        }

        return result;
    }
}