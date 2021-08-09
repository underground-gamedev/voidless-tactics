using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class SpellSelectState: BaseControllerState
{
    protected Character active;
    protected ISpellComponent spellComponent;

    public SpellSelectState(Character character)
    {
        active = character;
        spellComponent = character.Components.FindChild<ISpellComponent>();
    }

    public override bool CellClick(int x, int y, Vector2 offset)
    {
        controller.MainStates.PopState();
        return false;
    }

    public override bool MenuActionSelected(string action)
    {
        var spell = spellComponent.GetSpellByName(action);
        if (spell == null)
        {
            controller.MainStates.PopState();
            return false;
        }

        controller.MainStates.ReplaceState(new SpellUseState(active, spell));
        return true;
    }

    public override void OnEnter()
    {
        var map = controller.Map;
        map.MoveHighlightLayer.Clear();
        map.MoveHighlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        var availableSpells = spellComponent.GetAvailableSpellNames();
        hud?.DisplayMenuWithActions(active.GetGlobalTransformWithCanvas().origin + new Godot.Vector2(20f, 0.5f), availableSpells);
        hud?.DisplayActiveCharacter(active);
    }

    public override void OnLeave()
    {
        var map = controller.Map;
        map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideMenuWithActions();
        hud?.HideActiveCharacter();
    }
}