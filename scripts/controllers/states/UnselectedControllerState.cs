using System.Linq;

public class UnselectedControllerState: BaseControllerState
{
    public UnselectedControllerState()
    {
    }

    public UnselectedControllerState(AbstractController controller, TacticMap map)
    {
        this.Init(controller, map);
    }

    protected override BaseControllerState CharacterClick(Character character)
    {
        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }

        return NextState(new EnemySelectedState(character));
    }
}