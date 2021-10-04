namespace Battle
{
    public enum SetupOrder
    {
        // Debug Steps
        EventTracer,
        
        // Logical Steps
        MapLogical,
        MapGenerator,
        CharactersLogical,
        TeamsLogical,
        
        // Binding Steps
        BindTeamsWithCharacters,
        BindMapWithCharacters,
        
        // View Steps
        MapView,
        CharactersView,
    }
}