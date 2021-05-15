using Godot;
using System;

public class Main : Node
{
	[Export]
	public PackedScene Mob;

	private int _score;
	private Random _random = new Random();

	private Timer _cachedMobTimer;
	private Timer _cachedScoreTimer;
	private Timer _cachedStartTimer;
	private Position2D _cachedStartPosition;

	public override void _Ready()
	{
		_cachedMobTimer = GetNode<Timer>("MobTimer");
		_cachedScoreTimer = GetNode<Timer>("ScoreTimer");
		_cachedStartTimer = GetNode<Timer>("StartTimer");
		_cachedStartPosition = GetNode<Position2D>("StartPosition");

		_cachedStartTimer.Connect("timeout", this, nameof(OnStartTimerTimeout));
		_cachedScoreTimer.Connect("timeout", this, nameof(OnScoreTimerTimeout));
		_cachedMobTimer.Connect("timeout", this, nameof(OnMobTimerTimeout));

		NewGame();
	}

	public void GameOver()
	{
		_cachedMobTimer.Stop();
		_cachedScoreTimer.Stop();
	}

	public void NewGame()
	{
		_score = 0;

		var player = GetNode<Player>("Player");
		player.Start(_cachedStartPosition.Position);

		_cachedStartTimer.Start();
	}

	public void OnStartTimerTimeout()
	{
		_cachedMobTimer.Start();
		_cachedScoreTimer.Start();
	}

	public void OnScoreTimerTimeout()
	{
		_score++;
	}

	public void OnMobTimerTimeout()
	{
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.Offset = _random.Next();

		var mobInstance = Mob.Instance<RigidBody2D>();
		AddChild(mobInstance);

		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		mobInstance.Position = mobSpawnLocation.Position;

		direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mobInstance.Rotation = direction;

		//var minSpeed = mobInstance.MinSpeed;
		//var maxSpeed = mobInstance.MaxSpeed;
		mobInstance.LinearVelocity = new Vector2(RandRange(150f, 250f), 0).Rotated(direction);
	}

	private float RandRange(float min, float max)
	{
		return (float)_random.NextDouble() * (max - min) + min;
	}
	public override void _Process(float delta)
	{
		
	}
}
