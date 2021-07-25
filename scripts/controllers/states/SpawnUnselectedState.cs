using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class SpawnUnselectedState: BaseControllerState
{
    List<MapCell> spawnArea;
    public SpawnUnselectedState(List<MapCell> spawnArea)
    {
        this.spawnArea = spawnArea;
    }

    public SpawnUnselectedState(AbstractController controller, TacticMap map, List<MapCell> spawnArea)
    {
        this.Init(controller, map);
        this.spawnArea = spawnArea;
    }

    protected override Task<BaseControllerState> GenericCellClick(MapCell cell)
    {
        var targetCharacter = cell.MapObject as Character;
        if (targetCharacter == null) return Async(this);
        var fromMyTeam = controller.Characters.Contains(targetCharacter);
        if (fromMyTeam)
        {
            return NextState(new SpawnSelectedState(targetCharacter, spawnArea));
        }

        return Async(this);
    }

    protected override void GenericCellHover(MapCell cell)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCellInfo(cell);
        var targetCharacter = cell.MapObject as Character;
        if (targetCharacter != null)
        {
            hud?.DisplayCharacter(targetCharacter);
        }
        else
        {
            hud?.HideCharacterDisplay();
        }
    }
    
    public override void OnEnter()
    {
        var highlight = map.MoveHighlightLayer;
        highlight.Clear();
        spawnArea.ForEach(cell => highlight.Highlight(cell.X, cell.Y, MoveHighlightType.LongMove));
    }
    
    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideCellInfo();

        map.MoveHighlightLayer.Clear();
    }
}