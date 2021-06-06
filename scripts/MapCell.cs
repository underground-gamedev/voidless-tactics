public class MapCell
{
    Character character;
    bool solid;

    public Character Character { get => character; set => character = value; }
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