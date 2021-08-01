using Godot;

public class MapObject: Node2D
{
    protected MapCell cell;
    public MapCell Cell => cell;
    protected TacticMap map;
    public TacticMap Map => map;

    public override void _Ready()
    {
        Visible = false;
    }

    public void SyncWithMap(TacticMap map)
    {
        var (x, y) = cell.Position;
        SyncWithMap(map, x, y);
    }

    public void SyncWithMap(TacticMap map, int x, int y)
    {
        var tilemap = map.VisualLayer.TileMap;
        var offset = tilemap.CellSize * (new Vector2(x, y) + new Vector2(0.5f, 0.5f));
        GlobalPosition = tilemap.GlobalPosition + offset;
        if (!map.IsOutOfBounds(x, y))
        {
            ZIndex = map.VisualLayer.GetZ(map, map.CellBy(x, y), 0);
        }
    }

    public void SetCell(MapCell cell)
    {
        if (this.cell != null)
        {
            this.cell.MapObject = null;
        }
        this.cell = cell;
        cell.MapObject = this;
    }

    public void BindMap(TacticMap map, MapCell cell)
    {
        this.map = map;
        SetCell(cell);
        SyncWithMap(map);
        Visible = true;
    }

}