using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpellSelectState: BaseControllerState
{
    protected Character active;
    protected ISpellComponent spellComponent;

    public SpellSelectState(Character character)
    {
        active = character;
        spellComponent = character.Components.FindChild<ISpellComponent>();
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        if (character == active) return Async(this);

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }
        return NextState(new EnemySelectedState(character));
    }

    public override Task<BaseControllerState> MenuActionSelected(string action)
    {
        var spell = spellComponent.GetSpellByName(action);
        if (spell == null) return Async(this);
        return NextState(new SpellUseState(active, spell));
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return NextState(new UnselectedControllerState());
    }

    public override void OnEnter()
    {
        map.MoveHighlightLayer.Clear();
        map.MoveHighlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        var availableSpells = spellComponent.GetAvailableSpellNames();
        hud?.DisplayMenuWithActions(active.GetGlobalTransformWithCanvas().origin + new Godot.Vector2(20f, 0.5f), availableSpells);
        hud?.DisplayCharacter(active);
    }

    public override void OnLeave()
    {
        map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideMenuWithActions();
        hud?.ResetCharacterDisplay();
    }
}