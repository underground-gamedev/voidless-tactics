using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MoveCharacterState: BaseControllerState
{
    protected Character active;
    private List<MoveCell> availableCells;

    public MoveCharacterState(Character character)
    {
        this.active = character;
    }

    public override bool CellClick(int x, int y)
    {
        var charClick = this.CharacterByPos(x, y, (character) => {
            controller.MainStates.PopState();
            return true;
        });

        if (charClick) return false;

        controller.MainStates.PopState();
        return this.CellByPos(x, y, (cell) => {
            if (availableCells.All(c => c.MapCell != cell)) return false;

            controller.MainStates.PushState(new EventConsumerMainState());
            active.Components
                .FindChild<IMoveComponent>()
                .MoveTo(cell)
                .GetAwaiter()
                .OnCompleted(() => controller.MainStates.PopState());

            return true;
        });
    }

    public override void OnEnter()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);

        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        highlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var moveComponent = active.Components.FindChild<IMoveComponent>();
        if (moveComponent?.MoveAvailable() != true) {
            availableCells = new List<MoveCell>();
            return;
        }

        availableCells = moveComponent.GetMoveArea();
        foreach (var moveCell in availableCells)
        {
            var (x, y) = moveCell.MapCell.Position;
            var highlightType = moveCell.ActionNeed == 1 ? MoveHighlightType.NormalMove : MoveHighlightType.LongMove;
            highlightLayer.Highlight(x, y, highlightType);
        }
    }

    public override void OnLeave()
    {
        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
    }
}