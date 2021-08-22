namespace Battle
{
    public abstract class CharacterStatModifier
    {
        public abstract int ModifyValue(int baseValue, int modifiedCurrent);
        public virtual int ModifyPriority => 0;
        public virtual CharacterStatModifier StackWith(CharacterStatModifier statModifier) { return statModifier; }
    }
}