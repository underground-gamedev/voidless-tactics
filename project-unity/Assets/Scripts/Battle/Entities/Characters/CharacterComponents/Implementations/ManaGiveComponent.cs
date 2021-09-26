using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
namespace Battle
{
    [RequireComponent(typeof(IManaContainerComponent))]
    public class ManaGiveComponent : MonoBehaviour, IManaGiveComponent
    {
        private Character character;
        private IManaContainerComponent manaContainer;

        private void Start()
        {
            manaContainer = GetComponent<IManaContainerComponent>();
        }

        public List<MapCell> GetGiveManaArea(EditableMapMono mapMono, MapCell src)
        {
            return mapMono.AllNeighboursFor(src.X, src.Y);
        }

        public IEnumerator GiveMana(EditableMapMono mapMono, Character targetChar)
        {
            var expectedTransfer = ExpectedTransfer(targetChar);
            var manaType = manaContainer.ManaType;

            manaContainer.ConsumeMana(manaContainer.ManaCount);
            targetChar.ManaContainerComponent.AddMana(manaType, expectedTransfer);

            yield break;
        }

        public bool GiveManaAvailable(EditableMapMono mapMono)
        {
            return manaContainer.ManaCount > 0;
        }

        public bool GiveManaAvailable(EditableMapMono mapMono, MapCell target)
        {
            if (!GiveManaAvailable(mapMono)) return false;
            var targetChar = target?.MapObject?.AsCharacter;
            if (targetChar == null) return false;
            return ExpectedTransfer(targetChar) > 0;
        }

        private int ExpectedTransfer(Character targetChar)
        {
            var targetManaContainer = targetChar.ManaContainerComponent;
            if (targetManaContainer == null) return 0;

            var manaType = manaContainer.ManaType;
            var manaCount = manaContainer.ManaCount;

            if (character.Team.IsAlly(targetChar))
            {
                if (targetManaContainer.IsSaveType(manaType, manaCount) 
                || targetManaContainer.IsTransmute(manaType, manaCount)
                || targetManaContainer.ManaType == ManaType.None) {
                    return targetManaContainer.SafeTransfer(manaType, manaCount);
                }
                return 0;
            }

            return manaCount;
        }
    }
}
*/