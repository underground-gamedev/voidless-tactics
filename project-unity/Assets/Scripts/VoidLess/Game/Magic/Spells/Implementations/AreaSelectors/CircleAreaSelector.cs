using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Magic.Spells.Base;
using VoidLess.Game.Map.Algorithms.AreaPatterns;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Implementations.AreaSelectors
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

        public string GetDescription(IEntity caster)
        {
            return $"circle: {range}";
        }
    }
}