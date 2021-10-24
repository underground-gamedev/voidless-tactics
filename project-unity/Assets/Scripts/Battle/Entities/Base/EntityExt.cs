using Core.Components;

namespace Battle
{
    public static class EntityExt
    {
        public static T GetComponent<T>(this IEntity ent) where T : class
        {
            return (T)ent.GetComponent(typeof(T));
        }

        public static void AddWithAssociation<T>(this IEntity ent, IComponent com) where T : class
        {
            ent.AddComponent(com);
            if (typeof(T) != com.GetType())
            {
                ent.AssociateComponent(typeof(T), com.GetType());
            }
        }

        public static void RemoveComponent<T>(this IEntity ent) where T : class
        {
            ent.RemoveComponent((IComponent)ent.GetComponent<T>());
        }

        public static void RemoveComponent(this IEntity ent, IComponent com)
        {
            ent.RemoveComponent(com);
        }

        public static bool HasComponent<T>(this IEntity ent) where T : class
        {
            return ent.GetComponent(typeof(T)) != null;
        }

        public static void Associate<TAssociation, TComponent>(this IEntity ent) where TComponent : class, IComponent
        {
            ent.AssociateComponent(typeof(TAssociation), typeof(TComponent));
        }

        public static void RemoveAssociation<TAssociation>(this IEntity ent)
        {
            ent.RemoveAssociation(typeof(TAssociation));
        }
    }
}