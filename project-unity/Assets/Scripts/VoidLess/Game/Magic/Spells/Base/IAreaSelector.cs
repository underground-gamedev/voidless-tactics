using System.Collections.Generic;
using VoidLess.Core.Entities;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Base
{
    public interface IAreaSelector
    {
        List<MapCell> GetFullArea(SpellComponentContext ctx);
        List<MapCell> GetRealArea(SpellComponentContext ctx);
        string GetDescription(IEntity caster);
    }
}