using Godot;
using System;

public class Character : Node2D
{
    [Export]
    private int movePoints = 6;
    public TeamController controller;

    private int moveActions = 1;
    private MapCell cell;
    private TacticMap map;
    private bool moved;

    private TileMap highlightMovement;

    public int MovePoints { get => movePoints; set => movePoints = value; }

    public override void _Ready()
    {
        highlightMovement = GetNode<TileMap>("MovementArea");
    }

    public void SyncWithMap(TileMap tilemap)
    {
        var (x, y) = cell.Position;
        GlobalPosition = tilemap.GlobalPosition + tilemap.CellSize * (new Vector2(x, y) + new Vector2(0.5f, 0.5f));
    }

    public void BindMap(TacticMap map, MapCell cell)
    {
        this.map = map;
        this.cell = cell;
        SyncWithMap(map.VisualLayer.TileMap);
    }

    public void SetHighlightAvailableMovement(bool enabled)
    {            
        highlightMovement.Clear();
        if (!enabled) return;
        if (moveActions == 0) return;

        var availablePositions = map.PathfindLayer.GetAllAvailablePathDest(cell, movePoints);
        var highlightTile = highlightMovement.TileSet.FindTileByName("normal_move");
        foreach (var (availX, availY) in availablePositions)
        {
            var localX = availX - cell.X;
            var localY = availY - cell.Y;
            highlightMovement.SetCell((int)localX, (int)localY, highlightTile);
        }
        highlightMovement.SetCell(0, 0, -1);
    }

    public void OnTurnStart()
    {
        moveActions = 1;
    }

    public async void MoveTo(int targetX, int targetY)
    {
        if (moved) return;
        if (moveActions == 0) return;
        if (new Vector2(targetX, targetY) == new Vector2(cell.X, cell.Y)) return;

        int x = cell.X;
        int y = cell.Y;

        var path = map.PathfindLayer.Pathfind(new Vector2(x, y), new Vector2(targetX, targetY));

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
        foreach(var (posX, posY) in path)
        {
            cell = map.CellBy(posX, posY);
            SyncWithMap(map.VisualLayer.TileMap);
            await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
        }
        moved = false;
        highlightMovement.Visible = storeVisible;

        moveActions -= 1;

        SetHighlightAvailableMovement(true);
    }
}
