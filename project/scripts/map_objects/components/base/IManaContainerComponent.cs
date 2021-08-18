using System.Threading.Tasks;

public interface IManaContainerComponent
{
    ManaType ManaType { get; }
    int ManaCount { get; }
    void ConsumeMana(int count);
    void TakeMana(ManaType type, int count);
}