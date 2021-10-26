using System;
using System.Collections.Generic;
using System.Linq;
using VoidLess.Core.Components.StatComponent;

namespace VoidLess.Core.Entities
{
    public static class Archtype
    {
        public static readonly IArchtype StatHolder = New()
            .With<IStatComponent>()
            .Build();
            
        public static readonly IArchtype Character = New()
            .Derive(StatHolder)
            .Build();

        
        private sealed class BuildedArchtype : IArchtype
        {
            public IReadOnlyList<Type> Components { get; }
            
            internal BuildedArchtype(List<Type> components)
            {
                Components = components;
            }
        }
        public sealed class Builder
        {
            private List<Type> components;
            internal Builder()
            {
                components = new List<Type>();
            }

            public Builder With<T>()
            {
                components.Add(typeof(T));
                return this;
            }

            public Builder Derive(IArchtype arch)
            {
                foreach (var archComponent in arch.Components)
                {
                    if (components.Contains(archComponent))
                    {
                        components.Add(archComponent);
                    }
                }

                return this;
            }

            public IArchtype Build()
            {
                return new BuildedArchtype(components.ToList());
            }
        }

        public static Builder New()
        {
            return new Builder();
        }
    }
}