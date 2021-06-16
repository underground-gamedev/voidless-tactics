using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

public class Character : MapObject, IBasicStatsHandler
{
    private BasicStats basicStats;
    public BasicStats BasicStats => basicStats = basicStats ?? GetNode<BasicStats>("Stats");

    private AbstractController controller;
    public AbstractController Controller {
        get => controller;
        set => controller = value;
    }

    private Node components;
    public Node Components => components = components ?? GetNode<Node>("Components");

    public void OnTurnStart()
    {
        BasicStats.MoveActions.ActualValue = BasicStats.MoveActions.MaxValue;
        BasicStats.FullActions.ActualValue = BasicStats.FullActions.MaxValue;
    }

    public bool AttackAvailable()
    {
        return BasicStats.FullActions.ActualValue > 0;
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
