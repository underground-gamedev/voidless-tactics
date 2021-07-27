public class EventConsumerMainState: BaseControllerState
{
    public override bool CellClick(int x, int y)
    {
        return true;
    }

    public override bool MenuActionSelected(string action)
    {
        return true;
    }
}