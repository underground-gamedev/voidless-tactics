using Godot;
using System;
using System.Threading.Tasks;

public class Character : MapObject
{
    private BasicStats basicStats;
    public BasicStats BasicStats => basicStats = basicStats ?? GetNode<BasicStats>("Stats");

    private AbstractController controller;
    public AbstractController Controller {
        get => controller;
        set => controller = Controller;
    }
    private bool moved;

    public void OnTurnStart()
    {
        BasicStats.MoveActions.ActualValue = BasicStats.MoveActions.MaxValue;
        BasicStats.FullActions.ActualValue = BasicStats.FullActions.MaxValue;
    }

    public bool MoveAvailable()
    {
        return BasicStats.MoveActions.ActualValue > 0;
    }

    public bool AttackAvailable()
    {
        return BasicStats.FullActions.ActualValue > 0;
    }

    public async Task MoveTo(int targetX, int targetY)
    {
        if (moved) return;
        if (BasicStats.MoveActions.ActualValue == 0) return;
        if (new Vector2(targetX, targetY) == new Vector2(cell.X, cell.Y)) return;

        int x = cell.X;
        int y = cell.Y;

        var path = map.PathfindLayer.Pathfind(new Vector2(x, y), new Vector2(targetX, targetY));

        if (path == null)
        {
            return;
        }

        var cost = path.Length - 1;

        if (cost <= 0 || cost > BasicStats.Speed.ActualValue)
        {
            return;
        }

        BasicStats.MoveActions.ActualValue -= 1;
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
        target.Hit(BasicStats.Damage.ActualValue);
        BasicStats.FullActions.ActualValue -= 1;
    }

    public void Hit(int damage)
    {
        var health = BasicStats.Health;
        health.ActualValue -= damage;

        if (health.ActualValue <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        controller?.RemoveCharacter(this);
        GetParent().RemoveChild(this);
        Cell.MapObject = null;
        QueueFree();
    }
}
