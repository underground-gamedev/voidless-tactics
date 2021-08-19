using System.Collections.Generic;
using System.Threading.Tasks;

public interface IManaGiveComponent
{
    bool GiveManaAvailable();
    bool GiveManaAvailable(MapCell target);
    List<MapCell> GetGiveManaArea(MapCell src);
    Task GiveMana(Character target);
}