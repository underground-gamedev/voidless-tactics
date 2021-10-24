using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Components.MapBindingComponent;
using Battle.Map.Extensions;
using Battle.Map.Interfaces;

namespace Battle
{
    public class CharacterMapLayer : ICharacterMapLayer
    {
        public event Action<IEntity, MapCell> OnCharacterAdded;
        public event Action<IEntity> OnCharacterRemoved;
        public event Action<IEntity, MapCell> OnCharacterRelocated;
        
        private ILayeredMap map;
        private Dictionary<MapCell, IEntity> posToChars;
        private Dictionary<IEntity, MapCell> charsToPos;
        
        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
            posToChars = new Dictionary<MapCell, IEntity>();
            charsToPos = new Dictionary<IEntity, MapCell>();
        }

        public void OnDeAttached()
        {
            this.map = null;
            posToChars = null;
            charsToPos = null;
        }

        public void AddCharacter(IEntity character, MapCell cell)
        {
            map.CheckOutOfBounds(cell);
            
            if (charsToPos.ContainsKey(character))
            {
                throw new InvalidOperationException("Character already on map");
            }

            if (posToChars.ContainsKey(cell))
            {
                throw new InvalidOperationException("Character on the same position already exists");
            }
            
            charsToPos.Add(character, cell);
            posToChars.Add(cell, character);

            character.AddComponent(new MapBindingComponent(map));

            OnCharacterAdded?.Invoke(character, cell);
        }

        public void RelocateCharacter(IEntity character, MapCell cell)
        {
            map.CheckOutOfBounds(cell);
            
            if (!charsToPos.ContainsKey(character))
            {
                throw new InvalidOperationException("Character not presented on map");
            }

            if (posToChars.ContainsKey(cell))
            {
                if (posToChars[cell] == character) return;
                throw new InvalidOperationException("Character on the same position already exists");
            }

            var oldCell = charsToPos[character];

            charsToPos[character] = cell;
            posToChars.Remove(oldCell);
            posToChars.Add(cell, character);

            OnCharacterRelocated?.Invoke(character, cell);
        }

        public void RemoveCharacter(IEntity character)
        {
            if (!charsToPos.ContainsKey(character)) return;
            posToChars.Remove(charsToPos[character]);
            charsToPos.Remove(character);
            
            character.RemoveComponent<MapBindingComponent>();

            OnCharacterRemoved?.Invoke(character);
        }

        public IEntity GetCharacter(MapCell cell)
        {
            return posToChars.TryGetValue(cell, out var character) ? character : null;
        }

        public MapCell? GetPosition(IEntity character)
        {
            return charsToPos.TryGetValue(character, out var cell) ? new MapCell?(cell) : null;
        }

        public IEntity[] GetAllCharacters()
        {
            return charsToPos.Keys.ToArray();
        }
    }
}