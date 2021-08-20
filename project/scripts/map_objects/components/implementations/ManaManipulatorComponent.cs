using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public class ManaManipulatorComponent : Node, IManaContainerComponent, IManaPickupComponent
{
    [Export]
    private int manaPickupCount = 40;

    static Dictionary<(ManaType, ManaType), ManaType> mixResults = new Dictionary<(ManaType, ManaType), ManaType>() {
        [(ManaType.Magma, ManaType.Wind)] = ManaType.Fire,
        [(ManaType.Magma, ManaType.Water)] = ManaType.Earth,
        [(ManaType.Water, ManaType.Wind)] = ManaType.Ice,
    };

    private ManaType GetMixResult(ManaType first, ManaType second)
    {
        if (mixResults.ContainsKey((first, second))) return mixResults[(first, second)];
        if (mixResults.ContainsKey((second, first))) return mixResults[(second, first)];
        return ManaType.None;
    }

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

    public void AddMana(ManaType newType, int count)
    {
        var expectedManaType = ExpectedType(newType, count);
        var expectedManaValue = ExpectedValue(newType, count);
        this.manaType = expectedManaType;
        this.manaCount = expectedManaValue;
        var manaControl = character.BasicStats.ManaControl.ActualValue;
        if (expectedManaValue > manaControl)
        {
            ManaExplosion(expectedManaValue - manaControl);
        }
    }

    public bool IsTransmute(ManaType type, int count)
    {
        return count > 0 && GetMixResult(manaType, type) != ManaType.None;
    }

    public bool IsSaveType(ManaType type, int count)
    {
        return count == 0 || manaType == type;
    }

    public bool IsReplaceType(ManaType type, int count)
    {
        return count > 0 && !IsTransmute(type, count) && !IsSaveType(type, count);
    }

    public int SafeTransfer(ManaType type, int count)
    {
        var manaControl = character.BasicStats.ManaControl.ModifiedActualValue;
        var expectedValue = ExpectedValue(type, count);
        if (expectedValue > manaControl) return count - (expectedValue - manaControl);
        return count;
    }

    private int ExpectedValue(ManaType type, int count)
    {
        if (IsSaveType(type, count)) return manaCount + count;
        if (IsTransmute(type, count)) return (manaCount + count)/2;
        if (IsReplaceType(type, count)) return count;
        return 0;
    }

    private ManaType ExpectedType(ManaType type, int count)
    {
        if (IsSaveType(type, count)) return manaType;
        if (IsTransmute(type, count)) return GetMixResult(manaType, type);
        if (IsReplaceType(type, count)) return type;
        return ManaType.None;
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

        AddMana(manaType, consumeCount);

        var map = character.Map;
        map.ManaLayer.OnSync(map);

        return Task.CompletedTask;
    }
}