using Godot;
using System.Collections.Generic;

public class TeamController : Node
{
    [Export]
    protected bool IsMyTurn;
    protected TacticMap tacticMap;

    protected List<Character> characters = new List<Character>();

    public void BindMap(TacticMap map)
    {
        tacticMap = map;
    }

    public void Init()
    {
        foreach(var child in this.GetChilds<Character>(".")) 
        {
            AddCharacter(child);
        }
    }

	protected MapCell FindStartPosition(TacticMap map)
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
	
    public virtual void AddCharacter(Character character)
    {
        characters.Add(character);
        character.controller = this;
        var pos = FindStartPosition(tacticMap);
		character.BindMap(tacticMap, pos);
        character.SetHighlightAvailableMovement(false);
    }

    public void OnTurnStart()
    {
        IsMyTurn = true;
        foreach (var character in characters)
        {
            character.OnTurnStart();
        }
    }

    public void OnTurnEnd()
    {
        IsMyTurn = false; 
    }
}