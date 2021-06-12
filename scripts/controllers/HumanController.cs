using System.Linq;
using Godot;

public class HumanController: AbstractController
{
    private enum ActiveCharacterStates
    {
        None, Move, Attack,
    }

    private Character activeCharacter;
    private ActiveCharacterStates activeState;
    private Character hoverCharacter;

    [Signal]
    public delegate void OnActiveCharacterChanged(Character activeCharacter);
    [Signal]
    public delegate void OnHoverCharacterChanged(Character hoverCharacter);

    protected override MapCell FindStartPosition(TacticMap map)
    {
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (!map.GetSolid(x, y) && map.GetCharacter(x, y) == null) {
                    return map.CellBy(x, y);
                }
            }
        }

        return null;
    }

    public void OnCellClick(int x, int y)
    {
        if (!IsMyTurn) return;
        if (tacticMap.IsOutOfBounds(x, y)) return;

        var targetCharacter = tacticMap.GetCharacter(x, y);
        if (targetCharacter == null)
        {
            switch (activeState)
            {
                case ActiveCharacterStates.Move: 
                activeCharacter?.MoveTo(x, y); 
                SwitchToNone();
                break;
            }
        }
        else if (characters.Contains(targetCharacter))
        {
            SetActiveCharacter(targetCharacter);
        }
    }

    public void SetActiveCharacter(Character character)
    {
        activeCharacter = character;
        activeState = ActiveCharacterStates.None;
        if (!SwitchToMove()) SwitchToNone();
        EmitSignal(nameof(OnActiveCharacterChanged), activeCharacter);
    }
    
    public bool SwitchToNone()
    {
        var moveHighlight = tacticMap.MoveHighlightLayer;
        activeState = ActiveCharacterStates.None;
        moveHighlight.Clear();

        return true;
    }

    public bool SwitchToMove()
    {
        if (activeCharacter == null) { return false; }
        if (!IsMyTurn) { return false; }
        if (activeCharacter.MoveActions == 0) { return false; }

        var moveHighlight = tacticMap.MoveHighlightLayer;
        moveHighlight.Clear();
        activeState = ActiveCharacterStates.Move;

        var availablePositions = tacticMap.PathfindLayer.GetAllAvailablePathDest(activeCharacter.Cell, activeCharacter.MovePoints);
        var availableCells = availablePositions.Select(pos => tacticMap.CellBy(pos.Item1, pos.Item2)).Where(cell => cell.Character == null).ToList();
        foreach (var (availX, availY) in availableCells.Select(cell => cell.Position))
        {
            moveHighlight.Highlight(availX, availY, MoveHighlightType.NormalMove);
        }

        return true;
    }
}