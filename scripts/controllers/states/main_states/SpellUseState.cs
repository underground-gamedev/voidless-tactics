using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpellUseState: BaseControllerState
{
    private Character active;
    private ISpell activeSpell;

    public SpellUseState(Character character, ISpell spell)
    {
        active = character;
        activeSpell = spell;
    }

    public override bool CellClick(int x, int y)
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
                controller.MainStates.PushState(new SpellSelectState(active));
            }

            return true;
        });
    }

    public override void OnEnter()
    {
        var hud = UserInterfaceService.GetHUD<TacticHUD>();
        hud?.DisplayActiveCharacter(active);
        hud?.DisplayCellInfo(active.Cell);
        hud?.DisplaySpellDescriptor(activeSpell.GetDescription());
        
        controller.HoverStates.PushState(new SpellUseHoverState(active, activeSpell));
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