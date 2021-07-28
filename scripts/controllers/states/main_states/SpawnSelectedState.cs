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

    public override bool CellClick(int x, int y)
    {
        var charClick = this.CharacterByPos(x, y, (character) => {
            var fromMyTeam = controller.Characters.Contains(character);

            BaseControllerState nextState = new SpawnUnselectedState(spawnArea);
            if (fromMyTeam)
            {
                nextState = new SpawnSelectedState(character, spawnArea);
            }

            controller.MainStates.ReplaceState(nextState);
            return true;
        });

        if (charClick) return true;

        controller.MainStates.ReplaceState(new SpawnUnselectedState(spawnArea));
        return this.CellByPos(x, y, (cell) => {
            if (spawnArea.Contains(cell))
            {
                active.SetCell(cell);
                active.SyncWithMap(controller.Map.VisualLayer.TileMap);
            }

            return true;
        });
    }

    public override bool DragStart(int x, int y)
    {
        return this.CharacterByPos(x, y, (character) => {
            if (character.Controller != controller)
            {
                controller.MainStates.ReplaceState(new SpawnUnselectedState(spawnArea));
            }
            else if (character == active)
            {
                controller.HoverStates.PushState(new CharacterDragHoverState(active));
            }
            else
            {
                controller.MainStates.ReplaceState(new SpawnSelectedState(character, spawnArea));
                return false;
            }

            return true;
        });
    }

    public override bool DragEnd(int x, int y)
    {
        controller.HoverStates.PopState();
        return CellClick(x, y);
    }

    public override void OnEnter()
    {
        var map = controller.Map;
        map.MoveHighlightLayer.Clear();

        var highlight = map.MoveHighlightLayer;
        spawnArea.ForEach(cell => highlight.Highlight(cell.X, cell.Y, MoveHighlightType.LongMove));
        map.MoveHighlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);
    }
    public override void OnLeave()
    {
        var map = controller.Map;
        map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideMenuWithActions();
    }
}