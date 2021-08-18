using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class MoveCharacterState: BaseControllerState
{
    protected Character active;
    private List<MoveCell> availableCells;

    public MoveCharacterState(Character character)
    {
        this.active = character;
    }

    public override bool CellClick(int x, int y, Vector2 offset)
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
                .OnCompleted(() => controller.TriggerEndTurn());

            return true;
        });
    }

    public override void OnEnter()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(active);

        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        highlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var moveComponent = active.Components.GetComponent<IMoveComponent>();
        if (moveComponent?.MoveAvailable() != true) {
            availableCells = new List<MoveCell>();
            return;
        }

        availableCells = moveComponent.GetMoveArea();
        foreach (var moveCell in availableCells)
        {
            var (x, y) = moveCell.MapCell.Position;
            var highlightType = moveCell.WeakAttack ? MoveHighlightType.LongMove : MoveHighlightType.NormalMove;
            highlightLayer.Highlight(x, y, highlightType);
        }
    }

    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideActiveCharacter();

        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
    }
}