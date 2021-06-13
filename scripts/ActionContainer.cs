using Godot;
using System;
using System.Collections.Generic;

public class ActionContainer : MarginContainer
{
    [Export]
    public PackedScene ButtonPrefab;

    private Control actionContainer;

    [Signal]
    public delegate void ActionSelected(string actionName);

    public override void _Ready()
    {
        actionContainer = GetNode<Control>("Actions");
    }


    private void OnButtonPressed(Button button)
    {
        EmitSignal(nameof(ActionSelected), button.Name);
    }

    public void SetActions(List<string> actions)
    {
        foreach (Node child in actionContainer.GetChildren())
        {
            actionContainer.RemoveChild(child);
            child.QueueFree();
        }

        foreach (var actionName in actions)
        {
            var button = ButtonPrefab.Instance<SelfIdentificateButton>();
            button.Name = actionName;
            button.Text = actionName;
            button.Connect(nameof(SelfIdentificateButton.PressedExtended), this, nameof(OnButtonPressed));
            actionContainer.AddChild(button);
        }
    }
}
