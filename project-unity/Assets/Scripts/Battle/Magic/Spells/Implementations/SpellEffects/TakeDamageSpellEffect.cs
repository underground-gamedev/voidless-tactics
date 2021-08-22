using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class TakeDamageSpellEffect : ScriptableObject, ISpellEffect
    {
        [SerializeField]
        private int damage;

        private CharacterStat GetPowerStat(Character caster)
        {
            return caster.BasicStats.Magic;
        }

        public void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            foreach (var cell in effectArea)
            {
                var targetCharacter = cell.MapObject.AsCharacter;
                if (targetCharacter is null) continue;

                var targetComponent = targetCharacter.TargetComponent;
                if (targetComponent is null) continue;

                targetComponent.TakeDamage(damage + GetPowerStat(ctx.Caster).Value);
            }
        }

        public bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            foreach (var effectTarget in effectArea)
            {
                var targetCharacter = effectTarget.MapObject.AsCharacter;
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