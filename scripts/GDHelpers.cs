using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public static class GDHelpers
{
    public static List<T> GetChilds<T>(this Node node, NodePath path)
    {
        var result = new List<T>();
        foreach (var child in node.GetNode<Node>(path).GetChildren())
        {
            if (child is T targetChild)
            {
                result.Add(targetChild);
            }
        }

        return result;
    }

    public static Task Wait(this Node node, float time)
    {
        return Task.Run(async () => {
            await node.ToSignal(node.GetTree().CreateTimer(time), "timeout");
        });
    }
}