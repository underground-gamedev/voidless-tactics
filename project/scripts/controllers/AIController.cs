using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AIController : AbstractController
{
	[Signal]
	public delegate void OnEndTurn();

	public override async Task MakeTurn(Character active)
	{
		var aiComponent = active.GetAIComponent();
		if (aiComponent == null)
		{
			await DefaultAI(active);
		}
		else
		{
			await aiComponent.MakeTurn();
		}
	}

	private async Task DefaultAI(Character active)
	{
		var rand = new Random();
		var moveComponent = active.Components.GetComponent<IMoveComponent>();
		if (moveComponent?.MoveAvailable() != true) return;

		var availableCells = moveComponent.GetMoveArea();
		if (availableCells.Count == 0) return;

		var randIndex = rand.Next(0, availableCells.Count);

		var target = availableCells[randIndex];
		await moveComponent.MoveTo(target.MapCell);
	}

    public override Task SpawnUnits(TacticMap map, List<MapCell> startArea)
    {
		var rand = new Random();
		foreach(var character in characters)
		{
			var spawnPosIndex = rand.Next(0, startArea.Count);
			character.BindMap(map, startArea[spawnPosIndex]);
			startArea.RemoveAt(spawnPosIndex);
		}

		return Task.CompletedTask;
    }
}
