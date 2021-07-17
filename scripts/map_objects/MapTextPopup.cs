using Godot;
using System;
using System.Threading.Tasks;

public class MapTextPopup : Node2D
{
    [Export]
    private Vector2 moveDirection;
    [Export]
    private float duration;

    private Label label;
    private Tween tween;
    private Vector2 startPosition;
    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        tween = GetNode<Tween>("Tween");

        this.label.Visible = false;
        this.startPosition = label.RectPosition;
    }

    public async Task Animate(string message, Color color)
    {
        GD.Print("Animate Start");
        this.label.Text = message;
        this.label.Modulate = new Color(color.r, color.g, color.b, 0);
        this.label.RectPosition = startPosition;
        this.label.Visible = true;
        tween.InterpolateProperty(
            this.label, "modulate:a", 
            0, 1, duration/5,
            Tween.TransitionType.Linear, Tween.EaseType.InOut, 0);
        tween.InterpolateProperty(
            this.label, "rect_position", 
            this.label.RectPosition, this.label.RectPosition + moveDirection, duration*4/5,
            Tween.TransitionType.Linear, Tween.EaseType.InOut, 0);
        tween.InterpolateProperty(
            this.label, "modulate:a", 
            1, 0, duration/5,
            Tween.TransitionType.Linear, Tween.EaseType.InOut, duration*4/5);
        
        tween.Start();
        await this.Wait(duration);
        GD.Print("Animate Complete");
    }
}
