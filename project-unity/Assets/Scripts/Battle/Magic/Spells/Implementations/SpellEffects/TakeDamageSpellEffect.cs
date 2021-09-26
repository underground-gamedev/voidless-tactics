using System.Collections.Generic;
using Battle.Map.Interfaces;
using UnityEngine;

/*
namespace Battle
{
    public class TakeDamageSpellEffect : ScriptableObject, ISpellEffect
    {
        [SerializeField]
        private int damage;

        private EntityStat GetPowerStat(Character caster)
        {
            return caster.BasicStats.Magic;
        }

        public void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            var map = ctx.Map;
            var charLayer = map.GetLayer<ICharacterMapLayer>();
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
            var charLayer = map.GetLayer<ICharacterMapLayer>();
            foreach (var effectTarget in effectArea)
            {
                var targetCharacter = charLayer.GetCharacter(effectTarget);
                if (targetCharacter is null) continue;
                if (ctx.Caster.Team.IsAlly(targetCharacter)) continue;
                return true;
            }

            return false;
        }

        public string GetDescription(Character caster)
        {
            var powerStat = GetPowerStat(caster);
            return $"take damage: {damage}+{powerStat.Value}";
        }
    }
}
*/