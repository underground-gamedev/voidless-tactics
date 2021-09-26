using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.Utilities;

namespace Core.Components
{
    public class ComponentContainer<TComponent>
        where TComponent : class
    {
        private Dictionary<Type, TComponent> associations = new Dictionary<Type, TComponent>();
        private HashSet<TComponent> components = new HashSet<TComponent>();
        
        public IReadOnlyList<TComponent> Components => components.ToList();
        
        private Action<TComponent> onComAttached;
        private Action<TComponent> onComDeAttached;

        public ComponentContainer(Action<TComponent> onNewComponentAttached, Action<TComponent> onFullComponentDeAttached)
        {
            onComAttached = onNewComponentAttached;
            onComDeAttached = onFullComponentDeAttached;
        }

        public TAssociated Get<TAssociated>() where TAssociated : class, TComponent
        {
            return Get(typeof(TAssociated)) as TAssociated;
        }

        public TComponent Get(Type tAssociated)
        {
            if (associations.TryGetValue(tAssociated, out var com))
            {
                return com;
            }
            return null;
        }

        public void Attach(Type tAssociated, [NotNull] TComponent com)
        {
            if (!tAssociated.IsInstanceOfType(com))
            {
                throw new InvalidOperationException($"Component ({com.GetType().Name}) not instance of {tAssociated.Name}");
            }
            
            if (associations.ContainsKey(tAssociated))
            {
                throw new InvalidOperationException($"Association with type {tAssociated.Name} already exists");
            }
            
            var realType = com.GetType();
            
            #if DEBUG || CHECK_COM_REQUIREMENTS
            var attrs = TypeDescriptor.GetAttributes(realType);
            var requireAttr = (RequireAttribute) attrs[typeof(RequireAttribute)];
            if (requireAttr != null)
            {
                var notResolvedRequirements = requireAttr.Requirements.Where(req => !associations.ContainsKey(req)).ToList();
                if (notResolvedRequirements.Any())
                {
                    var notResolvedNames = string.Join(", ", notResolvedRequirements.Select(req => req.Name));
                    throw new InvalidDataException($"Not all requirements for {realType.Name} resolved. Missed {notResolvedNames} types");
                }
            }
            #endif

            if (!components.Contains(com))
            {
                onComAttached?.Invoke(com);
                components.Add(com);
            }
            
            associations.Add(tAssociated, com);
        }

        public void Attach<TAssociated>([NotNull] TAssociated com) where TAssociated : class, TComponent
        {
            var tAssociated = typeof(TAssociated);
            Attach(tAssociated, com);
        }

        public void DeAttach(Type tAssociated)
        {
            if (!associations.TryGetValue(tAssociated, out var com))
            {
                return;
            }
            
            associations.Remove(tAssociated);
            
            if (!associations.ContainsValue(com))
            {
                components.Remove(com);
                onComDeAttached?.Invoke(com);
            }
        }

        public void DeAttach<TAssotiated>() where TAssotiated : class, TComponent
        {
            DeAttach(typeof(TAssotiated));
        }

        public void DeAttachAll()
        {
            associations.Clear();
            foreach (var component in components)
            {
                onComDeAttached?.Invoke(component);
            }
            components.Clear();
        }
    }
}