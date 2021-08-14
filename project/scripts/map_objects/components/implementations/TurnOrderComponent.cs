using System.Collections.Generic;
using Godot;

public class TurnOrderComponent: Node
{
    private int orderNumber = 0;
    public int OrderNumber => orderNumber;
    public void OnTurnPlanned(Character parent, List<Character> plan)
    {
        orderNumber = plan.IndexOf(parent) + 1;
    }
}