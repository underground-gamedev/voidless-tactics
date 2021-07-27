using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class UnselectedControllerState: BaseControllerState
{
    public override bool CellClick(int x, int y)
    {
        return this.CharacterByPos(x, y, (character) => {
            controller.MainStates.PushState(new CharacterSelectTransition(character));
            return true;
        });
    }
}