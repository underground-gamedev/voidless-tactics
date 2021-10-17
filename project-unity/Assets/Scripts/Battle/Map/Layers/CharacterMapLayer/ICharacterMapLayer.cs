using System;

namespace Battle.Map.Interfaces
{
    public interface ICharacterMapLayer: IMapLayer
    {
        event Action<ICharacter, MapCell> OnCharacterAdded;
        event Action<ICharacter> OnCharacterRemoved;
        event Action<ICharacter, MapCell> OnCharacterRelocated;
        
        void AddCharacter(ICharacter character, MapCell cell);
        void RelocateCharacter(ICharacter character, MapCell cell);
        void RemoveCharacter(ICharacter character);
        
        ICharacter GetCharacter(MapCell cell);
        MapCell? GetPosition(ICharacter character);
        ICharacter[] GetAllCharacters();
    }
}