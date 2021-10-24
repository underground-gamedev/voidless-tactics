using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle
{
    public static class Archtype
    {
        public static readonly IArchtype Character = New()
                .With<IStatComponent>()
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