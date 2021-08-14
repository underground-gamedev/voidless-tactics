using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class SpellUseState: BaseControllerState
{
    private ISpell activeSpell;

    public SpellUseState(ISpell spell)
    {
        activeSpell = spell;
    }

    public override bool CellClick(int x, int y, Vector2 offset)
    {
        return this.CellByPos(x, y, (cell) => {
            controller.MainStates.PopState();
            
            if (activeSpell.CastAvailable(cell))
            {
                controller.MainStates.PushState(new EventConsumerMainState());
                activeSpell.ApplyEffect(cell)
                    .GetAwaiter()
                    .OnCompleted(() => controller.TriggerEndTurn());
            }
            else
            {
                controller.MainStates.PushState(new SpellSelectState());
            }

            return true;
        });
    }

    public override void OnEnter()
    {
        var activeCharacter = controller.ActiveCharacter;
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(activeCharacter);
        hud?.DisplayCellInfo(activeCharacter.Cell);
        hud?.DisplaySpellDescriptor(activeSpell.GetDescription());
        
        controller.HoverStates.PushState(new SpellUseHoverState(activeCharacter, activeSpell));
    }

    public override void OnLeave()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.HideActiveCharacter();
        hud?.HideCellInfo();
        hud?.HideSpellDescriptor();

        controller.HoverStates.PopState();
    }
}