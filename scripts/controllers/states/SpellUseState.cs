using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpellUseState: BaseControllerState
{
    private Character active;
    private ISpell activeSpell;

    public SpellUseState(Character character, ISpell spell)
    {
        active = character;
        activeSpell = spell;
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return NextState(new SpellSelectState(active));
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        var targetCell = character.Cell;
        if (activeSpell.CastAvailable(targetCell))
        {
            activeSpell.ApplyEffect(targetCell);
            return NextState(new ActiveCharacterState(active));
        }

        return NextState(new SpellSelectState(active));
    }

    private void DisplayOnHUD(Character character)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(character);
    }

    public override void OnEnter()
    {
        DisplayOnHUD(active);

        map.MoveHighlightLayer.Clear();
        map.MoveHighlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        UserInterfaceService.GetHUD<TacticHUD>()?.HideMenuWithActions();

        var highlightLayer = map.MoveHighlightLayer;
        foreach (var cell in activeSpell.GetTargetArea())
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.Attack);
        }
    }

    public override void OnLeave()
    {
        DisplayOnHUD(null);
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
    }
}