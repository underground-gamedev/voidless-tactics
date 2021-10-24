using System;
using Battle.Map.Interfaces;
using Battle.Map.Layers.PresentationLayers;
using Core.Components;
using UnityEngine;

namespace Battle.Components.ViewComponent
{
    public class CharacterViewComponent: MonoBehaviour, IComponent, IEntityAttachable
    {
        private IEntity character;
        
        public void OnAttached(IEntity character)
        {
            character.Correspond(Archtype.Character);
            
            this.character = character;
            this.enabled = true;
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