using Godot;
using System;
using System.Threading.Tasks;

public class Character : Node2D
{
    [Export]
    private int movePoints = 6;
    public AbstractController controller;

    private int moveActions = 1;
    private MapCell cell;
    public MapCell Cell => cell;
    private TacticMap map;
    private bool moved;

    private TileMap highlightMovement;
    private bool highlightEnabled;

    public int MovePoints { get => movePoints; set => movePoints = value; }

    public override void _Ready()
    {
        highlightMovement = GetNode<TileMap>("MovementArea");
    }

    public void SyncWithMap(TileMap tilemap)
    {
        var (x, y) = cell.Position;
        SyncWithMap(tilemap, x, y);
    }

    public void SyncWithMap(TileMap tilemap, int x, int y)
    {
        GlobalPosition = tilemap.GlobalPosition + tilemap.CellSize * (new Vector2(x, y) + new Vector2(0.5f, 0.5f));
    }

    private void SetCell(MapCell cell)
    {
        if (this.cell != null)
        {
            this.cell.Character = null;
        }
        this.cell = cell;
        cell.Character = this;
    }

    public void BindMap(TacticMap map, MapCell cell)
    {
        this.map = map;
        SetCell(cell);
        SyncWithMap(map.VisualLayer.TileMap);
    }

    public void SetHighlightAvailableMovement(bool enabled)
    {            
        highlightMovement.Clear();
        highlightEnabled = enabled;
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
        SetHighlightAvailableMovement(highlightEnabled);
    }

    public async Task MoveTo(int targetX, int targetY)
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

        SetCell(map.CellBy(targetX, targetY));
        foreach(var (posX, posY) in path)
        {
            SyncWithMap(map.VisualLayer.TileMap, posX, posY);
            await this.Wait(0.1f);
        }
        moved = false;
        highlightMovement.Visible = storeVisible;

        moveActions -= 1;

        SetHighlightAvailableMovement(true);
    }
}
