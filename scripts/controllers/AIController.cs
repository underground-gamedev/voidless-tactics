using Godot;
using System;
using System.Linq;

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
}
