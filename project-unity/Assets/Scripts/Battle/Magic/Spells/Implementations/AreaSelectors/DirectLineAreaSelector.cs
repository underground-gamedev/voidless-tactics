using System.Collections.Generic;
using System.Linq;
using Battle.Algorithms.AreaPatterns;
using UnityEngine;

namespace Battle
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