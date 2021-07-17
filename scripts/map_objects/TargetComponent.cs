using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TargetComponent : Node, ITargetComponent
{
    private Character character;
    private BasicStats basicStats;

    public async Task TakeDamage(int damage)
    {
        var health = basicStats.Health;
        health.ActualValue -= damage;

        if (health.ActualValue <= 0)
        {
            character.Kill();
        }
        await character.Map.PopupText(character.Cell, damage.ToString(), new Color(1f, 0.6f, 0.6f));
    }

    public override void _Ready()
    {
        character = this.FindParent<Character>();
        basicStats = character.BasicStats;
    }
}