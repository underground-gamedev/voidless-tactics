using System.Collections;

namespace Battle
{
    public interface IManaPickupComponent
    {
        bool ManaPickupAvailable(TacticMap map, MapCell src);
        bool ManaPickupAvailable(TacticMap map);
        IEnumerator ManaPickup(TacticMap map, MapCell src);
    }
}