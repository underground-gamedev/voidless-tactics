using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class StateStack<T> where T: BaseState
{
    private List<T> stack = new List<T>();

    private HumanController parent;

    private void Stacktrace()
    {
        var delimiter = "+-----------------------+\n";
        var result = delimiter;
        foreach(var frame in stack.AsEnumerable().Reverse())
        {
            result += frame.GetType().Name + "\n";
            result += delimiter;
        }
        GD.Print(result);
    }

    public StateStack(HumanController parent, T initState)
    {
        this.parent = parent;
        initState.SetController(parent);
        stack.Add(initState);
    }
    public void PushState(T state)
    {
        stack.Last().OnLeave();
        state.SetController(parent);
        stack.Add(state);
        state.OnEnter();
    }

    public void PopState()
    {
        if (stack.Count <= 1) return;
        var top = stack.Last();
        top.OnLeave();
        stack.Remove(top);
        stack.Last().OnEnter();
    }

    public void ReplaceState(T state)
    {
        if (stack.Count <= 1)
        {
            PushState(state);
            return;
        }

        var top = stack.Last();
        top.OnLeave();
        stack.Remove(top);

        state.SetController(parent);
        stack.Add(state);
        state.OnEnter();
    }

    public void Clear()
    {
        if (stack.Count <= 1) return;
        while (stack.Count > 1)
        {
            PopState();
        }
        stack.First().OnEnter();
    }

    public void HandleEvent(Func<T, bool> handler)
    {
        var topState = stack.Last();
        while (true)
        {
            var result = handler(topState);
            if (result) return;
            if (topState == stack.Last()) return;
            topState = stack.Last();
        }
    }
}