using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MoveCharacterState: ActiveCharacterState
{
    private List<MoveCell> availableCells;

    public MoveCharacterState(Character character):base(character)
    {
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        if (active == character) { return NextState(new ActiveCharacterState(active)); }
        return base.CharacterClick(character);
    }

    protected override async Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        if (availableCells.Any(cell => cell.MapCell.Position == (x, y))) {
            await active.Components.FindChild<IMoveComponent>().MoveTo(active.Map.CellBy(x, y));
        }
        return await NextState(new ActiveCharacterState(active));
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UserInterfaceService.GetHUD<TacticHUD>()?.HideMenuWithActions();

        var moveComponent = active.Components.FindChild<IMoveComponent>();
        if (moveComponent?.MoveAvailable() != true) {
            availableCells = new List<MoveCell>();
            return;
        }

        var highlightLayer = map.MoveHighlightLayer;
        availableCells = moveComponent.GetMoveArea();
        foreach (var moveCell in availableCells)
        {
            var (x, y) = moveCell.MapCell.Position;
            var highlightType = moveCell.ActionNeed == 1 ? MoveHighlightType.NormalMove : MoveHighlightType.LongMove;
            highlightLayer.Highlight(x, y, highlightType);
        }
    }
    protected override Task<BaseControllerState> CharacterHover(Character character)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(character);
        hud?.DisplayCellInfo(character.Cell);
        return Async(this);
    }

    protected override Task<BaseControllerState> EmptyCellHover(MapCell cell)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);
        hud?.DisplayCellInfo(cell);
        return Async(this);
    }

    public override void OnLeave()
    {
        base.OnLeave();
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideCellInfo();
    }
}