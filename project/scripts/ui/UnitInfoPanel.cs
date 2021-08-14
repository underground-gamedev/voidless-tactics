using Godot;

public class UnitInfoPanel: Control
{
    private Label healthLabel;
    private Label damageLabel;
    private Label moveLabel;
    private Label initiativeLabel;
    private Label manaControlLabel;
    private Label spellPowerLabel;
    private Label manaLabel;

    public override void _Ready()
    {
        var labels = GetNode<Node>("Labels");
        healthLabel = labels.GetNode<Label>("HealthLabel");
        damageLabel = labels.GetNode<Label>("DamageLabel");
        moveLabel = labels.GetNode<Label>("MoveLabel");
        initiativeLabel = labels.GetNode<Label>("InitiativeLabel");
        manaLabel = labels.GetNode<Label>("ManaLabel");
        manaControlLabel = labels.GetNode<Label>("ManaControlLabel");
        spellPowerLabel = labels.GetNode<Label>("SpellPowerLabel");

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
        healthLabel.Text = $"{stats.Health.Name}: {stats.Health.ActualValue}/{stats.Health.MaxValue}";
        damageLabel.Text = $"{stats.Damage.Name}: {stats.Damage.ActualValue}";
        moveLabel.Text = $"{stats.Speed.Name}: {stats.Speed.ActualValue}";
        initiativeLabel.Text = $"{stats.Initiative.Name}: {stats.Initiative.MinValue}-{stats.Initiative.MaxValue}";
        manaControlLabel.Text = $"{stats.ManaControl.Name}: {stats.ManaControl.ActualValue}";
        spellPowerLabel.Text = $"{stats.SpellPower.Name}: {stats.SpellPower.ActualValue}";

        var spellComponent = character.Components.GetComponent<SpellComponent>();
        if (spellComponent != null)
        {
            manaLabel.Text = $"Mana: {spellComponent.ManaCount} {spellComponent.ManaType.ToString().ToLower()}";
        }
        else
        {
            manaLabel.Text = $"Mana: unavailable";
        }
    }

    public void HideInfo()
    {
        Visible = false;
    }
}