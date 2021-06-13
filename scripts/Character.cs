using Godot;
using System;
using System.Threading.Tasks;

public class Character : Node2D
{
    [Export]
    private int movePoints = 6;
    [Export]
    private int health = 5;
    [Export]
    private int attackDamage = 2;

    private int moveActions = 1;
    private int fullActions = 1;

    public AbstractController controller;
    private MapCell cell;
    public MapCell Cell => cell;
    private TacticMap map;
    private bool moved;

    public int MoveActions => moveActions;
    public int MovePoints { get => movePoints; set => movePoints = value; }

    public int Health { get => health; }

    public int AttackDamage { get => attackDamage; }

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

    public void OnTurnStart()
    {
        moveActions = 1;
        fullActions = 1;
    }

    public bool MoveAvailable()
    {
        return moveActions > 0;
    }

    public bool AttackAvailable()
    {
        return fullActions > 0;
    }

    /*public void SetHighlightAvailableMovement(bool enabled)
    {            
        var highlightMovement = TileMap;

        highlightMovement.Clear();
        highlightMovement.Visible = enabled;

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
    }*/

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

        moveActions -= 1;
        moved = true;

        SetCell(map.CellBy(targetX, targetY));
        foreach(var (posX, posY) in path)
        {
            SyncWithMap(map.VisualLayer.TileMap, posX, posY);
            await this.Wait(0.1f);
        }
        moved = false;
    }

    public void Attack(Character target)
    {
        target.Hit(attackDamage);
        fullActions -= 1;
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        controller.RemoveCharacter(this);
    }
}
