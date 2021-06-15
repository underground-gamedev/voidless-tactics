using Godot;
using System;

public class DraggingCamera : Camera2D
{
    private Vector2 mouseStartPos;
    private Vector2 screenStartPosition;
    private bool dragging;
    public override void _Input(InputEvent @event)
    {
        var mouseEvent = @event as InputEventMouse;
        if (mouseEvent is null) return;

        if (@event.IsAction("drag"))
        {
            if (@event.IsPressed())
            {
                mouseStartPos = @mouseEvent.Position;
                screenStartPosition = Position;
                dragging = true;
            }
            else
            {
                dragging = false;
            }
        }
        else if (@event is InputEventMouseMotion && dragging)
        {
            Position = Zoom * (mouseStartPos - mouseEvent.Position) + screenStartPosition;
        }
    }
}