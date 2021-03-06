using Godot;
using System;

public class DraggingCamera : Camera2D
{
	private Vector2 mouseStartPos;
	private Vector2 screenStartPosition;
	private Vector2 mousePrevPos;
	private bool dragging;

	[Export]
	private float zoomAdjustFloat = 0.05f;
	Vector2 zoomAdjust;

	[Export]
	private float minZoomFloat = 0.2f;
	Vector2 minZoom;

	[Signal]
	public delegate void OnCameraMove(Vector2 delta);

	[Signal]
	public delegate void OnCameraZoom(Vector2 prevZoom, Vector2 delta);

	public override void _Ready()
	{
		zoomAdjust = new Vector2(zoomAdjustFloat, zoomAdjustFloat);
		minZoom = new Vector2(minZoomFloat, minZoomFloat);

		base._Ready();
	}
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
				mousePrevPos = mouseEvent.Position;
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
			Vector2 delta = (mousePrevPos - mouseEvent.Position);
			EmitSignal(nameof(OnCameraMove), delta);

			mousePrevPos = mouseEvent.Position;
			
		}

		if (@event.IsActionPressed("zoom"))
		{
			var evt = (InputEventMouseButton)@event;
			if (evt != null)
			{
				Vector2 previusZoom = Zoom;

				if (evt.ButtonIndex == (int)ButtonList.WheelUp)
				{
					Zoom -= zoomAdjust;

					if (Zoom < minZoom)
						Zoom = minZoom;


				}
				else
				{
					Zoom += zoomAdjust;
				}

				EmitSignal(nameof(OnCameraZoom),previusZoom, Zoom - previusZoom);
			}
			
		}
	}
}