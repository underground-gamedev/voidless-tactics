namespace VoidLess.Game.Setup.SetupSteps
{
    public enum SetupOrder
    {
        // Debug Steps
        EventTracer,
        Yield,
        DebugControl,
        
        // Game System Steps
        TurnSystem,
        MoveSystem,
        
        // Logical Steps
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