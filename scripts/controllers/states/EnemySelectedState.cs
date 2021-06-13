using System;
using System.Linq;
using System.Threading.Tasks;

public class EnemySelectedState: BaseControllerState
{
    private Character enemy;

    public EnemySelectedState(Character enemy)
    {
        this.enemy = enemy;
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        if (character == enemy) return Async(this);

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }

        return NextState(new EnemySelectedState(character));
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return NextState(new UnselectedControllerState());
    }

    private void DisplayOnHUD(Character character)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        if (hud == null) return;
        hud.DisplayCharacter(character);
    }

    public override void OnEnter()
    {
        DisplayOnHUD(enemy);

        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        foreach (var cell in map.GetAvailableMoveCells(enemy))
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.NormalMove);
        }
        highlightLayer.Highlight(enemy.Cell.X, enemy.Cell.Y, MoveHighlightType.Active);
    }

    public override void OnLeave()
    {
        DisplayOnHUD(null);
        map.MoveHighlightLayer.Clear();
    }
}