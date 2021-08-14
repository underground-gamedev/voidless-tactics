using Godot;

public class EventConsumerMainState: BaseControllerState
{
    public override bool CellClick(int x, int y, Vector2 offset)
    {
        return true;
    }

    public override bool MenuActionSelected(string action)
    {
        return true;
    }
}