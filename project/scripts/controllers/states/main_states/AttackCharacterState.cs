using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class AttackCharacterState: BaseControllerState
{
    private Character active;
    private IAttackComponent attackComponent;

    public AttackCharacterState(Character character)
    {
        active = character;
        attackComponent = active.Components.FindChild<IAttackComponent>();
    }

    public override bool CellClick(int x, int y, Vector2 offset)
    {
        controller.MainStates.PopState();

        return this.CellByPos(x, y, (cell) => {
            var character = cell.MapObject as Character;
            if (character == null) return true;
            var fromMyTeam = controller.Characters.Contains(character);
            if (fromMyTeam) return false;
            if (!attackComponent.AttackAvailable()) return true;

            var attackArea = attackComponent.GetAttackArea(active.Cell);
            var targetComponent = character.Components.FindChild<ITargetComponent>();
            if (targetComponent == null || !attackArea.Contains(character.Cell)) return false;

            controller.MainStates.PushState(new EventConsumerMainState());
            attackComponent
                .Attack(targetComponent)
                .GetAwaiter()
                .OnCompleted(() => controller.TriggerEndTurn());
            return true;
        });
    }

    public override void OnEnter()
    {
        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        highlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);
        foreach (var cell in attackComponent.GetAttackArea(active.Cell))
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.Attack);
        }

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(active);
    }

    public override void OnLeave()
    {
        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideActiveCharacter();
    }
}