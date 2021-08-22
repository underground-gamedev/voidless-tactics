using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class HealSpellEffect : ScriptableObject, ISpellEffect
    {
        [SerializeField]
        private int heal;

        private CharacterStat GetPowerStat(Character caster)
        {
            return caster.BasicStats.Magic;
        }

        public void ApplyEffect(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            foreach (var cell in effectArea)
            {
                var targetCharacter = cell?.MapObject?.AsCharacter;
                if (targetCharacter is null) continue;

                var targetComponent = targetCharacter.TargetComponent;
                if (targetComponent is null) continue;

                targetComponent.TakeHeal(heal + GetPowerStat(ctx.Caster).Value);
            }
        }

        public bool EffectAvailable(SpellComponentContext ctx, List<MapCell> effectArea)
        {
            foreach (var effectTarget in effectArea)
            {
                var targetCharacter = effectTarget.MapObject.AsCharacter;
                if (targetCharacter is null) continue;
                if (ctx.Caster.Team.IsEnemy(targetCharacter)) continue;
                return true;
            }

            return false;
        }

        public string GetDescription(Character caster)
        {
            var powerStat = GetPowerStat(caster);
            return $"heal: {heal}+{powerStat.Value}";
        }
    }
}