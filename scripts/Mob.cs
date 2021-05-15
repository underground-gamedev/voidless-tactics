using Godot;
using System;

public class Mob : RigidBody2D
{
	static private Random _random = new Random();

	[Export]
	public int MinSpeed = 150;
	[Export]
	public int MaxSpeed = 250;

	public override void _Ready()
	{
		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		var mobTypes = animatedSprite.Frames.GetAnimationNames();
		animatedSprite.Animation = mobTypes[_random.Next(0, mobTypes.Length)];
		GetNode<VisibilityNotifier2D>("VisibilityNotifier2D").Connect("screen_exited", this, nameof(ScreenExited));
	}

	public void ScreenExited()
	{
		QueueFree();
	}

	/*public override void _Process(float delta)
	{
	}*/
}
