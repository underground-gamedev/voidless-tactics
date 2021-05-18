using Godot;
using System;

public class UI : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    private Label _messageLabel;
    private Timer _messageTimer;
    private Label _scoreLabel;
    private Button _startButton;
    
    public override void _Ready()
    {
        _messageLabel = GetNode<Label>("Message");
        _messageTimer = GetNode<Timer>("MessageTimer");
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _startButton = GetNode<Button>("StartButton");

        _messageTimer.Connect("timeout", this, nameof(OnMessageTimerTimeout));
        _startButton.Connect("pressed", this, nameof(OnStartButtonPressed));
    }

    public void SetMessage(String text)
    {
        var message = _messageLabel;
        message.Text = text;
        message.Show();
    }

    public void ShowMessage(string text)
    {
        SetMessage(text);
        _messageTimer.Start();
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");
        await ToSignal(_messageTimer, "timeout");
        SetMessage("Dodge the\nCreeps!");
        // await ToSignal(GetTree().CreateTimer(1), "timeout");
        _startButton.Show();
    }

    public void UpdateScore(int score)
    {
        _scoreLabel.Text = score.ToString();
    }

    public void OnStartButtonPressed()
    {
        _startButton.Hide();
        EmitSignal(nameof(StartGame));
    }

    public void OnMessageTimerTimeout()
    {
        _messageLabel.Hide();
    }
}
