using System;
using System.Collections.Generic;
using System.Linq;

public class AttackCharacterState: ActiveCharacterState
{

    public AttackCharacterState(Character character): base(character)
    {
    }

    protected override BaseControllerState CharacterClick(Character character)
    {
        if (character == active) return this;

        var fromMyTeam = controller.Characters.Contains(character);
        if (fromMyTeam)
        {
            return NextState(new ActiveCharacterState(character));
        }

        character.controller.RemoveCharacter(character);
        return NextState(new ActiveCharacterState(active));
    }

    public override void OnEnter()
    {
        base.OnEnter();
        var highlightLayer = map.MoveHighlightLayer;
        foreach (var cell in map.DirectNeighboursFor(active.Cell.X, active.Cell.Y))
        {
            highlightLayer.Highlight(cell.X, cell.Y, MoveHighlightType.Attack);
        }
    }

    public override void OnLeave()
    {
        base.OnLeave();
        var highlightLayer = map.MoveHighlightLayer;
        highlightLayer.Clear();
    }
}