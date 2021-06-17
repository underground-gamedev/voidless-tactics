using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AttackCharacterState: BaseControllerState
{

    private Character active;
    private IAttackComponent attackComponent;

    public AttackCharacterState(Character character)
    {
        active = character;
        attackComponent = active.Components.FindChild<IAttackComponent>();
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return NextState(new ActiveCharacterState(active));
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        if (active == character) { return NextState(new ActiveCharacterState(active)); }

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }

        if (!attackComponent.AttackAvailable())
        {
            return NextState(new ActiveCharacterState(active));
        }

        var attackArea = attackComponent.GetAttackArea();
        var targetComponent = character.Components.FindChild<ITargetComponent>();
        if (targetComponent != null && attackArea.Contains(character.Cell))
        {
            attackComponent.Attack(targetComponent);
        }

        return NextState(new ActiveCharacterState(active));
    }

    private void DisplayOnHUD(Character character)
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(character);
    }

    public override void OnEnter()
    {
        DisplayOnHUD(active);

        map.MoveHighlightLayer.Clear();
        map.MoveHighlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        UserInterfaceService.GetHUD<TacticHUD>()?.HideMenuWithActions();

        var highlightLayer = map.MoveHighlightLayer;
        foreach (var cell in attackComponent.GetAttackArea())
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.Attack);
        }
    }

    public override void OnLeave()
    {
        DisplayOnHUD(null);

        base.OnLeave();
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
    }
}