using System;
using System.Collections.Generic;
using System.Linq;

public class ActiveCharacterState: BaseControllerState
{
    protected Character active;

    protected const string AttackAction = "Attack";
    protected const string MoveAction = "Move";

    public ActiveCharacterState(Character character)
    {
        this.active = character;
    }

    protected override BaseControllerState CharacterClick(Character character)
    {
        if (character == active) return this;

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }
        return NextState(new EnemySelectedState(character));
    }

    public override BaseControllerState MenuActionSelected(string action)
    {
        if (action == AttackAction)
        {
            return NextState(new AttackCharacterState(active));
        } else if (action == MoveAction)
        {
            return NextState(new MoveCharacterState(active));
        }

        return this;
    }

    protected override BaseControllerState EmptyCellClick(int x, int y)
    {
        return NextState(new UnselectedControllerState());
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

        var hud = UserInterfaceService.GetHUD<TacticHUD>();

        var availableActions = new List<string>();
        if (active.AttackAvailable()) availableActions.Add(AttackAction);
        if (active.MoveAvailable()) availableActions.Add(MoveAction);
        hud?.DisplayMenuWithActions(availableActions);
    }

    public override void OnLeave()
    {
        DisplayOnHUD(null);
        map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideMenuWithActions();
    }
}