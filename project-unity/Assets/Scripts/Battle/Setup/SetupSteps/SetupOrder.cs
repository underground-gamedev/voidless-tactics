namespace Battle
{
    public enum SetupOrder
    {
        // Debug Steps
        EventTracer,
        Yield,
        
        // Logical Steps
        TurnSystem,
        MapLogical,
        MapGenerator,
        CharactersLogical,
        TeamsLogical,
        
        // Binding Steps
        BindTeamsWithCharacters,
        BindMapWithCharacters,
        BindRandom,
        
        // View Steps
        MapView,
        CharactersView,
    }
}