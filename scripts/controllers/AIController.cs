using Godot;
using System;
using System.Linq;

public class AIController : AbstractController
{
    [Signal]
    public delegate void OnEndTurn();

    protected override MapCell FindStartPosition(TacticMap map)
    {
        for (int x = map.Width-1; x > 0; x--)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (!map.GetSolid(x, y) && map.GetCharacter(x, y) == null) {
                    return map.CellBy(x, y);
                }
            }
        }

        return null;
    }

    public override async void OnTurnStart()
    {
        base.OnTurnStart();
        var rand = new Random();
        await this.Wait(1);
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
        await this.Wait(1);
        EmitSignal(nameof(OnEndTurn));
    }
}
