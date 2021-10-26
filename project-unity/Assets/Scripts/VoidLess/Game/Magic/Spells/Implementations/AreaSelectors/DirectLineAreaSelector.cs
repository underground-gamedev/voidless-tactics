using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Magic.Spells.Base;
using VoidLess.Game.Map.Algorithms.AreaPatterns;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Implementations.AreaSelectors
{
    public class DirectLineAreaSelector : ScriptableObject, IAreaSelector
    {
        [SerializeField]
        private int range;

        private IRelativeAreaPattern areaPattern;

        private void Awake()
        {
            this.areaPattern = new RelativeLinePattern(range);
        }

        public List<MapCell> GetFullArea(SpellComponentContext ctx)
        {
            return areaPattern.GetPattern(ctx.Map, ctx.SourceCell, ctx.TargetCell).ToList();
        }

        public List<MapCell> GetRealArea(SpellComponentContext ctx)
        {
            return GetFullArea(ctx);
        }

        public string GetDescription(IEntity caster)
        {
            return $"relative line: {range}";
        }
    }
}