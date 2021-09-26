namespace Battle
{
    public abstract class EntityStatModifier
    {
        public abstract int ModifyValue(int baseValue, int modifiedCurrent);
        public virtual int ModifyPriority => 0;
        public virtual EntityStatModifier StackWith(EntityStatModifier statModifier) { return statModifier; }
    }
}