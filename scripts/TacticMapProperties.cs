public static class TacticMapProperties
{
	public static bool GetSolid(this TacticMap map, int x, int y)
	{
		return map.CellBy(x, y).Solid;
	}

	public static void SetSolid(this TacticMap map, int x, int y, bool value)
	{
		map.CellBy(x, y).Solid = value;
	}

	public static Character GetCharacter(this TacticMap map, int x, int y)
	{
		return map.CellBy(x, y).Character;
	}

	public static void SetCharacter(this TacticMap map1, int x, int y, Character character)
	{
		map1.CellBy(x, y).Character = character;
	}
}
