using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public abstract class AIComponent: Node
{
    protected Character character;
    protected BasicStats stats;
    protected TacticMap map;
    protected List<Character> enemyCharacters;
    protected List<Character> allyCharacters;
    protected IMoveComponent moveComponent;
    protected IAttackComponent attackComponent;
    protected ISpellComponent spellComponent;

    public abstract Task MakeTurn();

    public void OnTurnStart(Character character)
    {
        if (this.character == null)
        {
            Init(character);
        }
    }

    private void Init(Character character)
    {
        this.character = character;
        this.stats = character.BasicStats;
        this.map = character.Map;
        var allCharacters = map.GetAllCharacters;
        this.allyCharacters = character.Controller.Characters.ToList();
        this.enemyCharacters = allCharacters.Where(ch => !allyCharacters.Contains(ch)).ToList();
        this.moveComponent = character.GetMoveComponent();
        this.attackComponent = character.GetAttackComponent();
        this.spellComponent = character.GetSpellComponent();
        AddToGroup(GDTriggers.CharacterDeathTrigger);
    }

    public void CharacterDeathTrigger(Character character)
    {
        allyCharacters.Remove(character);
        enemyCharacters.Remove(character);
    }
}