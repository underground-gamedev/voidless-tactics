using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TargetComponent : Node, ITargetComponent
{
    private Character character;
    private BasicStats basicStats;

    public Task TakeDamage(int damage)
    {
        var health = basicStats.Health;
        health.ActualValue -= damage;

        if (health.ActualValue <= 0)
        {
            character.Kill();
        }

        return Task.CompletedTask;
    }

    public override void _Ready()
    {
        character = this.FindParent<Character>();
        basicStats = character.BasicStats;
    }
}