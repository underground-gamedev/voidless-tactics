using System.Collections.Generic;
using System.Linq;
using Godot;

public class TurnQueueUI: Control
{
    [Export]
    private PackedScene queueElement;
    private TurnQueueElement bigElement;
    private QueueContainer smallElementQueue;

    public override void _Ready()
    {
        smallElementQueue = GetNode<QueueContainer>("SmallElementQueue");
        bigElement = GetNode<TurnQueueElement>("BigTurnQueueElement");
        SetPlannedQueue(new List<Character>());
    }
    public void SetPlannedQueue(List<Character> plannedQueue)
    {
        foreach (Node child in smallElementQueue.GetChildren()) {
            smallElementQueue.RemoveChild(child);
            child.QueueFree();
        }

        bigElement.Visible = false;

        if (plannedQueue.Count == 0) return;

        var plannedElements = new List<TurnQueueElement>();

        bigElement.Visible = true;
        bigElement.SetCharacter(plannedQueue.First());

        for (int i = 1; i < plannedQueue.Count; i++)
        {
            var character = plannedQueue[i];
            var charElement = queueElement.Instance<TurnQueueElement>();
            charElement.SetCharacter(character);
            smallElementQueue.AddChild(charElement);
            plannedElements.Add(charElement);
        }
    }
}