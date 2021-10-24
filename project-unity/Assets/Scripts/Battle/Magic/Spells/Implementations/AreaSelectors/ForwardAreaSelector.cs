using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Algorithms.AreaPatterns;
using UnityEngine;

namespace Battle
{
    public abstract class ForwardAreaSelector : ScriptableObject, IAreaSelector
    {
        [SerializeField]
        protected int range;
        [SerializeField]
        protected bool interruptOnCharacter;
        [SerializeField]
        protected bool excludeBasePosition;

        protected abstract List<MapCell> GetDirections();

        private IAreaPattern fullAreaPattern;
        private IAreaPattern realAreaPattern;

        private void Awake()
        {
            fullAreaPattern = new DirectionalPattern(GetDirections(), range)
                .InterruptOnCharacter(interruptOnCharacter)
                .InterruptOnCharacter(false)
                .ExcludeBasePositions(excludeBasePosition) ;
            realAreaPattern = new DirectionalPattern(GetDirections(), range)
                .InterruptOnCharacter(interruptOnCharacter)
                .InterruptOnCharacter(true)
                .ExcludeBasePositions(excludeBasePosition);
        }

        public List<MapCell> GetFullArea(SpellComponentContext ctx)
        {
            return fullAreaPattern.GetPattern(ctx.Map, ctx.TargetCell).ToList();
        }

        public List<MapCell> GetRealArea(SpellComponentContext ctx)
        {
            return realAreaPattern.GetPattern(ctx.Map, ctx.TargetCell).ToList();
        }

        public abstract string GetDescription(IEntity caster);
    }
}