using Godot;
using System.Collections.Generic;

public class TeamController : Node
{
    [Export]
    protected bool IsMyTurn;
    protected TacticMap tacticMap;

    private List<Character> characters = new List<Character>();

    public void BindMap(TacticMap map)
    {
        tacticMap = map;
    }

    public void Init()
    {
        foreach(var child in GetChildren()) 
        {
            if (child is Character charChild)
            {
                AddCharacter(charChild);
            }
        }
    }

	protected MapCell FindStartPosition(TacticMap map)
	{
		for (int x = 0; x < map.Width; x++)
		{
			for (int y = 0; y < map.Height; y++)
			{
				if (!map.GetSolid(x, y)) {
					return map.CellBy(x, y);
				}
			}
		}

		return null;
	}
	
    public virtual void AddCharacter(Character character)
    {
        characters.Add(character);
        character.controller = this;
		character.BindMap(tacticMap, FindStartPosition(tacticMap));
        character.SetHighlightAvailableMovement(false);
    }

    public void OnTurnStart()
    {
        foreach (var character in characters)
        {
            character.OnTurnStart();
        }
    }
}