using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class ComponentContainer: Node
{
    private Dictionary<Type, Node> components = new Dictionary<Type, Node>();

    public T GetComponent<T>() where T: class
    {
        lock(components)
        {
            Node com = null;
            var type = typeof(T);
            if (components.TryGetValue(type, out com))
            {
                return com as T;
            }

            com = this.GetChilds<Node>(".").FirstOrDefault(child => child is T);
            if (com != null)
            {
                components.Add(type, com);
            }
            return com as T;
        }
    }

    public void AddComponent(Node component)
    {
        lock(components)
        {
            AddChild(component);
        }
    }

    public void RemoveComponent(Node component)
    {
        lock(components)
        {
            var needRemove = components.Keys.Where(key => components[key] == component);
            foreach(var key in needRemove)
            {
                components.Remove(key);
            }
            RemoveChild(component);
            component.QueueFree();
        }
    }
}