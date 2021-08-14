using System.Collections.Generic;
using Godot;

public class CharacterHUD: Control
{
    private Label orderLabel;
    public Label OrderLabel => orderLabel = orderLabel ?? GetNode<Label>("OrderLabel");

    public override void _Ready()
    {
        OrderLabel.Visible = false;
    }

    public void OnRoundStart(Character character)
    {
        orderLabel.Visible = false;
    }

    public void OnTurnPlanned(Character character, List<Character> plan)
    {
        var orderComponent = character.Components.GetComponent<TurnOrderComponent>();
        if (orderComponent == null) return;
        orderLabel.Visible = true;
        orderLabel.Text = orderComponent.OrderNumber.ToString();
    }

    public void OnRoundEnd(Character character)
    {
        orderLabel.Visible = false;
    }
}