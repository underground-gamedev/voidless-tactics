using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public partial class SpellComponent : Node, ISpellComponent
{
    private Character character;
    private bool pickupUsed = false;

    [Export]
    private int manaPickupCount = 40;
    private Dictionary<ManaType, List<ModularSpell>> spells;
    private List<ModularSpell> activeSpells => spells.ContainsKey(ManaType) ? spells[ManaType] : new List<ModularSpell>();

    public ManaType ManaType = ManaType.None;
    public int ManaCount = 0;

    public bool CastSpellAvailable()
    {
        return activeSpells.Any(spell => spell.CastAvailable());
    }

    public List<string> GetAvailableSpellNames()
    {
        return activeSpells.Where(spell => spell.CastAvailable())
                     .Select(spell => spell.SpellName)
                     .ToList();
    }

    public ISpell GetSpellByName(string name)
    {
        return activeSpells.FirstOrDefault(spell => spell.SpellName == name);
    }

    public bool PickupAvailable(TacticMap map, MapCell srcMapCell)
    {
        return !pickupUsed;
    }

    public void PickupMana(TacticMap map, MapCell srcMapCell)
    {
        ManaCell srcManaCell = srcMapCell.Mana;
        var manaType = srcManaCell.ManaType;

        var manaPickupCountPlanned = manaPickupCount;

        var manaControl = character.BasicStats.ManaControl.ActualValue;
        if (ManaType == ManaType.None || ManaType == manaType)
        {
            manaPickupCountPlanned = Mathf.Min(manaPickupCount, manaControl - ManaCount);
        }

        var consumeCount = srcManaCell.Consume(manaPickupCountPlanned);

        pickupUsed = true;

        TakeMana(manaType, consumeCount);

        map.ManaLayer.OnSync(map);
    }

    public void Consume(int manaCount)
    {
        ManaCount -= manaCount;
        if (ManaCount <= 0)
        {
            ManaCount = 0;
            ManaType = ManaType.None;
        }
    }

    public void TakeMana(ManaType newType, int count)
    {
        if (newType == ManaType)
        {
            ManaCount += count;
        }
        else
        {
            var mixResults = new Dictionary<(ManaType, ManaType), ManaType>() {
                [(ManaType.Magma, ManaType.Wind)] = ManaType.Fire,
                [(ManaType.Magma, ManaType.Water)] = ManaType.Earth
            };

            ManaType resultType = ManaType.None;
            if (mixResults.ContainsKey((ManaType, newType))) {
                resultType = mixResults[(ManaType, newType)];
            }
            else if (mixResults.ContainsKey((newType, ManaType))) {
                resultType = mixResults[(newType, ManaType)];
            }

            if (resultType == ManaType.None)
            {
                ManaType = newType;
                ManaCount = count;
            }
            else
            {
                ManaType = resultType;
                ManaCount = (ManaCount + count) / 2;
            }
        }

        var manaControl = character.BasicStats.ManaControl.ActualValue;
        if (ManaCount > manaControl)
        {
            ManaExplosion(ManaCount - manaControl);
        }
    }

    private void ManaExplosion(int explosionCount)
    {
        Consume(explosionCount);
        character.Components.FindChild<ITargetComponent>()?.TakeDamage(explosionCount/5);
    }

    public void OnTurnStart(Character parent)
    {
        pickupUsed = false;
    }

    public override void _Ready()
    {
        character = this.FindParent<Character>();
        spells = ConfiguredSpellBindings.DefaultSpellBindings.BindSpells(this);
    }
}