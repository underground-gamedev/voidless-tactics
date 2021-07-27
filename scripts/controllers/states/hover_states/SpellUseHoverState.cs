public class SpellUseHoverState : SimpleHoverState
{
    private Character activeCharacter;
    private ISpell activeSpell;

    private MoveHighlightLayer HighlightLayer => controller.Map.MoveHighlightLayer;

    public SpellUseHoverState(Character character, ISpell spell): base(character)
    {
        activeCharacter = character;
        activeSpell = spell; 
    }

    private void BaseHighlight()
    {
        var (activeX, activeY) = activeCharacter.Cell.Position;
        var highlightLayer = HighlightLayer;
        highlightLayer.Highlight(activeX, activeY, MoveHighlightType.Active);
        foreach (var cell in activeSpell.GetTargetArea())
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.LongMove);
        }
    }

    private void EffectAreaHighlight(MapCell hoverCell)
    {
        var highlightLayer = HighlightLayer;
        foreach (var cell in activeSpell.GetEffectArea(hoverCell))
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.Attack);
        }
    }

    public override bool OnCellHover(int x, int y)
    {
        base.OnCellHover(x, y);
        return this.CellByPos(x, y, (cell) => {
            var highlightLayer = HighlightLayer;
            highlightLayer.Clear();
            BaseHighlight();
            if (!activeSpell.CastAvailable(cell)) return false;
            EffectAreaHighlight(cell);
            return true;
        });
    }

    public override void OnEnter()
    {
        base.OnEnter();
        HighlightLayer.Clear();
        BaseHighlight();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        HighlightLayer.Clear();
    }
}