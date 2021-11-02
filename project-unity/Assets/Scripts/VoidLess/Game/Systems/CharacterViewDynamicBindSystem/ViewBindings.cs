using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VoidLess.Game.Entities.Characters.Base;
using VoidLess.Game.Entities.Characters.Components.CharacterViewComponent;

namespace VoidLess.Game.Setup.SetupSteps.ViewSteps
{
    public class ViewBindings
    {
        [OdinSerialize, Required]
        private Dictionary<CharacterViewTag, CharacterViewComponent> viewBindings;
        public ViewBindings(Dictionary<CharacterViewTag, CharacterViewComponent> viewBindings)
        {
            this.viewBindings = viewBindings;
        }

        public CharacterViewComponent CreateView(CharacterViewTag viewTag)
        {
            if (!viewBindings.TryGetValue(viewTag, out var viewAsset))
            {
                Debug.LogError($"{nameof(ViewBindings)}:: Invalid configuration. Not found view component for {viewTag}");
                return null;
            }

            return Object.Instantiate(viewAsset);
        }

        public void RemoveView(CharacterViewComponent view)
        {
            Object.Destroy(view);
        }
    }
}