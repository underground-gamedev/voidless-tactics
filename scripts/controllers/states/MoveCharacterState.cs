using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MoveCharacterState: ActiveCharacterState
{
    private List<(int, int)> availableCells;

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
        if (availableCells.Contains((x, y))) {
            await active?.MoveTo(x, y); 
        }
        return await NextState(new ActiveCharacterState(active));
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UserInterfaceService.GetHUD<TacticHUD>()?.HideMenuWithActions();

        availableCells = map.GetAvailableMoveCells(active).Select(cell => cell.Position).ToList();

        var highlightLayer = map.MoveHighlightLayer;
        foreach (var (x, y) in availableCells)
        {
            highlightLayer.Highlight(x, y, MoveHighlightType.NormalMove);
        }
    }

    public override void OnLeave()
    {
        base.OnLeave();
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
    }
}