using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Algorithms.AreaPatterns;
using UnityEditor;
using UnityEngine;

namespace Battle
{
    public class CircleAreaSelector : ScriptableObject, IAreaSelector
    {
        [SerializeField]
        private int range;

        private IAreaPattern pattern;
        
        private void Awake()
        {
            pattern = new CirclePattern(range);
        }

        public List<MapCell> GetFullArea(SpellComponentContext ctx)
        {
            return pattern.GetPattern(ctx.Map, ctx.TargetCell).ToList();
        }

        public List<MapCell> GetRealArea(SpellComponentContext ctx)
        {
            return GetFullArea(ctx);
        }

        public string GetDescription(Character caster)
        {
            return $"circle: {range}";
        }
    }
}