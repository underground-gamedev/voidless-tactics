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

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        if (active == character) { return NextState(new ActiveCharacterState(active)); }

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam) { return NextState(new ActiveCharacterState(character)); }
        return NextState(new EnemySelectedState(character));
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
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);

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
    protected override void CharacterHover(Character character)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(character);
        hud?.DisplayCellInfo(character.Cell);
    }

    protected override void EmptyCellHover(MapCell cell)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);
        hud?.DisplayCellInfo(cell);
    }

    public override void OnLeave()
    {
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideCellInfo();
    }
}