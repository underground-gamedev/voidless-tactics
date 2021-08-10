using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class SpellComponent : Node, ISpellComponent
{
    private Character character;
    private bool pickupUsed = false;

    [Export]
    private int manaPickupCount = 40;

    private List<ISpell> Spells => this.GetChilds<ISpell>(".");

    public ManaType ManaType = ManaType.None;
    public int ManaCount = 0;

    public bool CastSpellAvailable()
    {
        return Spells.Any(spell => spell.CastAvailable());
    }

    public List<string> GetAvailableSpellNames()
    {
        return Spells.Where(spell => spell.CastAvailable())
                     .Select(spell => (spell as Node).Name)
                     .ToList();
    }

    public ISpell GetSpellByName(string name)
    {
        return Spells.FirstOrDefault(spell => (spell as Node).Name == name);
    }

    public bool PickupAvailable(TacticMap map, MapCell srcMapCell)
    {
        return !pickupUsed;
    }

    public void PickupMana(TacticMap map, MapCell srcMapCell)
    {
        ManaCell srcManaCell = srcMapCell.Mana;
        var manaType = srcManaCell.ManaType;
        var consumeCount = srcManaCell.Consume(manaPickupCount);

        map.ManaLayer.OnSync(map);

        pickupUsed = true;

        TakeMana(manaType, consumeCount);
    }

    public void TakeMana(ManaType type, int count)
    {
        if (type == ManaType)
        {
            ManaCount += count;
            return;
        }

        ManaType = type;
        ManaCount = count;
    }

    public void OnTurnStart(Character parent)
    {
        pickupUsed = false;
    }

    public override void _Ready()
    {
        character = this.FindParent<Character>();
    }
}