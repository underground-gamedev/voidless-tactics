using System;
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

	public static T FindParent<T>(this Node node)
	{
		var currentParent = node.GetParent();
		while (currentParent != null)
		{
			if (currentParent is T target)
			{
				return target;
			}
			currentParent = currentParent.GetParent();
		}

		throw new InvalidProgramException($"Expected {nameof(T)} as parent");
	}

	public static T FindChild<T>(this Node node)
	{
		var childs = node.GetChilds<T>(".");
		return childs.Count > 0 ? childs[0] : default;
	}

	public static async Task Wait(this Node node, float time)
	{
		await node.ToSignal(node.GetTree().CreateTimer(time), "timeout");
	}

	public static void GroupTrigger(this SceneTree scene, string trigger, params object[] args)
	{
		scene.CallGroup(trigger, trigger, args);
	}

	public static void PropagateCall(this Node node, string method, params object[] args)
	{
		node.PropagateCall(method, args: new Godot.Collections.Array(args));
	}
}
