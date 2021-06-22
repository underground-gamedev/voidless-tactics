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

    protected override Task<BaseControllerState> CharacterHover(Character character)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(character);
        hud?.DisplayCellInfo(character.Cell);
        return Async(this);
    }

    protected override Task<BaseControllerState> EmptyCellHover(MapCell cell)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.DisplayCellInfo(cell);
        return Async(this);
    }
    
    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideCellInfo();
    }
}