using Godot;

public class BasicStats: Node
{
    private Stat health;
    private Stat initiative;
    private Stat damage;
    private Stat speed;
    private Stat manaControl;
    private Stat spellPower;

    public Stat Health => health = health ?? GetNode<Stat>("Health");
    public Stat Damage => damage = damage ?? GetNode<Stat>("Damage");
    public Stat Initiative => initiative = initiative ?? GetNode<Stat>("Initiative");
    public Stat Speed => speed = speed ?? GetNode<Stat>("Speed");
    public Stat ManaControl => manaControl = manaControl ?? GetNode<Stat>("ManaControl");
    public Stat SpellPower => spellPower = spellPower ?? GetNode<Stat>("SpellPower");
}