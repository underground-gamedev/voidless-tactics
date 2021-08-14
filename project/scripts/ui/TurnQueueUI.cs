using System.Collections.Generic;
using System.Linq;
using Godot;

public class TurnQueueUI: Control
{
    [Export]
    private PackedScene queueElement;

    public override void _Ready()
    {
        SetPlannedQueue(new List<Character>());
    }
    public void SetPlannedQueue(List<Character> plannedQueue)
    {
        foreach (Node child in GetChildren()) {
            RemoveChild(child);
            child.QueueFree();
        }

        if (plannedQueue.Count == 0) return;

        var plannedElements = new List<TurnQueueElement>();

        foreach (var character in plannedQueue) {
            var charElement = queueElement.Instance<TurnQueueElement>();
            charElement.SetCharacter(character);
            AddChild(charElement);
            plannedElements.Add(charElement);
        }

        plannedElements.First().SetActive(true);
    }
}