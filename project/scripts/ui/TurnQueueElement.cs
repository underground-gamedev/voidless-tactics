using Godot;

public class TurnQueueElement: Control
{
    [Export]
    private Texture defaultSmallTexture;
    [Export]
    private Texture defaultBigTexture;
    [Export]
    private bool useBigPortrait;

    private Character character;
    private Vector2 baseSize;
    private Vector2 baseScale;
    private TextureRect portrait;
    public TextureRect Portrait => portrait = portrait ?? GetNode<TextureRect>("Mask/Portrait");
    public override void _Ready()
    {
        baseSize = RectMinSize;
        baseScale = Portrait.RectScale;
    }
    public void SetCharacter(Character character)
    {
        this.character = character;
        var avatarComponent = character.Components.GetComponent<PortraitComponent>();
        Texture characterTexture;

        if (useBigPortrait)
        {
            characterTexture = avatarComponent?.PortraitBig ?? defaultSmallTexture;
        }
        else
        {
            characterTexture = avatarComponent?.PortraitSmall ?? defaultSmallTexture;
        }

        Portrait.Texture = characterTexture;
        var orderNumber = character.Components.GetComponent<TurnOrderComponent>().OrderNumber;
        GetNode<Label>("TurnOrder").Text = orderNumber.ToString();
    }
}