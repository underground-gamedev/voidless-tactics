using System.Threading.Tasks;
using Godot;
public class BaseControllerState
{
    protected AbstractController controller;
    protected TacticMap map; 
    private bool initialized;

    public bool Initialized => initialized;

    protected BaseControllerState() {}

    protected Task<BaseControllerState> Async(BaseControllerState result)
    {
        return Task.Run(() => result);
    }

    protected void Init(AbstractController controller, TacticMap map)
    {
        this.controller = controller;
        this.map = map;
        this.initialized = true;
    }

    protected Task<BaseControllerState> NextState(BaseControllerState next)
    {
        this.OnLeave();
        next.Init(controller, map);
        next.OnEnter();
        return Async(next);
    }

    public virtual async Task<BaseControllerState> CellClick(int x, int y)
    { 
        if (!controller.IsMyTurn()) return this;
        if (map.IsOutOfBounds(x ,y)) return this;

        var targetCharacter = map.GetCharacter(x, y);

        if (targetCharacter == null) return await EmptyCellClick(x, y);

        return await CharacterClick(targetCharacter);
    }

    protected virtual Task<BaseControllerState> CharacterClick(Character character)
    {
        return Async(this);
    }

    protected virtual Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return Async(this);
    }

    public virtual Task<BaseControllerState> MenuActionSelected(string action)
    {
        return Async(this);
    }

    public virtual void OnEnter() {}
    public virtual void OnLeave() {}
}