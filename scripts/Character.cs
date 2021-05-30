using Godot;
using System;

public class Character : Node2D
{
    [Export]
    private int movePoints = 6;
    private Vector2 pos;
    private TacticMap map;
    private bool moved;

    private TileMap highlightMovement;

    public override void _Ready()
    {
        highlightMovement = GetNode<TileMap>("MovementArea");
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

    public void SetHighlightAvailableMovement(bool enabled)
    {            
        highlightMovement.Clear();
        if (!enabled)
        {
            return;
        }

        var availablePositions = map.GetAllAvailablePathDest(pos, movePoints);
        var highlightTile = highlightMovement.TileSet.FindTileByName("normal_move");
        foreach (var availPos in availablePositions)
        {
            var localPos = availPos - pos;
            highlightMovement.SetCell((int)localPos.x, (int)localPos.y, highlightTile);
        }
        highlightMovement.SetCell(0, 0, -1);
    }

    public async void MoveTo(int targetX, int targetY)
    {
        if (moved) return;
        if (new Vector2(targetX, targetY) == pos) return;

        int x = (int)pos.x;
        int y = (int)pos.y;

        var path = map.Pathfind(new Vector2(x, y), new Vector2(targetX, targetY));

        if (path == null)
        {
            return;
        }

        var cost = path.Length - 1;

        if (cost <= 0 || cost > movePoints)
        {
            return;
        }

        moved = true;
        var storeVisible = highlightMovement.Visible;
        highlightMovement.Visible = false;
        foreach(var pos in path)
        {
            this.pos = pos;
            SyncWithMap(map.TileMap);
            await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
        }
        moved = false;
        highlightMovement.Visible = storeVisible;
        movePoints = 0;
        SetHighlightAvailableMovement(true);
    }
}
