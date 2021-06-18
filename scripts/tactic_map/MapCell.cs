public class MapCell
{
    ManaCell manaCell = new ManaCell();
    MapObject mapObject;
    bool solid;

    public ManaCell Mana => manaCell;
    public MapObject MapObject { get => mapObject; set => mapObject = value; }
    public bool Solid { get => solid; set => solid = value; }
    public int X { get; }
    public int Y { get; }

    public (int, int) Position => (X, Y);

    public MapCell(int x, int y)
    {
        X = x;
        Y = y;
    }

}