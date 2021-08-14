using Godot;

public class AttackHoverState: SimpleHoverState
{
    InteractableSelectState parent;

    private int lastX;
    private int lastY;
    private Color savedModulateColor = new Color(1, 1, 1, 1);

    public AttackHoverState(InteractableSelectState parent)
    {
        this.parent = parent; 
    }

    private void CleanLast()
    {
        if (!Map.IsOutOfBounds(lastX, lastY))
        {
            var lastCharacter = Map.GetCharacter(lastX, lastY);
            if (lastCharacter != null)
            {
                BaseHighlight();
                lastCharacter.Modulate = savedModulateColor;
            }
        }
    }

    public override bool OnCellHover(int x, int y, Vector2 offset)
    {
        base.OnCellHover(x, y, offset);

        CleanLast();

        this.CharacterByPos(x, y, (character) => {
            var availableAttackTargets = parent.AvailableAttackTargets;
            if (!availableAttackTargets.Contains(character)) return false;
            var attackSrc = parent.GetAttackSourcePos(x, y, offset);
            Map.MoveHighlightLayer.Highlight(attackSrc.X, attackSrc.Y, MoveHighlightType.Active);

            savedModulateColor = character.Modulate;
            character.Modulate = character.Modulate + new Color(0.6f, 0.1f, 0.1f);
            return true;
        });

        lastX = x;
        lastY = y;
        return true;
    }

    public void BaseHighlight()
    {
        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        var activePosition = controller.ActiveCharacter.Cell;
        highlightLayer.Highlight(activePosition.X, activePosition.Y, MoveHighlightType.Active);
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
        CleanLast();
        Map.MoveHighlightLayer.Clear();
    }
}