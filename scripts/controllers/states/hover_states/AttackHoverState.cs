using Godot;

public class AttackHoverState: SimpleHoverState
{
    InteractableSelectState parent;

    public AttackHoverState(InteractableSelectState parent)
    {
        this.parent = parent; 
    }

    public override bool OnCellHover(int x, int y, Vector2 offset)
    {
        base.OnCellHover(x, y, offset);

        BaseHighlight();
        this.CharacterByPos(x, y, (character) => {
            var availableAttackTargets = parent.AvailableAttackTargets;
            if (!availableAttackTargets.Contains(character)) return false;
            var attackSrc = parent.GetAttackSourcePos(x, y, offset);
            Map.MoveHighlightLayer.Highlight(attackSrc.X, attackSrc.Y, MoveHighlightType.Active);
            return true;
        });

        return true;
    }

    public void BaseHighlight()
    {
        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        highlightLayer.Highlight(parent.ActiveCharacter.Cell.X, parent.ActiveCharacter.Cell.Y, MoveHighlightType.Active);
        foreach (var moveCell in parent.AvailableMoveCells)
        {
            var (x, y) = moveCell.MapCell.Position;
            var highlightType = moveCell.ActionNeed == 1 ? MoveHighlightType.NormalMove : MoveHighlightType.LongMove;
            highlightLayer.Highlight(x, y, highlightType);
        }
        parent.AvailableAttackTargets.ForEach(ch => highlightLayer.Highlight(ch.Cell.X, ch.Cell.Y, MoveHighlightType.Attack));
    }

    public override void OnEnter()
    {
        BaseHighlight();
    }

    public override void OnLeave()
    {
        Map.MoveHighlightLayer.Clear();
    }
}