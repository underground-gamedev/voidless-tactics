using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpell
{
    List<MapCell> GetTargetArea();
    List<MapCell> GetEffectArea(MapCell target);
    bool CastAvailable(MapCell target);
    bool CastAvailable();
    Task ApplyEffect(MapCell target);
    string GetDescription();
}