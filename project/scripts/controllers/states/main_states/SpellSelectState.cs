using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class SpellSelectState: BaseControllerState
{
    protected ISpellComponent spellComponent => controller.ActiveCharacter.Components.GetComponent<ISpellComponent>();

    public override bool CellClick(int x, int y, Vector2 offset)
    {
        controller.MainStates.PopState();
        return true;
    }

    public override bool MenuActionSelected(string action)
    {
        var spell = spellComponent.GetSpellByName(action);
        if (spell == null)
        {
            controller.MainStates.PopState();
            return false;
        }

        controller.MainStates.ReplaceState(new SpellUseState(spell));
        return true;
    }

    public override void OnEnter()
    {
        var activeCharacter = controller.ActiveCharacter;
        var map = controller.Map;
        map.MoveHighlightLayer.Clear();
        map.MoveHighlightLayer.Highlight(activeCharacter.Cell.X, activeCharacter.Cell.Y, MoveHighlightType.Active);

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        var availableSpells = spellComponent.GetAvailableSpellNames();
        hud?.DisplayMenuWithActions(availableSpells);
        hud?.DisplayActiveCharacter(activeCharacter);
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