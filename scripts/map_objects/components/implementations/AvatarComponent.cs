using Godot;

public class AvatarComponent: Node
{
    [Export]
    private Texture avatar;
    public Texture Avatar => avatar;

}