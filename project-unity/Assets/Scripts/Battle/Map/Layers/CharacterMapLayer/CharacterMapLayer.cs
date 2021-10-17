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
        public event Action<ICharacter, MapCell> OnCharacterAdded;
        public event Action<ICharacter> OnCharacterRemoved;
        public event Action<ICharacter, MapCell> OnCharacterRelocated;
        
        private ILayeredMap map;
        private Dictionary<MapCell, ICharacter> posToChars;
        private Dictionary<ICharacter, MapCell> charsToPos;
        
        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
            posToChars = new Dictionary<MapCell, ICharacter>();
            charsToPos = new Dictionary<ICharacter, MapCell>();
        }

        public void OnDeAttached()
        {
            this.map = null;
            posToChars = null;
            charsToPos = null;
        }

        public void AddCharacter(ICharacter character, MapCell cell)
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

            character.AddComponent<MapBindingComponent>(new MapBindingComponent(map));

            OnCharacterAdded?.Invoke(character, cell);
        }

        public void RelocateCharacter(ICharacter character, MapCell cell)
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

        public void RemoveCharacter(ICharacter character)
        {
            if (!charsToPos.ContainsKey(character)) return;
            posToChars.Remove(charsToPos[character]);
            charsToPos.Remove(character);
            
            character.RemoveComponent<MapBindingComponent>();

            OnCharacterRemoved?.Invoke(character);
        }

        public ICharacter GetCharacter(MapCell cell)
        {
            return posToChars.TryGetValue(cell, out var character) ? character : null;
        }

        public MapCell? GetPosition(ICharacter character)
        {
            return charsToPos.TryGetValue(character, out var cell) ? new MapCell?(cell) : null;
        }

        public ICharacter[] GetAllCharacters()
        {
            return charsToPos.Keys.ToArray();
        }
    }
}