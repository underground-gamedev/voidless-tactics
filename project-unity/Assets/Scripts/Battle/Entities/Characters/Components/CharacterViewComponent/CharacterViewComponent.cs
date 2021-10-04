using System;
using Battle.Map.Interfaces;
using Battle.Map.Layers.PresentationLayers;
using Core.Components;
using UnityEngine;

namespace Battle.Components.ViewComponent
{
    public class CharacterViewComponent: MonoBehaviour, IComponent, ICharacterAttachable, IEntityAttachable
    {
        private ICharacter character;
        
        public void OnAttached(ICharacter character)
        {
            this.character = character;
            this.enabled = true;
        }

        public void OnAttached(IEntity entity)
        {
            if (!(entity is ICharacter))
            {
                throw new InvalidOperationException(
                    $"{nameof(CharacterViewComponent)}:: Not supported custom entity type. Expected ICharacter");
            }
        }

        public void OnDeAttached()
        {
            this.character = null;
            this.enabled = false;
        }

        public void Relocate(Vector3 position)
        {
            transform.position = position;
        }
    }
}