using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Magic.Spells.Base;
using VoidLess.Game.Map.Algorithms.AreaPatterns;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Implementations.AreaSelectors
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