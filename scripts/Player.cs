using Godot;
using System.Collections.Generic;

public class Player : Area2D
{
    [Signal]
    public delegate void Hit();

    [Export]
    public int Speed = 300;
    private Vector2 _screenSize;
    private AnimatedSprite _cachedAnimatedSprite;
    private CollisionShape2D _cachedCollisionShape;

    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
        _cachedAnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _cachedCollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        Connect("body_entered", this, nameof(OnPlayerBodyEnter));
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        _cachedCollisionShape.Disabled = false;
    }

    public override void _Process(float delta)
    {
        var direction = GetDirection();

        SetAnimation(direction);

        if (direction.Length() > 0) 
        {
            var velocity = direction.Normalized() * Speed;
            Position += velocity * delta;
            Position = new Vector2(
                x: Mathf.Clamp(Position.x, 0, _screenSize.x),
                y: Mathf.Clamp(Position.y, 0, _screenSize.y)
            );
        }
    }
    
    private void OnPlayerBodyEnter(PhysicsBody2D body)
    {
        Hide();
        EmitSignal(nameof(Hit));
        _cachedCollisionShape.SetDeferred("disabled", true);
    }

    private void SetAnimation(Vector2 direction)
    {
        var animatedSprite = _cachedAnimatedSprite;
        if (direction.Length() == 0)
        {
            animatedSprite.Stop();
            return;
        }

        if (direction.x != 0)
        {
            animatedSprite.Animation = "walk";
            animatedSprite.FlipV = false;
            animatedSprite.FlipH = direction.x < 0;
        }
        else if (direction.y != 0)
        {
            animatedSprite.Animation = "up";
            animatedSprite.FlipV = direction.y > 0;
            animatedSprite.FlipH = false;
        }

        animatedSprite.Play();
    }
    private Vector2 GetDirection()
    {
        var velocity = new Vector2();
        var directions = new Dictionary<string, Vector2>() 
        {
            ["ui_right"] = new Vector2(1, 0),
            ["ui_left"] = new Vector2(-1, 0),
            ["ui_down"] = new Vector2(0, 1),
            ["ui_up"] = new Vector2(0, -1),
        };

        foreach(var direction in directions.Keys) 
        {
            if (Input.IsActionPressed(direction)) 
            {
                velocity += directions[direction];
            }
        }

        return velocity;
    }
}
