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

    public Task<BaseControllerState> CellClick(int x, int y)
    { 
        if (map.IsOutOfBounds(x, y)) return Async(this);
        return GenericCellClick(map.CellBy(x, y));
    }

    protected virtual Task<BaseControllerState> GenericCellClick(MapCell cell)
    {
        if (!controller.IsMyTurn()) return Async(this);

        var targetCharacter = map.GetCharacter(cell.X, cell.Y);
        if (targetCharacter == null) return EmptyCellClick(cell.X, cell.Y);
        return CharacterClick(targetCharacter);
    }

    protected virtual Task<BaseControllerState> CharacterClick(Character character)
    {
        return Async(this);
    }

    protected virtual Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return Async(this);
    }

    public void CellHover(int x, int y)
    {
        if (map.IsOutOfBounds(x, y)) return;
        GenericCellHover(map.CellBy(x, y));
    }

    protected virtual void GenericCellHover(MapCell cell)
    {
        if (!controller.IsMyTurn()) return;
        var targetCharacter = cell.MapObject as Character;
        if (targetCharacter != null)
        {
            CharacterHover(targetCharacter);
        }
        else
        {
            EmptyCellHover(cell);
        }
    }

    protected virtual void EmptyCellHover(MapCell cell)
    {
    }

    protected virtual void CharacterHover(Character character)
    {
    }

    public virtual Task<BaseControllerState> MenuActionSelected(string action)
    {
        return Async(this);
    }

    public virtual void OnEnter() {}
    public virtual void OnLeave() {}
}