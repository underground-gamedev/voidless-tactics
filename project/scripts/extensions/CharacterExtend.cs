using Godot;

public static class CharacterExtend
{
    public static ITargetComponent GetTargetComponent(this Character character)
    {
        return character.Components.GetComponent<ITargetComponent>();
    }

    public static IMoveComponent GetMoveComponent(this Character character)
    {
        return character.Components.GetComponent<IMoveComponent>();
    }

    public static IAttackComponent GetAttackComponent(this Character character)
    {
        return character.Components.GetComponent<IAttackComponent>();
    }

    public static ISpellComponent GetSpellComponent(this Character character)
    {
        return character.Components.GetComponent<ISpellComponent>();
    }

    public static IWaitComponent GetWaitComponent(this Character character)
    {
        return character.Components.GetComponent<IWaitComponent>();
    }

    public static PortraitComponent GetPortraitComponent(this Character character)
    {
        return character.Components.GetComponent<PortraitComponent>();
    }

    public static TurnOrderComponent GetTurnOrderComponent(this Character character)
    {
        return character.Components.GetComponent<TurnOrderComponent>();
    }

    public static AIComponent GetAIComponent(this Character character)
    {
        return character.Components.GetComponent<AIComponent>();
    }
}