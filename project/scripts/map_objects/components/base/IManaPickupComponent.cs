using System.Threading.Tasks;

public interface IManaPickupComponent
{
    bool ManaPickupAvailable(MapCell src);
    bool ManaPickupAvailable();
    Task ManaPickup(MapCell src);
}