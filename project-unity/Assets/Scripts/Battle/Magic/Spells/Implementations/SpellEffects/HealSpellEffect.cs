using System.Collections.Generic;
using Battle.Map.Interfaces;
using UnityEngine;

/*
namespace Battle
{
    public class HealSpellEffect : ScriptableObject, ISpellEffect
    {
        [SerializeField]
        private int heal;

        private EntityStat GetPowerStat(IEntity caster)
        {
            return caster.BasicStats.Magic;
        }

        public void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            var map = ctx.Map;
            var charLayer = map.GetLayer<IEntityMapLayer>();
            foreach (var effectCell in effectArea)
            {
                var targetCharacter = charLayer.GetCharacter(effectCell);
                if (targetCharacter is null) continue;

                var targetComponent = targetCharacter.TargetComponent;
                if (targetComponent is null) continue;

                targetComponent.TakeHeal(heal + GetPowerStat(ctx.Caster).Value);
            }
        }

        public bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            var map = ctx.Map;
            var charLayer = map.GetLayer<IEntityMapLayer>();
            foreach (var effectCell in effectArea)
            {
                var targetCharacter = charLayer.GetCharacter(effectCell);
                if (targetCharacter is null) continue;
                if (ctx.Caster.Team.IsEnemy(targetCharacter)) continue;
                return true;
            }

            return false;
        }

        public string GetDescription(IEntity caster)
        {
            var powerStat = GetPowerStat(caster);
            return $"heal: {heal}+{powerStat.Value}";
        }
    }
}
*/