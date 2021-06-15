using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public class TacticHUD: Node
{
    private Control turnHighlight;
    private Control labels;
    private ActionContainer actions;
 
    [Signal]
    public delegate void ActionSelected(string actionName);

    [Signal]
    public delegate void EndTurnPressed();

    public override void _Ready()
    {
        turnHighlight = GetNode<Control>("TurnHighlight");
        turnHighlight.Visible = false;

        labels = GetNode<Control>("Labels");
        labels.Visible = false;

        actions = GetNode<ActionContainer>("ActionContainer");
        actions.Visible = false;
        actions.Connect(nameof(ActionContainer.ActionSelected), this, nameof(OnActionSelected));

        GetNode<Button>("EndTurnButton/Button").Connect("pressed", this, nameof(OnEndTurnPressed));
    }

    private void OnActionSelected(string actionName)
    {
        EmitSignal(nameof(ActionSelected), actionName);
    }

    private void OnEndTurnPressed()
    {
        EmitSignal(nameof(EndTurnPressed));
    }

    public void DisplayCharacter(Character character)
    {
        if (character == null) { 
            ResetCharacterDisplay(); 
            return;
        }

        labels.Visible = true;
        labels.GetNode<Label>("Labels/HealthLabel").Text = $"Health: {character.BasicStats.Health.ActualValue}";
        labels.GetNode<Label>("Labels/DamageLabel").Text = $"Damage: {character.BasicStats.Damage.ActualValue}";
        labels.GetNode<Label>("Labels/MoveLabel").Text = $"Speed: {character.BasicStats.Speed.ActualValue}";
    }

    public void ResetCharacterDisplay()
    {
        labels.Visible = false;
    }

    public void DisplayMenuWithActions(Vector2 screenPosition, List<string> actionList)
    {
        if (actionList is null || actionList.Count == 0)
        {
            HideMenuWithActions();
            return;
        }

        screenPosition.y -= actions.RectSize.y;
        actions.RectPosition = screenPosition;
        actions.Visible = true;
        actions.SetActions(actionList);
    }

    public void HideMenuWithActions()
    {
        actions.Visible = false;
    }
    
    public async Task ShowTurnLabel(string text)
    {
        var turnLine = GetNode<Control>("TurnHighlight");
        var label = turnLine.GetNode<Label>("TurnLabel");
        label.Text = text;
        turnLine.Visible = true;
        await this.Wait(1);
        turnLine.Visible = false;
    }
}