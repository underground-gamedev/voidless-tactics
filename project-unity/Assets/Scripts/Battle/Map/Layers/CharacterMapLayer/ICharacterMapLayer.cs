using System;

namespace Battle.Map.Interfaces
{
    public interface ICharacterMapLayer: IMapLayer
    {
        public event Action<ICharacter, MapCell> OnCharacterAdded;
        public event Action<ICharacter> OnCharacterRemoved;
        public event Action<ICharacter, MapCell> OnCharacterRelocated;
        
        void AddCharacter(ICharacter character, MapCell cell);
        void RelocateCharacter(ICharacter character, MapCell cell);
        void RemoveCharacter(ICharacter character);
        
        ICharacter GetCharacter(MapCell cell);
        MapCell? GetPosition(ICharacter character);
        ICharacter[] GetAllCharacters();
    }
}