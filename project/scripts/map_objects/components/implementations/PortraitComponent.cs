using Godot;

public class PortraitComponent: Node
{
    [Export]
    private Texture portraitSmall;
    public Texture PortraitSmall => portraitSmall;

    [Export]
    private Texture portraitBig;
    public Texture PortraitBig => portraitBig;
}