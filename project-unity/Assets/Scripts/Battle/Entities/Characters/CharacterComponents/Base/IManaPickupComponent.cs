using System.Collections;

namespace Battle
{
    public interface IManaPickupComponent
    {
        bool ManaPickupAvailable(MapBuilder mapMono, MapCell src);
        bool ManaPickupAvailable(MapBuilder mapMono);
        IEnumerator ManaPickup(MapBuilder mapMono, MapCell src);
    }
}