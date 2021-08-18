using System.Threading.Tasks;
using Godot;

public class WaitComponent : Node, IWaitComponent
{
    private bool waitUsed;
    private Character character;

    public Task Wait()
    {
        waitUsed = true;
        GetTree().GroupTrigger(GDTriggers.CharacterWaitTrigger, character);
        return Task.CompletedTask;
    }

    public bool WaitAvailable()
    {
        return !waitUsed;
    }

    public void OnRoundStart(Character character)
    {
        this.character = character;
        waitUsed = false;
    }
}