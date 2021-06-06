using System.Collections.Generic;
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
}