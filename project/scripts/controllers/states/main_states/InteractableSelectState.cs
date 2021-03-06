using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class InteractableSelectState: BaseControllerState
{
    private List<MoveCell> availableMoveCells;
    private List<Character> availableAttackTargets;

    public List<MoveCell> AvailableMoveCells => availableMoveCells;
    public List<Character> AvailableAttackTargets => availableAttackTargets;

    protected const string ManaPickupAction = "Mana Pickup";
    protected const string GiveManaAction = "Give Mana";
    protected const string SpellAction = "Cast";
    protected const string WaitAction = "Wait";
    protected const string SkipTurnAction = "Skip";

    public override bool CellClick(int x, int y, Vector2 offset)
    {
        var charClick = this.CharacterByPos(x, y, (character) => {
            if (!availableAttackTargets.Contains(character)) return false;

            var srcPos = GetAttackSourcePos(x, y, offset);

            if (srcPos is null) {
                GD.PrintErr($"Unexpected srcPos is null. x: {x}, y: {y}, offset: {offset}");
                return false;
            }

            controller.MainStates.PopState();
            controller.MainStates.PushState(new EventConsumerMainState());
            controller.ActiveCharacter.Components
                .GetComponent<IMoveComponent>()
                .MoveTo(srcPos)
                .GetAwaiter()
                .OnCompleted(() => {
                    var targetComponent = character.Components.GetComponent<ITargetComponent>();
                    var attackComponent = controller.ActiveCharacter.Components.GetComponent<IAttackComponent>();
                    attackComponent
                        .Attack(targetComponent)
                        .GetAwaiter()
                        .OnCompleted(() => controller.TriggerEndTurn());
                });

            return true;
        });

        if (charClick) return false;

        return this.CellByPos(x, y, (cell) => {
            if (availableMoveCells.All(c => c.MapCell != cell)) return false;

            controller.MainStates.PopState();
            controller.MainStates.PushState(new EventConsumerMainState());
            controller.ActiveCharacter.Components
                .GetComponent<IMoveComponent>()
                .MoveTo(cell)
                .GetAwaiter()
                .OnCompleted(() => controller.TriggerEndTurn());

            return true;
        });
    }

    public MapCell GetAttackSourcePos(int x, int y, Vector2 offset)
    {
        var magnetDirections = new Dictionary<Vector2, (int, int)>() {
            [new Vector2(0, 0)] = (-1, -1),
            [new Vector2(0.5f, 0)] = (0, -1),
            [new Vector2(1f, 0)] = (1, -1),
            [new Vector2(0, 0.5f)] = (-1, 0),
            [new Vector2(1f, 0.5f)] = (1, 0),
            [new Vector2(0, 1f)] = (-1, 1),
            [new Vector2(0.5f, 1f)] = (0, 1),
            [new Vector2(1f, 1f)] = (1, 1),
        }.Where(dir => !Map.IsOutOfBounds(x + dir.Value.Item1, y + dir.Value.Item2))
        .Select(dir => new KeyValuePair<Vector2, MapCell>(dir.Key, Map.CellBy(x + dir.Value.Item1, y + dir.Value.Item2)))
        .Where(dir => availableMoveCells.Any(c => c.MapCell == dir.Value) || dir.Value == controller.ActiveCharacter.Cell)
        .ToDictionary(elem => elem.Key, elem => elem.Value);

        MapCell srcPos = null;
        float minDistance = float.PositiveInfinity;
        foreach (var magnet in magnetDirections)
        {
            var distance = offset.DistanceTo(magnet.Key);
            if (distance < minDistance)
            {
                srcPos = magnet.Value;
                minDistance = distance;
            }
        }

        return srcPos;
    }

    public override bool MenuActionSelected(string action)
    {
        var activeCharacter = controller.ActiveCharacter;
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(activeCharacter);

        switch (action) {
            case ManaPickupAction:
                controller.MainStates.PushState(new EventConsumerMainState());
                activeCharacter.GetManaPickupComponent()
                    .ManaPickup(activeCharacter.Cell)
                    .GetAwaiter()
                    .OnCompleted(() => {
                        controller.MainStates.PopState();
                        hud?.DisplayActiveCharacter(activeCharacter);
                        ShowMenuAction();
                    });
                break;
            case GiveManaAction:
                controller.MainStates.PushState(new GiveManaState());
                break;
            case SpellAction:
                controller.MainStates.PushState(new SpellSelectState());
                break;
            case WaitAction:
                controller.MainStates.PushState(new EventConsumerMainState());
                activeCharacter.GetWaitComponent()
                    .Wait()
                    .GetAwaiter()
                    .OnCompleted(() => controller.TriggerEndTurn());
                break;
            case SkipTurnAction:
                controller.TriggerEndTurn();
                break;
            default:
                GD.PrintErr($"Invalid menu action for {nameof(InteractableSelectState)} named: {action}");
                break;
        }
        return true;
    }

    private void ShowMenuAction()
    {
        var acitveCharacter = controller.ActiveCharacter;
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        var availableActions = new List<string>();
        if (acitveCharacter.GetManaPickupComponent()?.ManaPickupAvailable(acitveCharacter.Cell) == true) availableActions.Add(ManaPickupAction);
        if (acitveCharacter.GetManaGiveComponent()?.GiveManaAvailable() == true) availableActions.Add(GiveManaAction);
        if (acitveCharacter.GetSpellComponent()?.CastSpellAvailable() == true) availableActions.Add(SpellAction);
        if (acitveCharacter.GetWaitComponent()?.WaitAvailable() == true) availableActions.Add(WaitAction);

        availableActions.Add(SkipTurnAction);

        hud?.HideMenuWithActions();
        if (availableActions.Count > 0)
        {
            hud?.DisplayMenuWithActions(availableActions);
        }
    }

    public override void OnEnter()
    {
        var activeCharacter = controller.ActiveCharacter;
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(activeCharacter);

        ShowMenuAction();

        availableMoveCells = new List<MoveCell>();
        var moveComponent = activeCharacter.Components.GetComponent<IMoveComponent>();
        if (moveComponent?.MoveAvailable() == true) {
            availableMoveCells = moveComponent.GetMoveArea();
        }

        availableAttackTargets = new List<Character>();
        var attackComponent = activeCharacter.Components.GetComponent<IAttackComponent>();
        if (attackComponent?.AttackAvailable() == true) {
            var allMoveArea = availableMoveCells.Select(cell => cell.MapCell).ToList();
            allMoveArea.Add(activeCharacter.Cell);
            var attackTargets = allMoveArea
                .SelectMany(cell => attackComponent.GetAttackArea(cell))
                .Distinct()
                .Select(cell => cell.MapObject as Character)
                .Where(ch => ch != null && ch.Controller != activeCharacter.Controller)
                .ToList();

            availableAttackTargets = attackTargets;
        }

        var hoverState = new AttackHoverState(this);
        controller.HoverStates.PushState(hoverState);
    }

    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideActiveCharacter();
        hud?.HideMenuWithActions();

        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();

        controller.HoverStates.PopState();
    }
}