using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene Mob;

    private int _score;
    private Random _random = new Random();


    private UI _ui;
    private Timer _mobTimer;
    private Timer _scoreTimer;
    private Timer _startTimer;
    private Position2D _startPosition;
    private Player _player;

    public override void _Ready()
    {
        _ui = GetNode<UI>("UI");
        _mobTimer = GetNode<Timer>("MobTimer");
        _scoreTimer = GetNode<Timer>("ScoreTimer");
        _startTimer = GetNode<Timer>("StartTimer");
        _startPosition = GetNode<Position2D>("StartPosition");
        _player = GetNode<Player>("Player");

        _startTimer.Connect("timeout", this, nameof(OnStartTimerTimeout));
        _scoreTimer.Connect("timeout", this, nameof(OnScoreTimerTimeout));
        _mobTimer.Connect("timeout", this, nameof(OnMobTimerTimeout));
        _player.Connect("Hit", this, nameof(GameOver));
        _ui.Connect("StartGame", this, nameof(NewGame));
    }

    public void SetScore(int score)
    {
        _score = score;
        _ui.UpdateScore(score);
    }

    public void GameOver()
    {
        _mobTimer.Stop();
        _scoreTimer.Stop();

        _ui.ShowGameOver();

        GetTree().CallGroup("mobs", "queue_free");
    }

    public void NewGame()
    {
        SetScore(0);

        _ui.ShowMessage("Get Ready!");

        _player.Start(_startPosition.Position);

        _startTimer.Start();
    }

    public void OnStartTimerTimeout()
    {
        _mobTimer.Start();
        _scoreTimer.Start();
    }

    public void OnScoreTimerTimeout()
    {
        SetScore(_score + 1);
    }

    public void OnMobTimerTimeout()
    {
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = _random.Next();

        var mobInstance = Mob.Instance<Mob>();
        AddChild(mobInstance);

        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
        mobInstance.Position = mobSpawnLocation.Position;

        direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mobInstance.Rotation = direction;

        var minSpeed = mobInstance.MinSpeed;
        var maxSpeed = mobInstance.MaxSpeed;
        mobInstance.LinearVelocity = new Vector2(RandRange(minSpeed, maxSpeed), 0).Rotated(direction);
    }

    private float RandRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }
    public override void _Process(float delta)
    {
        
    }
}
