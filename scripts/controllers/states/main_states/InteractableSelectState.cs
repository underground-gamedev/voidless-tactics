using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class InteractableSelectState: BaseControllerState
{
    protected Character active;

    protected const string AttackAction = "Attack";
    protected const string MoveAction = "Move";
    protected const string SpellAction = "Cast";

    public InteractableSelectState(Character character)
    {
        this.active = character;
    }

    // public override bool CellClick(int x, int y)
    // {
    //     controller.MainStates.PopState();
    //     return this.CharacterByPos(x, y, (character) => {
    //         controller.MainStates.PushState(new CharacterSelectTransition(character));
    //         return true;
    //     });
    // }

    public override bool MenuActionSelected(string action)
    {
        var states = new Dictionary<string, BaseControllerState>()
        {
            [AttackAction] = new AttackCharacterState(active),
            [MoveAction] = new MoveCharacterState(active),
            [SpellAction] = new SpellSelectState(active),
        };

        if (states.ContainsKey(action))
        {
            controller.MainStates.PushState(states[action]);
            return true;
        }

        return false;
    }

    public override void OnEnter()
    {
        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        highlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        var availableActions = new List<string>();
        if (active.Components.FindChild<IAttackComponent>()?.AttackAvailable() == true) availableActions.Add(AttackAction);
        if (active.Components.FindChild<IMoveComponent>()?.MoveAvailable() == true) availableActions.Add(MoveAction);
        if (active.Components.FindChild<ISpellComponent>()?.CastSpellAvailable() == true) availableActions.Add(SpellAction);

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayMenuWithActions(active.GetGlobalTransformWithCanvas().origin + new Godot.Vector2(20f, 0.5f), availableActions);
        hud?.DisplayActiveCharacter(active);
    }

    public override void OnLeave()
    {
        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();

        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideMenuWithActions();
        hud?.HideActiveCharacter();
    }
}