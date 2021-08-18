using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public class ManaManipulatorComponent : Node, IManaContainerComponent, IManaPickupComponent
{
    [Export]
    private int manaPickupCount = 40;

    private Character character;
    private bool pickupUsed = false;

    public ManaType manaType = ManaType.None;
    public ManaType ManaType => manaType;
    public int manaCount = 0;
    public int ManaCount => manaCount;

    public void ConsumeMana(int count)
    {
        this.manaCount -= manaCount;
        if (this.manaCount <= 0)
        {
            this.manaCount = 0;
            manaType = ManaType.None;
        }
    }

    public void TakeMana(ManaType newType, int count)
    {
        if (newType == manaType)
        {
            manaCount += count;
        }
        else
        {
            var mixResults = new Dictionary<(ManaType, ManaType), ManaType>() {
                [(ManaType.Magma, ManaType.Wind)] = ManaType.Fire,
                [(ManaType.Magma, ManaType.Water)] = ManaType.Earth
            };

            ManaType resultType = ManaType.None;
            if (mixResults.ContainsKey((manaType, newType))) {
                resultType = mixResults[(manaType, newType)];
            }
            else if (mixResults.ContainsKey((newType, manaType))) {
                resultType = mixResults[(newType, manaType)];
            }

            if (resultType == ManaType.None)
            {
                manaType = newType;
                manaCount = count;
            }
            else
            {
                manaType = resultType;
                manaCount = (manaCount + count) / 2;
            }
        }

        var manaControl = character.BasicStats.ManaControl.ActualValue;
        if (manaCount > manaControl)
        {
            ManaExplosion(manaCount - manaControl);
        }
    }

    private void ManaExplosion(int explosionCount)
    {
        ConsumeMana(explosionCount);
        character.GetTargetComponent()?.TakeDamage(explosionCount/5);
    }

    public void OnRoundStart(Character character)
    {
        this.character = character;
        pickupUsed = false;
    }

    public bool ManaPickupAvailable(MapCell src)
    {
        return ManaPickupAvailable() && src.Mana.ManaType != ManaType.None;
    }

    public bool ManaPickupAvailable()
    {
        return !pickupUsed;
    }

    public Task ManaPickup(MapCell src)
    {
        ManaCell srcManaCell = src.Mana;
        var manaType = srcManaCell.ManaType;

        var manaPickupCountPlanned = manaPickupCount;

        var manaControl = character.BasicStats.ManaControl.ActualValue;
        if (this.manaType == ManaType.None || this.manaType == manaType)
        {
            manaPickupCountPlanned = Mathf.Min(manaPickupCount, manaControl - manaCount);
        }

        var consumeCount = srcManaCell.Consume(manaPickupCountPlanned);

        pickupUsed = true;

        TakeMana(manaType, consumeCount);

        var map = character.Map;
        map.ManaLayer.OnSync(map);

        return Task.CompletedTask;
    }
}