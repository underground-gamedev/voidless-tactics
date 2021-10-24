using System;

namespace Battle.Map.Interfaces
{
    public interface ICharacterMapLayer: IMapLayer
    {
        event Action<IEntity, MapCell> OnCharacterAdded;
        event Action<IEntity> OnCharacterRemoved;
        event Action<IEntity, MapCell> OnCharacterRelocated;
        
        void AddCharacter(IEntity character, MapCell cell);
        void RelocateCharacter(IEntity character, MapCell cell);
        void RemoveCharacter(IEntity character);
        
        IEntity GetCharacter(MapCell cell);
        MapCell? GetPosition(IEntity character);
        IEntity[] GetAllCharacters();
    }
}