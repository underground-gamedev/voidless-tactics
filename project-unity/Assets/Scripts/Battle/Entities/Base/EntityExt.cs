using Core.Components;

namespace Battle
{
    public static class EntityExt
    {
        public static T GetComponent<T>(this IEntity ent) where T : class
        {
            return ent.GetComponent(typeof(T)) as T;
        }

        public static void AddComponent<T>(this IEntity ent, T com) where T : class, IComponent
        {
            ent.AddComponent(com);
            if (typeof(T) != com.GetType())
            {
                ent.AssociateComponent(typeof(T), com.GetType());
            }
        }

        public static void RemoveComponent<T>(this IEntity ent) where T : class
        {
            ent.RemoveComponent(typeof(T));
        }

        public static bool HasComponent<T>(this IEntity ent) where T : class
        {
            return ent.GetComponent<T>() != null;
        }

        public static void Associate<TAssociation, TComponent>(this IEntity ent) where TComponent : class, IComponent
        {
            ent.AssociateComponent(typeof(TAssociation), typeof(TComponent));
        }
    }
}