using System.Threading.Tasks;

namespace Battle
{
    public interface IManaContainerComponent
    {
        ManaInfo Mana { get; }
        void AddMana(ManaInfo mana);
        ManaInfo ConsumeMana(int count);

        bool IsTransmute(ManaInfo mana);
        bool IsSaveType(ManaInfo mana);
        bool IsReplaceType(ManaInfo mana);
        int SafeTransfer(ManaInfo mana);
    }
}