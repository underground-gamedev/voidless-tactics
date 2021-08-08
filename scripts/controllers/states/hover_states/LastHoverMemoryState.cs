using Godot;

public class LastHoverMemoryState : SimpleHoverState
{
    private int lastX;
    private int lastY;
    private int currentX;
    private int currentY;
    public (int, int) LastPosition => (lastX, lastY);

    public LastHoverMemoryState(int x, int y)
    {
        lastX = x;
        lastY = y;
        currentX = x;
        currentY = y;
    }
    public override bool OnCellHover(int x, int y)
    {
        if (currentX != x || currentY != y)
        {
            lastX = currentX;
            lastY = currentY;
            currentX = x;
            currentY = y; 
        }

        return base.OnCellHover(x, y);
    }
}