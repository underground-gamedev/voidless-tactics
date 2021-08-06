using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class AbstractController : Node
{
    protected TacticMap tacticMap;

    protected List<Character> characters = new List<Character>();

    public IReadOnlyList<Character> Characters => characters.AsReadOnly();

    public async Task Init(TacticMap map, List<MapCell> startArea)
    {
        tacticMap = map;
        this.GetChilds<Character>(".").ForEach(child => AddCharacter(child));
        await SpawnUnits(map, startArea);
    }

    public virtual void AddCharacter(Character character)
    {
        characters.Add(character);
        character.Controller = this;
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

    public abstract Task SpawnUnits(TacticMap map, List<MapCell> startArea);
    public abstract Task MakeTurn(Character active);
}