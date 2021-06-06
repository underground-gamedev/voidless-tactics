using Godot;
using System.Collections.Generic;

public abstract class AbstractController : Node
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

    protected abstract MapCell FindStartPosition(TacticMap map);

    public virtual void AddCharacter(Character character)
    {
        characters.Add(character);
        character.controller = this;
        var pos = FindStartPosition(tacticMap);
		character.BindMap(tacticMap, pos);
        character.SetHighlightAvailableMovement(false);
    }

    public virtual void OnTurnStart()
    {
        IsMyTurn = true;
        foreach (var character in characters)
        {
            character.OnTurnStart();
        }
    }

    public virtual void OnTurnEnd()
    {
        IsMyTurn = false; 
    }
}