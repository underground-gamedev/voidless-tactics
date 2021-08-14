using System;
using System.Collections.Generic;
using Godot;

public class Character : MapObject, IBasicStatsPresenter
{
    private BasicStats basicStats;
    public BasicStats BasicStats => basicStats = basicStats ?? GetNode<BasicStats>("Stats");

    private ComponentContainer components;
    public ComponentContainer Components => components = components ?? GetNode<ComponentContainer>("Components");

    private Control characterHUD;
    public Control CharacterHUD => characterHUD = characterHUD ?? GetNode<Control>("CharacterHUD");

    private List<Node> propagateChilds = new List<Node>();

    private AbstractController controller;
    public AbstractController Controller {
        get => controller;
        set => controller = value;
    }

    public override void _Ready()
    {
        if (this.FindChild<ComponentContainer>() == null)
        {
            AddChild(new ComponentContainer());
        }

        var baseComponents = new List<Node>() {
            new TargetComponent(),
            new TurnOrderComponent(),
            new MoveComponent(),
            new AttackComponent(),
        };
        baseComponents.ForEach(com => Components.AddComponent(com));

        propagateChilds.Add(Components);
        propagateChilds.Add(CharacterHUD);
    }

    public void Kill()
    {
        GetTree().GroupTrigger(GDTriggers.CharacterDeathTrigger, this);

        Cell.MapObject = null;
        controller?.RemoveCharacter(this);
        GetParent().RemoveChild(this);
        QueueFree();
    }

    public void OnTurnStart()
    {
        propagateChilds.ForEach(child => child.PropagateCall(nameof(OnTurnStart), this));
    }

    public void OnTurnEnd()
    {
        propagateChilds.ForEach(child => child.PropagateCall(nameof(OnTurnEnd), this));
    }

    public void OnRoundStart()
    {
        propagateChilds.ForEach(child => child.PropagateCall(nameof(OnRoundStart), this));
    }

    public void OnTurnPlanned(List<Character> plan)
    {
        propagateChilds.ForEach(child => child.PropagateCall(nameof(OnTurnPlanned), this, plan));
    }

    public void OnRoundEnd()
    {
        propagateChilds.ForEach(child => child.PropagateCall(nameof(OnRoundEnd), this));
    }
}
