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

    private UnitInfoPanel hoverUnitInfo;
    private UnitInfoPanel activeUnitInfo;

 
    [Signal]
    public delegate void ActionSelected(string actionName);

    [Signal]
    public delegate void EndTurnPressed();

    public override void _Ready()
    {
        turnHighlight = GetNode<Control>("TurnHighlight");
        turnHighlight.Visible = false;

        hoverUnitInfo = GetNode<UnitInfoPanel>("HoverUnitInfo");
        activeUnitInfo = GetNode<UnitInfoPanel>("ActiveUnitInfo");

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

    public void DisplayHoverCharacter(Character character)
    {
        hoverUnitInfo.DisplayInfo(character);
    }
    public void HideHoverCharacter()
    {
        hoverUnitInfo.HideInfo();
    }

    public void DisplayActiveCharacter(Character character)
    {
        activeUnitInfo.DisplayInfo(character);
    }

    public void HideActiveCharacter()
    {
        activeUnitInfo.HideInfo();
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

    public void OnCameraZoom(Vector2 prevZoom, Vector2 deltaZoom)
    {
        Vector2 boxGlobalCenteredOffset = actions.RectGlobalPosition - GetViewport().GetVisibleRect().Size / 2;

        Vector2 zoomMultiprier = (prevZoom) / (prevZoom + deltaZoom);

        Vector2 totalOffset = boxGlobalCenteredOffset * (zoomMultiprier - Vector2.One);
        actions.RectGlobalPosition += totalOffset;

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
        spellDescriptor.GetNode<RichTextLabel>("DescriptionLabel").BbcodeText = description;
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