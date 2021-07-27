using System;
using System.Linq;
using System.Threading.Tasks;

public class NonInteractableSelectState: BaseControllerState
{
    private Character active;

    public NonInteractableSelectState(Character active)
    {
        this.active = active;
    }

    public override bool CellClick(int x, int y)
    {
        controller.MainStates.PopState();
        return this.CharacterByPos(x, y, (character) => {
            controller.MainStates.PushState(new CharacterSelectTransition(character));
            return true;
        });
    }

    public override void OnEnter()
    {
        var moveComponent = active.Components.FindChild<IMoveComponent>();
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

    }

    public override void OnLeave()
    {
        controller.Map.MoveHighlightLayer.Clear();
    }
}