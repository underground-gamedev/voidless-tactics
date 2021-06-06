using Godot;

public class HumanController: TeamController
{
    private Character activeCharacter;
    private Character hoverCharacter;

    [Signal]
    public delegate void OnActiveCharacterChanged(Character activeCharacter);
    [Signal]
    public delegate void OnHoverCharacterChanged(Character hoverCharacter);

    public void OnCellClick(int x, int y)
    {
        if (!IsMyTurn) return;
        if (tacticMap.IsOutOfBounds(x, y)) return;

		activeCharacter?.MoveTo(x, y);
    }

    public override void AddCharacter(Character character)
    {
        base.AddCharacter(character);

        if (activeCharacter is null)
        {
            activeCharacter = character;
            activeCharacter.SetHighlightAvailableMovement(true);
            EmitSignal(nameof(OnActiveCharacterChanged), activeCharacter);
        }
    }
}