using Godot;
public class SelfIdentificateButton: Button
{
    [Signal]
    public delegate void PressedExtended(Button sender);

    public override void _Ready()
    {
        base._Ready();
        this.Connect("pressed", this, nameof(OnPress));
    }

    private void OnPress()
    {
        EmitSignal(nameof(PressedExtended), this);
    }
}