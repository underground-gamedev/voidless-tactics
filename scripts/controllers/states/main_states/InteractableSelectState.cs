using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class InteractableSelectState: BaseControllerState
{
    protected Character active;
    private List<MoveCell> availableMoveCells;
    private List<Character> availableAttackTargets;
    private LastHoverMemoryState hoverMemoryState;

    protected const string AttackAction = "Attack";
    protected const string MoveAction = "Move";
    protected const string SpellAction = "Cast";

    public InteractableSelectState(Character character)
    {
        this.active = character;
    }

    public override bool CellClick(int x, int y)
    {
        var charClick = this.CharacterByPos(x, y, (character) => {
            if (!availableAttackTargets.Contains(character)) return false;

            var neighbours = Map.DirectNeighboursFor(x, y);
            var srcPos = neighbours.FirstOrDefault(neigh => neigh.Position == hoverMemoryState.LastPosition);
            if (srcPos == null || availableMoveCells.All(c => c.MapCell != srcPos)) {
                srcPos = neighbours.First(neigh => availableMoveCells.Any(c => c.MapCell == neigh));
            }

            controller.MainStates.PopState();
            controller.MainStates.PushState(new EventConsumerMainState());
            active.Components
                .FindChild<IMoveComponent>()
                .MoveTo(srcPos)
                .GetAwaiter()
                .OnCompleted(() => {
                    var targetComponent = character.Components.FindChild<ITargetComponent>();
                    var attackComponent = active.Components.FindChild<IAttackComponent>();
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
            active.Components
                .FindChild<IMoveComponent>()
                .MoveTo(cell)
                .GetAwaiter()
                .OnCompleted(() => controller.TriggerEndTurn());

            return true;
        });
    }

    public override void OnEnter()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(active);

        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
        highlightLayer.Highlight(active.Cell.X, active.Cell.Y, MoveHighlightType.Active);

        availableMoveCells = new List<MoveCell>();
        var moveComponent = active.Components.FindChild<IMoveComponent>();
        if (moveComponent?.MoveAvailable() == true) {
            availableMoveCells = moveComponent.GetMoveArea();
            foreach (var moveCell in availableMoveCells)
            {
                var (x, y) = moveCell.MapCell.Position;
                var highlightType = moveCell.ActionNeed == 1 ? MoveHighlightType.NormalMove : MoveHighlightType.LongMove;
                highlightLayer.Highlight(x, y, highlightType);
            }
        }

        availableAttackTargets = new List<Character>();
        var attackComponent = active.Components.FindChild<IAttackComponent>();
        if (attackComponent?.AttackAvailable() == true) {
            var allMoveArea = availableMoveCells.Select(cell => cell.MapCell).ToList();
            allMoveArea.Add(active.Cell);
            var attackTargets = allMoveArea
                .SelectMany(cell => attackComponent.GetAttackArea(cell))
                .Distinct()
                .Select(cell => cell.MapObject as Character)
                .Where(ch => ch != null && ch.Controller != active.Controller)
                .ToList();

            attackTargets.ForEach(ch => highlightLayer.Highlight(ch.Cell.X, ch.Cell.Y, MoveHighlightType.Attack));
            availableAttackTargets = attackTargets;
        }

        hoverMemoryState = new LastHoverMemoryState(active.Cell.X, active.Cell.Y);
        controller.HoverStates.PushState(hoverMemoryState);
    }

    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideActiveCharacter();

        var map = controller.Map;
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();

        controller.HoverStates.PopState();
    }
}