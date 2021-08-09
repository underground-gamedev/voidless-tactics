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
        hoverStates.PushState(new SimpleHoverState());
        mainStates.PushState(new InteractableSelectState(active));

        await ToSignal(this, nameof(EndTurn));

        mainStates.Clear();
        hoverStates.Clear();
    }

    public void OnDragStart(int x, int y, Vector2 offset)
    {
        mainStates.HandleEvent(state => state.DragStart(x, y, offset));
    }

    public void OnDragEnd(int x, int y, Vector2 offset)
    {
        mainStates.HandleEvent(state => state.DragEnd(x, y, offset));
    }

    public void OnCellClick(int x, int y, Vector2 offset)
    {
        mainStates.HandleEvent(state => state.CellClick(x, y, offset));
    }

    public void OnCellHover(int x, int y, Vector2 offset)
    {
        hoverStates.HandleEvent(state => state.OnCellHover(x, y, offset));
    }

    public void OnActionSelected(string actionName)
    {
        mainStates.HandleEvent(state => state.MenuActionSelected(actionName));
    }
}