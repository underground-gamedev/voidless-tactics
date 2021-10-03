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
            var mapBindingCom = character.GetComponent<MapBindingComponent.MapBindingComponent>();
            
            if (mapBindingCom?.Map == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(CharacterViewComponent)}:: Invalid configuration. Expected map binding component");
            }

            var map = mapBindingCom.Map;

            var characterLayer = map.GetLayer<ICharacterMapLayer>();
            var coordinateConverterLayer = map.GetLayer<ICoordinateConverterLayer>();

            var pos = characterLayer.GetPosition(character);
            
            if (!pos.HasValue)
            {
                throw new InvalidOperationException(
                    $"{nameof(CharacterViewComponent)}:: Invalid configuration. Expected position in map binding");
            }

            var globalPos = coordinateConverterLayer.MapToGlobal(pos.Value);
            transform.position = globalPos;
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
    }
}