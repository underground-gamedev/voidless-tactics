using System.Threading.Tasks;

namespace Battle
{
    public interface IManaContainerComponent
    {
        ManaType ManaType { get; }
        int ManaCount { get; }
        void ConsumeMana(int count);
        void AddMana(ManaType type, int count);

        bool IsTransmute(ManaType type, int count);
        bool IsSaveType(ManaType type, int count);
        bool IsReplaceType(ManaType type, int count);
        int SafeTransfer(ManaType type, int count);
    }
}