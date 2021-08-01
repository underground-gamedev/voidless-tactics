public class CharacterDragHoverState : SimpleHoverState
{
    private float baseAlpha; 
    private Character activeCharacter;

    public CharacterDragHoverState(Character character): base(character)
    {
        activeCharacter = character;
    }

    public override bool OnCellHover(int x, int y)
    {
        base.OnCellHover(x, y);
        return this.CellByPos(x, y, (cell) => {
            activeCharacter.SyncWithMap(activeCharacter.Map, x, y);
            return true;
        });
    }

    public override void OnEnter()
    {
        base.OnEnter();
        baseAlpha = activeCharacter.Modulate.a;
        var modulate = activeCharacter.Modulate;
        modulate.a = baseAlpha * 0.8f;
        activeCharacter.Modulate = modulate;
        activeCharacter.ZIndex++;
    }

    public override void OnLeave()
    {
        base.OnLeave();
        var modulate = activeCharacter.Modulate;
        modulate.a = baseAlpha;
        activeCharacter.Modulate = modulate;
        activeCharacter.ZIndex--;

        activeCharacter.SyncWithMap(activeCharacter.Map);
    }
}