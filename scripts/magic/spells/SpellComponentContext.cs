using System;

public struct SpellComponentContext
{
    public Character Caster;
    public TacticMap Map;
    public MapCell TargetCell;
    public MapCell SourceCell;

    public SpellComponentContext(Character caster, TacticMap map, MapCell sourceCell, MapCell targetCell)
    {
        Caster = caster;
        Map = map;
        SourceCell = sourceCell;
        TargetCell = targetCell; 
    }

    public SpellComponentContext(Character caster, TacticMap map, MapCell targetCell): this(caster, map, targetCell, targetCell)
    {
    }

    public SpellComponentContext(Character caster, MapCell baseCell): this(caster, caster.Map, baseCell)
    {
    }

    public SpellComponentContext(Character caster): this(caster, caster.Cell)
    {
    }

    public SpellComponentContext SetCaster(Character caster)
    {
        return new SpellComponentContext(caster, Map, SourceCell, TargetCell);
    }

    public SpellComponentContext SetMap(TacticMap map)
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