using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ActiveCharacterState: BaseControllerState
{
    protected Character active;

    protected const string AttackAction = "Attack";
    protected const string MoveAction = "Move";

    public ActiveCharacterState(Character character)
    {
        this.active = character;
    }

    protected override Task<BaseControllerState> CharacterClick(Character character)
    {
        if (character == active) return Async(this);

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }
        return NextState(new EnemySelectedState(character));
    }

    public override Task<BaseControllerState> MenuActionSelected(string action)
    {
        if (action == AttackAction)
        {
            return NextState(new AttackCharacterState(active));
        } else if (action == MoveAction)
        {
            return NextState(new MoveCharacterState(active));
        }

        return Async(this);
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
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
        if (active.Components.FindChild<IAttackComponent>()?.AttackAvailable() == true) availableActions.Add(AttackAction);
        if (active.Components.FindChild<IMoveComponent>()?.MoveAvailable() == true) availableActions.Add(MoveAction);
        hud?.DisplayMenuWithActions(active.GetGlobalTransformWithCanvas().origin + new Godot.Vector2(20f, 0.5f), availableActions);
    }

    public override void OnLeave()
    {
        DisplayOnHUD(null);
        map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideMenuWithActions();
    }
}