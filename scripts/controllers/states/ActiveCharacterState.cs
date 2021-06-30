using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ActiveCharacterState: BaseControllerState
{
    protected Character active;

    protected const string AttackAction = "Attack";
    protected const string MoveAction = "Move";
    protected const string SpellAction = "Cast";

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
        if (!controller.IsMyTurn()) return Async(this);
        switch (action)
        {
            case AttackAction: return NextState(new AttackCharacterState(active));
            case MoveAction: return NextState(new MoveCharacterState(active));
            case SpellAction: return NextState(new SpellSelectState(active));
        }

        return Async(this);
    }

    protected override Task<BaseControllerState> EmptyCellClick(int x, int y)
    {
        return NextState(new UnselectedControllerState());
    }

    public override void OnEnter()
    {
        map.MoveHighlightLayer.Clear();
        map.MoveHighlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayCharacter(active);

        var availableActions = new List<string>();
        if (active.Components.FindChild<IAttackComponent>()?.AttackAvailable() == true) availableActions.Add(AttackAction);
        if (active.Components.FindChild<IMoveComponent>()?.MoveAvailable() == true) availableActions.Add(MoveAction);
        if (active.Components.FindChild<ISpellComponent>()?.CastSpellAvailable() == true) availableActions.Add(SpellAction);
        hud?.DisplayMenuWithActions(active.GetGlobalTransformWithCanvas().origin + new Godot.Vector2(20f, 0.5f), availableActions);
    }

    public override void OnLeave()
    {
        map.MoveHighlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideCharacterDisplay();
        hud?.HideMenuWithActions();
    }
}