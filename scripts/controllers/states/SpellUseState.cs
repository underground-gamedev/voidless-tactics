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

    private Task<BaseControllerState> HandleSpell(MapCell targetCell)
    {
        if (activeSpell.CastAvailable(targetCell))
        {
            activeSpell.ApplyEffect(targetCell);
            return NextState(new ActiveCharacterState(active));
        }

        return NextState(new SpellSelectState(active));
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return HandleSpell(active.Map.CellBy(x, y));
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        return HandleSpell(character.Cell);
    }

    protected override void GenericCellHover(MapCell cell)
    {
        base.GenericCellHover(cell);

        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        BaseHighlight();
        if (!activeSpell.CastAvailable(cell)) return;
        EffectAreaHighlight(cell);
    }

    private void BaseHighlight()
    {
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);
        foreach (var cell in activeSpell.GetTargetArea())
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.LongMove);
        }
    }

    private void EffectAreaHighlight(MapCell hoverCell)
    {
        var highlightLayer = map.MoveHighlightLayer;
        foreach (var cell in activeSpell.GetEffectArea(hoverCell))
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.Attack);
        }
    }

    public override void OnEnter()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);
        hud?.HideMenuWithActions();

        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        BaseHighlight();
    }

    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();

        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
    }
}