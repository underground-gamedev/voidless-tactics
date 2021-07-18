using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public class TacticHUD: Node
{
    private Control turnHighlight;
    private Control characterInfo;
    private Control cellInfo;
    private ActionContainer actions;
    private MarginContainer spellDescriptor;
 
    [Signal]
    public delegate void ActionSelected(string actionName);

    [Signal]
    public delegate void EndTurnPressed();

    public override void _Ready()
    {
        turnHighlight = GetNode<Control>("TurnHighlight");
        turnHighlight.Visible = false;

        characterInfo = GetNode<Control>("Labels");
        characterInfo.Visible = false;

        actions = GetNode<ActionContainer>("ActionContainer");
        actions.Visible = false;
        actions.Connect(nameof(ActionContainer.ActionSelected), this, nameof(OnActionSelected));

        cellInfo = GetNode<Control>("CellInfo");
        cellInfo.Visible = false;

        spellDescriptor = GetNode<MarginContainer>("SpellDescriptor");
        spellDescriptor.Visible = false;

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
            HideCharacterDisplay(); 
            return;
        }

        characterInfo.Visible = true;
        var stats = character.BasicStats;
        characterInfo.GetNode<Label>("Labels/HealthLabel").Text = $"Health: {stats.Health.ActualValue}/{stats.Health.MaxValue}";
        characterInfo.GetNode<Label>("Labels/DamageLabel").Text = $"Damage: {stats.Damage.ActualValue}";
        characterInfo.GetNode<Label>("Labels/MoveLabel").Text = $"Speed: {stats.Speed.ActualValue}";
    }
    public void HideCharacterDisplay()
    {
        characterInfo.Visible = false;
    }

    public void DisplayCellInfo(MapCell cell)
    {
        if (cell == null)
        {
            HideCellInfo();
            return;
        }

        var mana = cell.Mana;
        cellInfo.Visible = true;
        cellInfo.GetNode<Label>("Labels/ManaTypeLabel").Text = $"Mana: {mana.ManaType.ToString()}";
        
        var densityValue = Math.Floor(mana.Density * 100);
        var densityString = mana.ManaType == ManaType.None ? "-" : densityValue.ToString() + "%";
        cellInfo.GetNode<Label>("Labels/DensityLabel").Text = $"Density: {densityString}";
        var cords = $"x:{cell.X} y:{cell.Y}";
        cellInfo.GetNode<Label>("Labels/CordsLabel").Text = $"{cords} solid:{cell.Solid}";
    }

    public void HideCellInfo()
    {
        cellInfo.Visible = false;
    }

    public void OnCameraDrag(Vector2 deltaScreenPosition)
	{
        actions.RectGlobalPosition -= deltaScreenPosition;
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

    public void DisplaySpellDescriptor(string description)
    {
        spellDescriptor.Visible = true;
        spellDescriptor.GetNode<Label>("DescriptionLabel").Text = description;
        spellDescriptor.RectSize = new Vector2(spellDescriptor.RectSize.x, 0);
    }

    public void HideSpellDescriptor()
    {
        spellDescriptor.Visible = false;
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