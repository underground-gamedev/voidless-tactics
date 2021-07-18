using System;

public struct SpellComponentContext
{
    public Character Caster;
    public TacticMap Map;
    public MapCell BaseCell;

    public SpellComponentContext(Character caster, TacticMap map, MapCell baseCell)
    {
        Caster = caster;
        Map = map;
        BaseCell = baseCell; 
    }

    public SpellComponentContext(Character caster, MapCell baseCell): this(caster, caster.Map, baseCell)
    {
    }

    public SpellComponentContext(Character caster): this(caster, caster.Cell)
    {
    }

    public SpellComponentContext SetCaster(Character caster)
    {
        return new SpellComponentContext(caster, Map, BaseCell);
    }

    public SpellComponentContext SetMap(TacticMap map)
    {
        return new SpellComponentContext(Caster, map, BaseCell);
    }

    public SpellComponentContext SetBaseCell(MapCell baseCell)
    {
        return new SpellComponentContext(Caster, Map, baseCell);
    }
}