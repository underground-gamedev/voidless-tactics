public class CharacterSelectTransition: BaseControllerState
{
    private Character character;

    public CharacterSelectTransition(Character character)
    {
        this.character = character;
    }

    public override void OnEnter()
    {
        BaseControllerState selectState = new InteractableSelectState(character);
        if (character.Controller != controller)
        {
            selectState = new NonInteractableSelectState(character); 
        }
        controller.MainStates.ReplaceState(selectState);
    }
}