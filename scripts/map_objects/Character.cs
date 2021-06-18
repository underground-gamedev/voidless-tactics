using System;
using Godot;

public class Character : MapObject, IBasicStatsPresenter
{
    private BasicStats basicStats;
    public BasicStats BasicStats => basicStats = basicStats ?? GetNode<BasicStats>("Stats");

    private Node components;
    public Node Components => components = components ?? GetNode<Node>("Components");

    private AbstractController controller;
    public AbstractController Controller {
        get => controller;
        set => controller = value;
    }

    public void OnTurnStart()
    {
    }

    public void Kill()
    {
        controller?.RemoveCharacter(this);
        GetParent().RemoveChild(this);
        Cell.MapObject = null;
        QueueFree();
    }

    public void OnTurnEnd()
    {
        BasicStats.MoveActions.ActualValue = BasicStats.MoveActions.MaxValue;
        BasicStats.FullActions.ActualValue = BasicStats.FullActions.MaxValue;
    }
}
