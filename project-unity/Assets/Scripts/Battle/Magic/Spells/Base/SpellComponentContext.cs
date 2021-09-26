using Battle.Map.Interfaces;

namespace Battle
{
    public readonly struct SpellComponentContext
    {
        public readonly ICharacter Caster;
        public readonly ILayeredMap Map;
        public readonly MapCell TargetCell;
        public readonly MapCell SourceCell;

        public SpellComponentContext(ICharacter caster, ILayeredMap map, MapCell sourceCell, MapCell targetCell)
        {
            Caster = caster;
            Map = map;
            SourceCell = sourceCell;
            TargetCell = targetCell; 
        }

        /*public SpellComponentContext(ICharacter caster, ILayeredMap map, MapCell targetCell): this(caster, map, targetCell, targetCell)
        {
        }

        public SpellComponentContext(ICharacter caster, MapCell baseCell): this(caster, caster.MapObject.MapMono, baseCell)
        {
        }

        public SpellComponentContext(ICharacter caster): this(caster, caster.MapObject.Cell)
        {
        }*/

        public SpellComponentContext SetCaster(ICharacter caster)
        {
            return new SpellComponentContext(caster, Map, SourceCell, TargetCell);
        }

        public SpellComponentContext SetMap(ILayeredMap map)
        {
            return new SpellComponentContext(Caster, map, SourceCell, TargetCell);
        }

        public SpellComponentContext SetTargetCell(MapCell targetCell)
        {
            return new SpellComponentContext(Caster, Map, SourceCell, targetCell);
        }

        public SpellComponentContext SetSourceCell(MapCell sourceCell)
        {
            return new SpellComponentContext(Caster, Map, sourceCell, TargetCell);
        }
    }
}