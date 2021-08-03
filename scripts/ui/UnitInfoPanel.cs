using Godot;

public class UnitInfoPanel: Control
{
    private Label healthLabel;
    private Label damageLabel;
    private Label moveLabel;

    public override void _Ready()
    {
        var labels = GetNode<Node>("Labels");
        healthLabel = labels.GetNode<Label>("HealthLabel");
        damageLabel = labels.GetNode<Label>("DamageLabel");
        moveLabel = labels.GetNode<Label>("MoveLabel");

        Hide();
    }

    public void DisplayInfo(Character character)
    {
        if (character == null) { 
            Hide(); 
            return;
        }

        Visible = true;
        var stats = character.BasicStats;
        healthLabel.Text = $"Health: {stats.Health.ActualValue}/{stats.Health.MaxValue}";
        damageLabel.Text = $"Damage: {stats.Damage.ActualValue}";
        moveLabel.Text = $"Speed: {stats.Speed.ActualValue}";
    }

    public void HideInfo()
    {
        Visible = false;
    }
}