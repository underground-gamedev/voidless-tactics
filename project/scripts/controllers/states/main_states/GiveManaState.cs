using System.Collections.Generic;
using Godot;

public class GiveManaState: BaseControllerState
{
    private List<MapCell> giveManaArea = new List<MapCell>();
    private IManaGiveComponent giveComponent;
    public override bool CellClick(int x, int y, Vector2 offset)
    {
        controller.MainStates.PopState();
        return this.CellByPos(x, y, (cell) => {
            if (giveManaArea.Contains(cell) && giveComponent.GiveManaAvailable(cell)) {
                controller.MainStates.PushState(new EventConsumerMainState());
                giveComponent.GiveMana(cell.MapObject as Character)
                    .GetAwaiter()
                    .OnCompleted(() => { controller.TriggerEndTurn(); });
            }
            return true;
        });
    }

    public override void OnEnter()
    {
        var activeCharacter = controller.ActiveCharacter;
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(activeCharacter);
        hud?.DisplayCellInfo(activeCharacter.Cell);

        giveComponent = activeCharacter.GetManaGiveComponent();
        giveManaArea = giveComponent.GetGiveManaArea(activeCharacter.Cell);

        var highlightLayer = Map.MoveHighlightLayer;
        highlightLayer.Clear();
        giveManaArea.ForEach(cell => highlightLayer.Highlight(cell.X, cell.Y, giveComponent.GiveManaAvailable(cell) ? MoveHighlightType.Attack: MoveHighlightType.NormalMove));
        var (currX, currY) = activeCharacter.Cell.Position;
        highlightLayer.Highlight(currX, currY, MoveHighlightType.Active);
        
        controller.HoverStates.PushState(new SimpleHoverState());
    }

    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideActiveCharacter();
        hud?.HideCellInfo();
        hud?.HideSpellDescriptor();

        var highlightLayer = Map.MoveHighlightLayer;
        highlightLayer.Clear();
        
        controller.HoverStates.PopState();
    }
}