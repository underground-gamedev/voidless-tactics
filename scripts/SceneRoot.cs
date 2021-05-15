using Godot;
using System;

public class SceneRoot : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int acc = 0;
    private Label outLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        outLabel = GetNode<Label>("Label");

        GetNode<Button>("Button").Connect("pressed", this, nameof(Accumulate));
        GetNode<Timer>("IdleCounter").Connect("timeout", this, nameof(Accumulate));
        this.Connect("on_accumulate", this, nameof(DebugFoo));
    }

    private void DebugFoo() 
    {
        GD.Print("From Foo");
    }

    private void Accumulate() 
    {
        acc++;
        outLabel.Text = acc.ToString();
        EmitSignal(nameof(on_accumulate));
    }

    [Signal]
    public delegate void on_accumulate();

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
