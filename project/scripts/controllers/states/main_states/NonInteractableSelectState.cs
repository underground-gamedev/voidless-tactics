using System;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class NonInteractableSelectState: BaseControllerState
{
    private Character active;

    public NonInteractableSelectState(Character active)
    {
        this.active = active;
    }

    public override bool CellClick(int x, int y, Vector2 offset)
    {
        controller.MainStates.PopState();
        return this.CharacterByPos(x, y, (character) => {
            controller.MainStates.PushState(new CharacterSelectTransition(character));
            return true;
        });
    }

    public override void OnEnter()
    {
        var moveComponent = active.Components.GetComponent<IMoveComponent>();
        if (moveComponent is null) return;

        var highlightLayer = controller.Map.MoveHighlightLayer;
        highlightLayer.Clear();
        highlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);
        var moveCells = moveComponent.GetMoveArea();
        foreach (var cell in moveCells)
        {
            var (x, y) = cell.MapCell.Position;
            highlightLayer.Highlight(x, y, MoveHighlightType.NormalMove);
        }

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(active);
    }

    public override void OnLeave()
    {
        controller.Map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideActiveCharacter();
    }
}