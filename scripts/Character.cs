using Godot;
using System;

public class Character : Node2D
{
    private Vector2 pos;
    private TacticMap map;
    private bool moved;

    public override void _Ready()
    {
    }

    public void SyncWithMap(TileMap tilemap)
    {
        GlobalPosition = tilemap.GlobalPosition + tilemap.CellSize * (pos + new Vector2(0.5f, 0.5f));
    }

    public void BindMap(TacticMap map, Vector2 pos)
    {
        this.map = map;
        this.pos = pos;
        SyncWithMap(map.TileMap);
    }

    public async void MoveTo(int targetX, int targetY)
    {
        if (moved) return;

        int x = (int)pos.x;
        int y = (int)pos.y;

        var path = map.Pathfind(new Vector2(x, y), new Vector2(targetX, targetY));

        if (path == null || path.Length == 0)
        {
            return;
        }

        moved = true;
        foreach(var pos in path)
        {
            this.pos = pos;
            SyncWithMap(map.TileMap);
            await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
        }
        moved = false;
    }
}
