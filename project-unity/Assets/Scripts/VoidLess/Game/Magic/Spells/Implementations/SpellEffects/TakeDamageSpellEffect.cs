using System.Collections.Generic;
using UnityEngine;

/*
namespace Battle
{
    public class TakeDamageSpellEffect : ScriptableObject, ISpellEffect
    {
        [SerializeField]
        private int damage;

        private EntityStat GetPowerStat(IEntity caster)
        {
            return caster.BasicStats.Magic;
        }

        public void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            var map = ctx.Map;
            var charLayer = map.GetLayer<IEntityMapLayer>();
            foreach (var cell in effectArea)
            {
                var targetCharacter = charLayer.GetCharacter(cell);
                if (targetCharacter is null) continue;

                var targetComponent = targetCharacter.TargetComponent;
                if (targetComponent is null) continue;

                targetComponent.TakeDamage(damage + GetPowerStat(ctx.Caster).Value);
            }
        }

        public bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            var map = ctx.Map;
            var charLayer = map.GetLayer<IEntityMapLayer>();
            foreach (var effectTarget in effectArea)
            {
                var targetCharacter = charLayer.GetCharacter(effectTarget);
                if (targetCharacter is null) continue;
                if (ctx.Caster.Team.IsAlly(targetCharacter)) continue;
                return true;
            }

            return false;
        }

        public string GetDescription(IEntity caster)
        {
            var powerStat = GetPowerStat(caster);
            return $"take damage: {damage}+{powerStat.Value}";
        }
    }
}
*/