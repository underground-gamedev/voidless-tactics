using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public class ManaGiveComponent : Node, IManaGiveComponent
{
    private Character character;
    private TacticMap map;
    public List<MapCell> GetGiveManaArea(MapCell src)
    {
        return map.AllNeighboursFor(src.X, src.Y);
    }

    public Task GiveMana(Character targetChar)
    {
        var manaContainer = character.GetManaContainerComponent();
        var expectedTransfer = ExpectedTransfer(targetChar);
        var manaType = manaContainer.ManaType;

        manaContainer.ConsumeMana(manaContainer.ManaCount);
        targetChar.GetManaContainerComponent().AddMana(manaType, expectedTransfer);

        return Task.CompletedTask;
    }

    public bool GiveManaAvailable()
    {
        var manaContainer = character.GetManaContainerComponent();
        if (manaContainer == null) return false;
        return manaContainer.ManaCount > 0;
    }

    public bool GiveManaAvailable(MapCell target)
    {
        if (!GiveManaAvailable()) return false;
        var targetChar = target.MapObject as Character;
        if (targetChar == null) return false;
        return ExpectedTransfer(targetChar) > 0;
    }

    private int ExpectedTransfer(Character targetChar)
    {
        var manaContainer = character.GetManaContainerComponent();
        var targetManaContainer = targetChar.GetManaContainerComponent();
        if (targetManaContainer == null) return 0;

        var manaType = manaContainer.ManaType;
        var manaCount = manaContainer.ManaCount;

        if (character.Controller == targetChar.Controller)
        {
            if (targetManaContainer.IsSaveType(manaType, manaCount) 
            || targetManaContainer.IsTransmute(manaType, manaCount)
            || targetManaContainer.ManaType == ManaType.None) {
                return targetManaContainer.SafeTransfer(manaType, manaCount);
            }
            return 0;
        }

        return manaCount;
    }

    public void OnRoundStart(Character character)
    {
        this.character = character;
        map = character.Map;
    }
}