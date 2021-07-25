using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AIController : AbstractController
{
	[Signal]
	public delegate void OnEndTurn();

	public override async void OnTurnStart()
	{
		base.OnTurnStart();
		var rand = new Random();
		await this.Wait(0.1f);
		foreach (var character in characters)
		{
			var moveComponent = character.Components.FindChild<IMoveComponent>();
			if (moveComponent?.MoveAvailable() != true) continue;

			var availableCells = moveComponent.GetMoveArea();
			if (availableCells.Count == 0) continue;

			var randIndex = rand.Next(0, availableCells.Count);

			var target = availableCells[randIndex];
			await moveComponent.MoveTo(target.MapCell);
		}
		await this.Wait(0.1f);
		EmitSignal(nameof(OnEndTurn));
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
