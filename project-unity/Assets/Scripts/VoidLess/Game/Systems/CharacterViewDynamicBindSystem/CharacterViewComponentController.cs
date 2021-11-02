using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.CharacterViewComponent;
using VoidLess.Game.Entities.Characters.Components.ViewTagComponent;
using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.CoordinateConverterLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Setup.SetupSteps.ViewSteps
{
    public class CharacterViewComponentController
    {
        private ViewBindings viewBindings;
        public CharacterViewComponentController(ViewBindings viewBindings)
        {
            this.viewBindings = viewBindings;
        }

        public HandleStatus AddViewToCharacter(IEntity character, ILayeredMap map, MapCell position)
        {
            var coordinateConverter = map.GetLayer<ICoordinateConverterLayer>();
            if (coordinateConverter == null)
            {
                Debug.LogError($"{nameof(CharacterViewDynamicBindSystem)}:: Invalid configuration. Expected coordinate converter layer on map.");
                return HandleStatus.Skipped;
            }
            
            var viewTagCom = character.GetComponent<ViewTagComponent>();
            if (viewTagCom == null) return HandleStatus.Skipped;
            
            var viewTag = viewTagCom.ViewTag;
            var view = viewBindings.CreateView(viewTag);
            if (view == null) return HandleStatus.Skipped;

            character.AddComponent(view);
            var viewDest = coordinateConverter.MapToGlobal(position);
            view.Relocate(viewDest);
            
            return HandleStatus.Handled;
        }

        public HandleStatus RemoveViewFromCharacter(IEntity character)
        {
            var view = character.GetComponent<CharacterViewComponent>();
            if (view == null) return HandleStatus.Skipped;
            character.RemoveComponent<CharacterViewComponent>();
            viewBindings.RemoveView(view);
            return HandleStatus.Handled;
        }
    }
}