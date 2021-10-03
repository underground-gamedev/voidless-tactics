using System.Collections.Generic;

namespace Battle.Map.Interfaces
{
    public interface ICharacterMapLayer: IMapLayer
    {
        void AddCharacter(ICharacter character, MapCell cell);
        void RelocateCharacter(ICharacter character, MapCell cell);
        void RemoveCharacter(ICharacter character);
        
        ICharacter GetCharacter(MapCell cell);
        MapCell? GetPosition(ICharacter character);
        ICharacter[] GetAllCharacters();
    }
}