using Godot;

public abstract class BaseHoverState: BaseState
{
    public abstract bool OnCellHover(int x, int y, Vector2 offset);
}