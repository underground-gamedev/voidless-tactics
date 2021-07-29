
using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

public class ModularSpell : Node, ISpell
{
    [Export]
    private NodePath targetAreaPath;
    [Export]
    private NodePath effectAreaPath;
    [Export]
    private NodePath spellEffectPath;
    [Export]
    private NodePath resourceConsumerPath;

    private IAreaSelector targetArea;
    private IAreaSelector spellEffectArea;
    private ISpellEffect spellEffect;
    private IResourceConsumer resourceConsumer;
    private Character caster;

    public override void _Ready()
    {
        targetArea = GetNode<IAreaSelector>(targetAreaPath);
        spellEffectArea = GetNode<IAreaSelector>(effectAreaPath);
        spellEffect = GetNode<ISpellEffect>(spellEffectPath);
        resourceConsumer = GetNode<IResourceConsumer>(resourceConsumerPath);
        caster = this.FindParent<Character>();
    }

    public Task ApplyEffect(MapCell target)
    {
        var ctx = MakeContext();

        var realEffectArea = spellEffectArea.GetRealArea(ctx.SetTargetCell(target));
        var consumeTags = resourceConsumer.GetConsumeTags(ctx);
        resourceConsumer.Consume(ctx);
        spellEffect.ApplyEffect(ctx, realEffectArea, consumeTags);

        return Task.CompletedTask;
    }

    private SpellComponentContext MakeContext()
    {
        return new SpellComponentContext(caster);
    }

    public bool CastAvailable(MapCell target)
    {
        var ctx = MakeContext();
        var realTargetArea = targetArea.GetRealArea(ctx);
        if (!realTargetArea.Contains(target)) return false;
        var realEffectArea = spellEffectArea.GetRealArea(ctx.SetTargetCell(target));
        if (realEffectArea.Count == 0) return false;
        if (!resourceConsumer.ConsumeAvailable(ctx)) return false;
        var consumeTags = resourceConsumer.GetConsumeTags(ctx);
        if (!spellEffect.EffectAvailable(ctx, realEffectArea, consumeTags)) return false;

        return true;
    }

    public bool CastAvailable()
    {
        var ctx = MakeContext();
        var realTargetArea = targetArea.GetRealArea(ctx);
        if (realTargetArea.Count == 0) return false;
        if (!resourceConsumer.ConsumeAvailable(ctx)) return false;

        return true;
    }

    public List<MapCell> GetEffectArea(MapCell target)
    {
        var ctx = MakeContext();
        return spellEffectArea.GetRealArea(ctx.SetTargetCell(target));
    }

    public List<MapCell> GetTargetArea()
    {
        var ctx = MakeContext();
        return targetArea.GetRealArea(ctx);
    }

    public string GetDescription()
    {
        return String.Join("\n", $@"
            Name
            {this.Name}

            Resource Require
            {resourceConsumer.GetDescription(caster)} 

            Target Area
            {targetArea.GetDescription(caster)}

            Effect Area
            {spellEffectArea.GetDescription(caster)}

            Effect
            {spellEffect.GetDescription(caster)}
        ".Trim().Split("\n").Select(line => line.Trim()));
    }
}