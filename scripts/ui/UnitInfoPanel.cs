using Godot;

public class UnitInfoPanel: Control
{
    private Label healthLabel;
    private Label damageLabel;
    private Label moveLabel;
    private Label initiativeLabel;

    public override void _Ready()
    {
        var labels = GetNode<Node>("Labels");
        healthLabel = labels.GetNode<Label>("HealthLabel");
        damageLabel = labels.GetNode<Label>("DamageLabel");
        moveLabel = labels.GetNode<Label>("MoveLabel");
        initiativeLabel = labels.GetNode<Label>("InitiativeLabel");

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
        initiativeLabel.Text = $"Initiative: {stats.Initiative.MinValue}-{stats.Initiative.MaxValue}";
    }

    public void HideInfo()
    {
        Visible = false;
    }
}