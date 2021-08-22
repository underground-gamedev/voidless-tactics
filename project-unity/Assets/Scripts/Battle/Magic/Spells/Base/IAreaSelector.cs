using System.Collections.Generic;

namespace Battle
{
    public interface IAreaSelector
    {
        List<MapCell> GetFullArea(SpellComponentContext ctx);
        List<MapCell> GetRealArea(SpellComponentContext ctx);
        string GetDescription(Character caster);
    }
}