
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

namespace Battle
{
    public class ModularSpell : MonoBehaviour, ISpell
    {
        [SerializeField]
        private string spellName;
        [SerializeField]
        private IAreaSelector targetArea;
        [SerializeField]
        private IAreaSelector spellEffectArea;
        [SerializeField]
        private ISpellEffect spellEffect;
        [SerializeField]
        private IResourceConsumer resourceConsumer;
        private Character caster;

        public string SpellName => spellName;

        public Task ApplyEffect(Character caster, MapCell target)
        {
            var ctx = MakeContext();

            var realEffectArea = spellEffectArea.GetRealArea(ctx.SetTargetCell(target));
            resourceConsumer.Consume(ctx);
            spellEffect.ApplyEffect(ctx, realEffectArea);

            return Task.CompletedTask;
        }

        private SpellComponentContext MakeContext()
        {
            return default;
            // return new SpellComponentContext(caster);
        }

        public bool CastAvailable(Character caster, MapCell target)
        {
            var ctx = MakeContext();
            var realTargetArea = targetArea.GetRealArea(ctx);
            if (!realTargetArea.Contains(target)) return false;
            var realEffectArea = spellEffectArea.GetRealArea(ctx.SetTargetCell(target));
            if (realEffectArea.Count == 0) return false;
            if (!resourceConsumer.ConsumeAvailable(ctx)) return false;
            if (!spellEffect.EffectAvailable(ctx, realEffectArea)) return false;

            return true;
        }

        public bool CastAvailable(Character caster)
        {
            var ctx = MakeContext();
            var realTargetArea = targetArea.GetRealArea(ctx);
            if (realTargetArea.Count == 0) return false;
            if (!resourceConsumer.ConsumeAvailable(ctx)) return false;

            return true;
        }

        public List<MapCell> GetEffectArea(Character caster, MapCell target)
        {
            var ctx = MakeContext();
            return spellEffectArea.GetRealArea(ctx.SetTargetCell(target));
        }

        public List<MapCell> GetTargetArea(Character caster)
        {
            var ctx = MakeContext();
            return targetArea.GetRealArea(ctx);
        }

        public string GetDescription(Character caster)
        {
            return string.Join("\n", $@"
                [b]Name[/b]
                {this.spellName}
                [b]Resource Require[/b]
                {resourceConsumer.GetDescription(caster)} 
                [b]Target Area[/b]
                {targetArea.GetDescription(caster)}
                [b]Effect Area[/b]
                {spellEffectArea.GetDescription(caster)}
                [b]Effect[/b]
                {spellEffect.GetDescription(caster)}
            ".Trim().Split('\n').Select(line => line.Trim()));
        }
    }
}