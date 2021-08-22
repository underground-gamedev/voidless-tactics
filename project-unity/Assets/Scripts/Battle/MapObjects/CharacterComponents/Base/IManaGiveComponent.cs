using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Battle
{
    public interface IManaGiveComponent
    {
        bool GiveManaAvailable(TacticMap map);
        bool GiveManaAvailable(TacticMap map, MapCell target);
        List<MapCell> GetGiveManaArea(TacticMap map, MapCell src);
        IEnumerator GiveMana(TacticMap map, Character target);
    }
}