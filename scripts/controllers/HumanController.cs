using Godot;

public class HumanController: AbstractController
{
    private Character activeCharacter;
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
            activeCharacter?.MoveTo(x, y);
        }
        else if (characters.Contains(targetCharacter))
        {
            SetActiveCharacter(targetCharacter);
        }
        else
        {
        }
    }

    public void SetActiveCharacter(Character character)
    {
        if (activeCharacter != null)
        {
            activeCharacter.SetHighlightAvailableMovement(false);
        }
        activeCharacter = character;
        activeCharacter.SetHighlightAvailableMovement(true);
        EmitSignal(nameof(OnActiveCharacterChanged), activeCharacter);

    }

    public override void AddCharacter(Character character)
    {
        base.AddCharacter(character);

        if (activeCharacter is null)
        {
            SetActiveCharacter(character);
        }
    }
}