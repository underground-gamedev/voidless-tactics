using Godot;

public class BasicStats: Node
{
    private Stat health;
    private Stat damage;
    private Stat speed;
    private Stat moveActions;
    private Stat fullActions;

    public Stat Health => health = health ?? GetNode<Stat>("Health");
    public Stat Damage => damage = damage ?? GetNode<Stat>("Damage");
    public Stat Speed => speed = speed ?? GetNode<Stat>("Speed");
    public Stat MoveActions => moveActions = moveActions ?? GetNode<Stat>("MoveActions");
    public Stat FullActions => fullActions = fullActions ?? GetNode<Stat>("FullActions");
}