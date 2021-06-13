using Godot;
public class BaseControllerState
{
    protected AbstractController controller;
    protected TacticMap map; 
    private bool initialized;

    public bool Initialized => initialized;

    protected BaseControllerState() {}

    protected void Init(AbstractController controller, TacticMap map)
    {
        this.controller = controller;
        this.map = map;
        this.initialized = true;
    }

    protected BaseControllerState NextState(BaseControllerState next)
    {
        this.OnLeave();
        next.Init(controller, map);
        next.OnEnter();
        return next;
    }

    public virtual BaseControllerState CellClick(int x, int y)
    { 
        if (!controller.IsMyTurn()) return this;
        if (map.IsOutOfBounds(x ,y)) return this;

        var targetCharacter = map.GetCharacter(x, y);
        if (targetCharacter == null) return EmptyCellClick(x, y);

        return CharacterClick(targetCharacter);
    }

    protected virtual BaseControllerState CharacterClick(Character character)
    {
        return this;
    }

    protected virtual BaseControllerState EmptyCellClick(int x, int y)
    {
        return this;
    }

    public virtual BaseControllerState MenuActionSelected(string action)
    {
        GD.Print($"handle menu action selected {action}");
        return this;
    }

    public virtual void OnEnter() {}
    public virtual void OnLeave() {}
}