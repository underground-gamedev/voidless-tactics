using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class TurnManager: Node
{
    private List<Character> allCharacters;
    private List<Character> plannedQueue;
    private int roundNumber;
    private Random rand = new Random();

    public override void _Ready()
    {
        AddToGroup(GDTriggers.CharacterDeathTrigger);
        AddToGroup(GDTriggers.CharacterWaitTrigger);
    }

    public void CharacterDeathTrigger(Character character)
    {
        allCharacters.Remove(character);
        plannedQueue.Remove(character);
    }

    public void CharacterWaitTrigger(Character character)
    {
        character.BasicStats.Initiative.AddModifier("initiative.wait", new FixStatModifier(-1, int.MaxValue));
        var lastWaiterIndex = plannedQueue.FindIndex(ch => ch.BasicStats.Initiative.ModifiedActualValue == 0);
        if (lastWaiterIndex == -1)
        {
            plannedQueue.Add(character);
        }
        else
        {
            plannedQueue.Insert(lastWaiterIndex, character);
        }
    }

    public async void TurnLoop(List<AbstractController> controllers) {
        allCharacters = controllers
            .SelectMany(c => c.Characters)
            .Where(ch => ch.Map != null)
            .ToList();
        roundNumber = 0;

        var hud = UserInterfaceService.GetHUD<TacticHUD>();

        while(true)
        {
            roundNumber++;
            var turnText = $"{roundNumber} Round";
            await hud.ShowTurnLabel(turnText);

            allCharacters.ForEach(ch => ch.OnRoundStart());
            plannedQueue = PlanQueue(allCharacters);
            allCharacters.ForEach(ch => ch.OnTurnPlanned(plannedQueue));
            while(plannedQueue.Count > 0)
            {
                hud?.SetPlannedQueue(plannedQueue);
                var activeCharacter = plannedQueue.First();
                plannedQueue.Remove(activeCharacter);

                var activeController = activeCharacter.Controller;
                activeCharacter.OnTurnStart();
                await activeController.MakeTurn(activeCharacter);
                activeCharacter.OnTurnEnd();

                plannedQueue = SortByInitiative(plannedQueue);
            }

            hud?.SetPlannedQueue(plannedQueue);

            ClearWaitModifiers();
            allCharacters.ForEach(ch => ch.OnRoundEnd());
        }
    }

    private List<Character> SortByInitiative(List<Character> plannedQueue)
    {
        return plannedQueue.OrderByDescending(ch => ch.BasicStats.Initiative.ModifiedActualValue).ToList();
    }

    private void ClearWaitModifiers()
    {
        foreach(var character in allCharacters)
        {
            character.BasicStats.Initiative.RemoveModifier("initiative.wait");
        }
    }

    private List<Character> PlanQueue(List<Character> allCharacters)
    {
        var initiativeGroups = new Dictionary<int, List<Character>>();

        foreach (var ch in allCharacters)
        {
            var initiative = ch.BasicStats.Initiative;
            var min = initiative.MinValue;
            var max = initiative.MaxValue;
            initiative.ActualValue = rand.Next(min, max + 1);

            var modifiedInitiative = initiative.ModifiedActualValue;

            if (!initiativeGroups.ContainsKey(modifiedInitiative)) {
                initiativeGroups.Add(modifiedInitiative, new List<Character>());
            }

            initiativeGroups[modifiedInitiative].Add(ch);
        };

        var plannedQueue = new List<Character>();
        foreach (var group in initiativeGroups.OrderByDescending(x => x.Key).Select(x => x.Value))
        {
            rand.Shuffle(group);
            plannedQueue.AddRange(group);
        }

        return plannedQueue;
    }

}