using Godot;

public class MapObject: Node2D
{
    protected MapCell cell;
    public MapCell Cell => cell;
    protected TacticMap map;
    public TacticMap Map => map;

    public void SyncWithMap(TileMap tilemap)
    {
        var (x, y) = cell.Position;
        SyncWithMap(tilemap, x, y);
    }

    public void SyncWithMap(TileMap tilemap, int x, int y)
    {
        var offset = tilemap.CellSize * (new Vector2(x, y) + new Vector2(0.5f, 0.5f));
        GlobalPosition = tilemap.GlobalPosition + offset;
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
        SyncWithMap(map.VisualLayer.TileMap);
    }

}