using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class HumanController: AbstractController
{
    private BaseControllerState state;

    public override async Task SpawnUnits(TacticMap map, List<MapCell> startArea)
    {
        var spawnPositions = startArea.ToList();
		var rand = new Random();
		foreach(var character in characters)
		{
			var spawnPosIndex = rand.Next(0, startArea.Count);
			character.BindMap(map, spawnPositions[spawnPosIndex]);
			spawnPositions.RemoveAt(spawnPosIndex);
		}

        state = new SpawnUnselectedState(this, tacticMap, startArea);
        state.OnEnter();

        await ToSignal(UserInterfaceService.GetHUD<TacticHUD>(), nameof(TacticHUD.EndTurnPressed));

        SetStartState();
    }

    private void SetStartState()
    {
        if (state != null)
        {
            state.OnLeave();
        }
        state = new UnselectedControllerState(this, tacticMap);
    }

    private void ChangeState(BaseControllerState nextState)
    {
        if (!nextState.Initialized) {
            throw new InvalidProgramException($"Invalid controller state initialization, recheck code. OnCellClick: {state.GetType().Name} -> {nextState.GetType().Name}");
        }
        state = nextState;
    }

    public async void OnCellClick(int x, int y)
    {
        if (state == null) return;
        ChangeState(await state.CellClick(x, y));
    }

    public void OnCellHover(int x, int y)
    {
        if (state == null) return;
        state.CellHover(x, y);
    }

    public async void OnActionSelected(string actionName)
    {
        ChangeState(await state.MenuActionSelected(actionName));
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        SetStartState();
    }

    /*
        public void SetActiveCharacter(Character character)
        {
            activeCharacter = character;
            activeState = ActiveCharacterStates.None;
            if (!SwitchToMove()) SwitchToNone();
            EmitSignal(nameof(OnActiveCharacterChanged), activeCharacter);
        }

        public bool SwitchToNone()
        {
            var moveHighlight = tacticMap.MoveHighlightLayer;
            activeState = ActiveCharacterStates.None;
            moveHighlight.Clear();

            return true;
        }

        public bool SwitchToMove()
        {
            if (activeCharacter == null) { return false; }
            if (!isMyTurn) { return false; }
            if (activeCharacter.MoveActions == 0) { return false; }

            var moveHighlight = tacticMap.MoveHighlightLayer;
            moveHighlight.Clear();
            activeState = ActiveCharacterStates.Move;

            var availablePositions = tacticMap.PathfindLayer.GetAllAvailablePathDest(activeCharacter.Cell, activeCharacter.MovePoints);
            var availableCells = availablePositions.Select(pos => tacticMap.CellBy(pos.Item1, pos.Item2)).Where(cell => cell.Character == null).ToList();
            foreach (var (availX, availY) in availableCells.Select(cell => cell.Position))
            {
                moveHighlight.Highlight(availX, availY, MoveHighlightType.NormalMove);
            }

            return true;
        }
        */
}