using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

public class MagicShotSpell: Node, ISpell
{
    [Export]
    private int range;
    [Export]
    private int damage;
    [Export]
    private int cost;

    Character character;

    public override void _Ready()
    {
        character = this.FindParent<Character>();
    }

    public Task ApplyEffect(MapCell target)
    {
        var area = GetEffectArea(target);
        foreach (var cell in area)
        {
            var targetCharacter = cell.MapObject as Character;
            if (targetCharacter is null) continue;

            var targetComponent = targetCharacter.Components.FindChild<ITargetComponent>();
            if (targetComponent is null) continue;

            targetComponent.TakeDamage(damage);
        }

        character.BasicStats.FullActions.ActualValue -= 1;
        character.Cell.Mana.Density -= cost;
        character.Map.Sync();

        return Task.CompletedTask;
    }

    public List<MapCell> GetEffectArea(MapCell target)
    {
        return new List<MapCell>() { target }.Concat(character.Map.DirectNeighboursFor(target.X, target.Y)).ToList();
    }

    public bool CastAvailable(MapCell target)
    {
        if (!GetTargetArea().Contains(target)) return false;

        foreach (var effectTarget in GetEffectArea(target))
        {
            var targetCharacter = effectTarget.MapObject as Character;
            if (targetCharacter is null) continue;
            if (targetCharacter.Controller == character.Controller) continue;
            return true;
        }

        return false;
    }

    public List<MapCell> GetTargetArea()
    {
        var map = character.Map;
        var area = new List<MapCell>();
        var directions = new List<(int, int)>() {
            (0, -1), (-1, 0), (1, 0), (0, 1),
        };

        var (charX, charY) = character.Cell.Position;

        for (var range = 1; range <= this.range; range++)
        {
            foreach (var dir in directions)
            {
                var (offsetX, offsetY) = dir;
                var targetX = charX + offsetX * range;
                var targetY = charY + offsetY * range;
                if (map.IsOutOfBounds(targetX, targetY)) continue;
                area.Add(map.CellBy(targetX, targetY));
            }
        }

        return area;
    }

    public bool CastAvailable()
    {
        var mana = character.Cell.Mana;
        if (mana.ManaType == ManaType.None) return false;
        if (mana.Density < cost)  return false;

        return character.BasicStats.FullActions.ActualValue > 0;
    }

    public string GetDescription()
    {
        return $"Spell: {Name}\n" +
               $"Damage: {damage}\n" +
               $"Range: {range}\n" +
               $"Cost: {cost}";
    }
}