using Godot;

public class UnitInfoPanel: Control
{
    private Label healthLabel;
    private Label damageLabel;
    private Label moveLabel;
    private Label initiativeLabel;
    private Label spellPowerLabel;
    private Label manaLabel;

    public override void _Ready()
    {
        var labels = this;
        healthLabel = labels.GetNode<Label>("HealthLabel");
        damageLabel = labels.GetNode<Label>("DamageLabel");
        moveLabel = labels.GetNode<Label>("MoveLabel");
        initiativeLabel = labels.GetNode<Label>("InitiativeLabel");
        manaLabel = labels.GetNode<Label>("ManaLabel");
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
        healthLabel.Text = $"hp: {stats.Health.ActualValue}/{stats.Health.MaxValue}";
        damageLabel.Text = $"dmg: {stats.Damage.ActualValue}";
        moveLabel.Text = $"spd: {stats.Speed.ActualValue}";
        initiativeLabel.Text = $"init: {stats.Initiative.MinValue}-{stats.Initiative.MaxValue}";
        spellPowerLabel.Text = $"pwr: {stats.SpellPower.ActualValue}";

        var spellComponent = character.Components.GetComponent<SpellComponent>();
        if (spellComponent != null)
        {
            manaLabel.Text = $"mp: {spellComponent.ManaType.ToString().ToLower()} {spellComponent.ManaCount}/{stats.ManaControl.ActualValue}";
        }
        else
        {
            manaLabel.Text = $"mp: unavailable";
        }
    }

    public void HideInfo()
    {
        Visible = false;
    }
}