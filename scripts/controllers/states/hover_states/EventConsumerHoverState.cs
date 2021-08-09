using Godot;

public class EventConsumerHoverState : BaseHoverState
{
    public override bool OnCellHover(int x, int y, Vector2 offset)
    {
        return true;
    }
}