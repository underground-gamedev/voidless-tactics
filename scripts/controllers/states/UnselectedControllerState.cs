using System.Linq;
using System.Threading.Tasks;

public class UnselectedControllerState: BaseControllerState
{
    public UnselectedControllerState()
    {
    }

    public UnselectedControllerState(AbstractController controller, TacticMap map)
    {
        this.Init(controller, map);
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }

        return NextState(new EnemySelectedState(character));
    }
}