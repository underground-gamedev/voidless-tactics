using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class HumanController: AbstractController
{
    private StateStack<BaseHoverState> hoverStates;
    public StateStack<BaseHoverState> HoverStates => hoverStates;
    private StateStack<BaseControllerState> mainStates;
    public StateStack<BaseControllerState> MainStates => mainStates;

    public TacticMap Map => tacticMap;

    [Signal]
    public delegate void EndTurn();

    public void TriggerEndTurn()
    {
        EmitSignal(nameof(EndTurn));
    }

    public HumanController()
    {
        hoverStates = new StateStack<BaseHoverState>(this, new EventConsumerHoverState());
        mainStates = new StateStack<BaseControllerState>(this, new EventConsumerMainState());
    }

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

        mainStates.PushState(new SpawnUnselectedState(startArea));
        hoverStates.PushState(new SimpleHoverState());

        await UserInterfaceService.GetHUD<TacticHUD>().WaitCompleteButtonPressed();
        mainStates.Clear();
        hoverStates.Clear();
    }

    public override async Task MakeTurn(Character active)
    {
        mainStates.PushState(new InteractableSelectState(active));
        hoverStates.PushState(new SimpleHoverState());

        await ToSignal(this, nameof(EndTurn));

        mainStates.Clear();
        hoverStates.Clear();
    }

    public void OnDragStart(int x, int y)
    {
        mainStates.HandleEvent(state => state.DragStart(x, y));
    }

    public void OnDragEnd(int x, int y)
    {
        mainStates.HandleEvent(state => state.DragEnd(x, y));
    }

    public void OnCellClick(int x, int y)
    {
        mainStates.HandleEvent(state => state.CellClick(x, y));
    }

    public void OnCellHover(int x, int y)
    {
        hoverStates.HandleEvent(state => state.OnCellHover(x, y));
    }

    public void OnActionSelected(string actionName)
    {
        mainStates.HandleEvent(state => state.MenuActionSelected(actionName));
    }
}