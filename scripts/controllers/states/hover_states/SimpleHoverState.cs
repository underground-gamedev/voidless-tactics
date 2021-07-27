public class SimpleHoverState: BaseHoverState
{
    private Character defaultCharacter;

    public SimpleHoverState(Character defaultCharacter = null)
    {
        this.defaultCharacter = defaultCharacter;
    }

    public override bool OnCellHover(int x, int y)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        var result = this.CellByPos(x, y, (cell) => {
            hud?.DisplayCharacter(cell.MapObject as Character);
            hud?.DisplayCellInfo(cell);
            return true;
        });

        if (result) return true;

        hud?.DisplayCharacter(defaultCharacter);
        hud?.HideCellInfo();
        return false;
    }

    public override void OnEnter()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(defaultCharacter);
        hud?.DisplayCellInfo(defaultCharacter?.Cell);
    }

    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideCellInfo();
    }
}