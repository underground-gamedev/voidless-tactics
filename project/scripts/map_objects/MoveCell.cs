public struct MoveCell
{
    public MapCell MapCell;
    public bool WeakAttack;

    public MoveCell(MapCell mapCell, bool weakAttack)
    {
        MapCell = mapCell;
        WeakAttack = weakAttack;
    }
}
