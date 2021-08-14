using System;

public static class StateHelpers
{
    public static bool CellByPos(this BaseState state, int x, int y, Func<MapCell, bool> handler)
    {
        if (state.Map.IsOutOfBounds(x, y)) return false;
        var cell = state.Map.CellBy(x, y);
        return handler(cell);
    }

    public static bool CharacterByPos(this BaseState state, int x, int y, Func<Character, bool> handler)
    {
        if (state.Map.IsOutOfBounds(x, y)) return false;
        var cell = state.Map.CellBy(x, y);
        var character = cell.MapObject as Character;
        if (character == null) return false;
        return handler(character);
    }
}