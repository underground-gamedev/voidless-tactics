using Godot;

public class TurnQueueElement: Control
{
    [Export]
    private Texture defaultTexture;

    private Character character;
    private Vector2 baseSize;
    private Vector2 baseScale;
    private TextureRect avatar;
    public TextureRect Avatar => avatar = avatar ?? GetNode<TextureRect>("Avatar");
    public override void _Ready()
    {
        baseSize = RectMinSize;
        baseScale = Avatar.RectScale;
    }
    public void SetCharacter(Character character)
    {
        this.character = character;
        var avatarComponent = character.Components.FindChild<AvatarComponent>();
        var characterTexture = avatarComponent?.Avatar ?? defaultTexture;
        Avatar.Texture = characterTexture;
        GetNode<Label>("TurnOrder").Text = character.Components.FindChild<TurnOrderComponent>().OrderNumber.ToString();
    }

    public void SetActive(bool enabled)
    {
        var newSize = enabled ? baseSize * 1.4f : baseSize;
        RectMinSize = newSize;
        RectSize = newSize;

        var newScale = enabled ? baseScale * 1.3f : baseScale;
        Avatar.RectScale = newScale;
    }
}