using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Battle
{
    public interface IManaGiveComponent
    {
        bool GiveManaAvailable(MapBuilder mapMono);
        bool GiveManaAvailable(MapBuilder mapMono, MapCell target);
        List<MapCell> GetGiveManaArea(MapBuilder mapMono, MapCell src);
        IEnumerator GiveMana(MapBuilder mapMono, Character target);
    }
}