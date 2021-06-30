using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MagicShotSpell: Node, ISpell
{
    [Export]
    private int range;
    [Export]
    private int damage;
    [Export]
    private float cost;

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
        return new List<MapCell>() { target };
    }

    public bool CastAvailable(MapCell target)
    {
        var targetCharacter = target.MapObject as Character;
        if (targetCharacter is null) return false;
        if (targetCharacter.Controller == character.Controller) return false;
        return GetTargetArea().Contains(target);
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
}