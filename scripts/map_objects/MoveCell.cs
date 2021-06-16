public struct MoveCell
{
    public MapCell MapCell;
    public int ActionNeed;

    public MoveCell(MapCell mapCell, int actionNeed)
    {
        MapCell = mapCell;
        ActionNeed = actionNeed; 
    }
}
