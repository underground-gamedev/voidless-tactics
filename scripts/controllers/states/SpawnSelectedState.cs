using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpawnSelectedState: BaseControllerState
{
    protected Character active;
    private List<MapCell> spawnArea;

    public SpawnSelectedState(Character character, List<MapCell> spawnArea)
    {
        this.active = character;
        this.spawnArea = spawnArea;
    }

    protected override Task<BaseControllerState> GenericCellClick(MapCell cell)
    {
        var targetCharacter = cell.MapObject as Character;
        if (targetCharacter == null) 
        {
            return EmptyCellClick(cell.X, cell.Y);
        }
        else
        {
            return CharacterClick(targetCharacter);
        }
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
    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        if (character == active) return Async(this);

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new SpawnSelectedState(character, spawnArea));
        }
        return NextState(new SpawnUnselectedState(spawnArea));
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        var targetCell = map.CellBy(x, y);
        if (spawnArea.Contains(targetCell))
        {
            active.SetCell(targetCell);
            active.SyncWithMap(map.VisualLayer.TileMap);
        }

        return NextState(new SpawnUnselectedState(spawnArea));
    }

    public override void OnEnter()
    {
        map.MoveHighlightLayer.Clear();

        var highlight = map.MoveHighlightLayer;
        spawnArea.ForEach(cell => highlight.Highlight(cell.X, cell.Y, MoveHighlightType.LongMove));
        map.MoveHighlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);
    }

    public override void OnLeave()
    {
        map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideMenuWithActions();
    }
}