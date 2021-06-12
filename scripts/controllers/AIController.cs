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
            var availablePositions = tacticMap.PathfindLayer.GetAllAvailablePathDest(character.Cell, character.MovePoints);
            var availableCells = availablePositions.Select(pos => tacticMap.CellBy(pos.Item1, pos.Item2)).Where(cell => cell.Character == null).ToList();
            var randIndex = rand.Next(0, availableCells.Count);
            var target = availableCells[randIndex];
            await character.MoveTo(target.X, target.Y);
        }
        await this.Wait(1);
        EmitSignal(nameof(OnEndTurn));
    }
}