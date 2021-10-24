namespace Battle
{
    public class MoveToGameEvent : IGlobalEvent
    {
        public readonly IEntity Entity;
        public readonly MapCell Target;

        public MoveToGameEvent(IEntity entity, MapCell target)
        {
            Entity = entity;
            Target = target;
        }

        public void Deconstruct(out IEntity entity, out MapCell cell)
        {
            entity = Entity;
            cell = Target;
        }
    }
}