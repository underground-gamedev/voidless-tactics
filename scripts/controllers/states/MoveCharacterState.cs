using System;
using System.Collections.Generic;
using System.Linq;

public class MoveCharacterState: ActiveCharacterState
{
    private List<(int, int)> availableCells;

    public MoveCharacterState(Character character):base(character)
    {
    }

    protected override BaseControllerState EmptyCellClick(int x, int y)
    {
        if (availableCells.Contains((x, y))) {
            active?.MoveTo(x, y); 
        }
        return NextState(new ActiveCharacterState(active));
    }

    public override void OnEnter()
    {
        base.OnEnter();
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