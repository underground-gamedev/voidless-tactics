using Godot;
using System;
using System.Collections.Generic;

public abstract class AbstractController : Node
{
    [Export]
    protected bool isMyTurn;
    protected TacticMap tacticMap;

    protected List<Character> characters = new List<Character>();

    public IReadOnlyList<Character> Characters => characters.AsReadOnly();

    public void BindMap(TacticMap map)
    {
        tacticMap = map;
    }

    public virtual void Init()
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
        character.Controller = this;
        var pos = FindStartPosition(tacticMap);
        character.BindMap(tacticMap, pos);
    }

    public virtual void RemoveCharacter(Character character)
    {
        if (!characters.Contains(character))
        {
            throw new ArgumentException("Expected character from current team");
        }

        characters.Remove(character);
        if (character.Controller == this) {
            character.Controller = null;
        }
    }

    public virtual void OnTurnStart()
    {
        isMyTurn = true;
        foreach (var character in characters)
        {
            character.OnTurnStart();
        }
    }

    public virtual void OnTurnEnd()
    {
        isMyTurn = false; 
    }

    public bool IsMyTurn() 
    {
        return isMyTurn;
    }
}