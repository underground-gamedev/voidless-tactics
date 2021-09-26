using Core.Components;

namespace Battle
{
    public static class EntityExt
    {
        public static T GetComponent<T>(this IEntity ent) where T : class, IComponent
        {
            return ent.GetComponent(typeof(T)) as T;
        }

        public static void AddComponent<T>(this IEntity ent, T com) where T : class, IComponent
        {
            ent.AddComponent(typeof(T), com);
        }

        public static void RemoveComponent<T>(this IEntity ent) where T : class, IComponent
        {
            ent.RemoveComponent(typeof(T));
        }

        public static bool HasComponent<T>(this IEntity ent) where T : class, IComponent
        {
            return ent.GetComponent<T>() != null;
        }
    }
}