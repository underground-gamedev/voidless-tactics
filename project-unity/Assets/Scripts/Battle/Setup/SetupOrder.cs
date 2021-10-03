namespace Battle
{
    public enum SetupOrder
    {
        EventTracer,
        MapLogical,
        MapGenerator,
        CharactersLogical,
        TeamsLogical,
        BindTeamsWithCharacters,
        BindMapWithCharacters,
        MapView,
        CharactersView,
    }
}