using Godot;
using System;

public class MainMenu : Node
{
    public override void _Ready()
    {
        GetNode<AudioStreamPlayer>("BackgroundMusic").Play();
        GetNode<CPUParticles2D>("TitleName/TitleEffect").Restart();
        GetNode<Button>("Menu/NewGameButton").Connect("pressed", this, nameof(OnNewGamePressed));
        GetNode<Button>("Menu/ExitButton").Connect("pressed", this, nameof(OnExitPressed));
    }

    public void OnNewGamePressed()
    {
        GetTree().ChangeScene("res://scenes/TacticBattle.tscn");
    }

    public void OnExitPressed()
    {
        GetTree().Quit();
    }
}
