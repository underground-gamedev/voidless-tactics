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

    protected override void CharacterHover(Character character)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(character);
        hud?.DisplayCellInfo(character.Cell);
    }

    protected override void EmptyCellHover(MapCell cell)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.DisplayCellInfo(cell);
    }
    
    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideCellInfo();
    }
}