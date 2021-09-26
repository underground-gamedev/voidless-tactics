using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Algorithms.AreaPatterns;
using UnityEngine;

namespace Battle
{
    public class SquareAreaSelector: MonoBehaviour, IAreaSelector
    {
        [SerializeField]
        private int range;

        private IAreaPattern areaPattern;

        private void Awake()
        {
            areaPattern = new SquarePattern(range);
        }

        public List<MapCell> GetFullArea(SpellComponentContext ctx)
        {
            return areaPattern.GetPattern(ctx.Map, ctx.TargetCell).ToList();
        }

        public List<MapCell> GetRealArea(SpellComponentContext ctx)
        {
            return GetFullArea(ctx);
        }

        public string GetDescription(Character caster)
        {
            return $"square: {range}";
        }
    }
}