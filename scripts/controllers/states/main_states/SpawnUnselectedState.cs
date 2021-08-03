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

    public override bool CellClick(int x, int y)
    {
        return this.CharacterByPos(x, y, (character) => {
            var fromMyTeam = controller.Characters.Contains(character);
            if (!fromMyTeam) return false;
            controller.MainStates.ReplaceState(new SpawnSelectedState(character, spawnArea));
            return true;
        });
    }

    public override bool DragStart(int x, int y)
    {
        this.CharacterByPos(x, y, (character) => {
            controller.MainStates.ReplaceState(new SpawnSelectedState(character, spawnArea));
            return false;
        });

        return false;
    }

    public override void OnEnter()
    {
        var map = controller.Map;
        var highlight = map.MoveHighlightLayer;
        highlight.Clear();
        spawnArea.ForEach(cell => highlight.Highlight(cell.X, cell.Y, MoveHighlightType.LongMove));
    }
    
    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideHoverCharacter();
        hud?.HideCellInfo();

        var map = controller.Map;
        map.MoveHighlightLayer.Clear();
    }
}