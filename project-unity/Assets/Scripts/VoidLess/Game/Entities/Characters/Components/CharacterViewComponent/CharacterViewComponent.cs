using UnityEngine;
using VoidLess.Core.Components;
using VoidLess.Core.Entities;

namespace VoidLess.Game.Entities.Characters.Components.CharacterViewComponent
{
    public class CharacterViewComponent: MonoBehaviour, IComponent, IEntityAttachable
    {
        private IEntity character;
        
        public void OnAttached(IEntity character)
        {
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