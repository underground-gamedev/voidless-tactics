using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

/*
namespace Battle
{
    public class ManaManipulatorComponent : MonoBehaviour, IManaContainerComponent, IManaPickupComponent, IRoundWatcher
    {
        [SerializeField]
        private int manaPickupCount = 40;

        private Character character;
        private bool pickupUsed = false;

        private ManaType manaType = ManaType.None;
        public ManaType ManaType => manaType;
        private int manaCount = 0;
        public int ManaCount => manaCount;

        public void ConsumeMana(int count)
        {
            this.manaCount -= manaCount;
            if (this.manaCount <= 0)
            {
                this.manaCount = 0;
                manaType = ManaType.None;
            }
        }

        public void AddMana(ManaType newType, int count)
        {
            var expectedManaType = ExpectedType(newType, count);
            var expectedManaValue = ExpectedValue(newType, count);
            this.manaType = expectedManaType;
            this.manaCount = expectedManaValue;
            var manaControl = character.BasicStats.ManaControl.Value;
            if (expectedManaValue > manaControl)
            {
                ManaExplosion(expectedManaValue - manaControl);
            }
        }

        public bool IsTransmute(ManaType type, int count)
        {
            return count > 0 && GetMixResult(manaType, type) != ManaType.None;
        }

        public bool IsSaveType(ManaType type, int count)
        {
            return count == 0 || manaType == type;
        }

        public bool IsReplaceType(ManaType type, int count)
        {
            return count > 0 && !IsTransmute(type, count) && !IsSaveType(type, count);
        }

        public int SafeTransfer(ManaType type, int count)
        {
            var manaControl = character.BasicStats.ManaControl.Value;
            var expectedValue = ExpectedValue(type, count);
            if (expectedValue > manaControl) return count - (expectedValue - manaControl);
            return count;
        }

        private int ExpectedValue(ManaType type, int count)
        {
            if (IsSaveType(type, count)) return manaCount + count;
            if (IsTransmute(type, count)) return (manaCount + count)/2;
            if (IsReplaceType(type, count)) return count;
            return 0;
        }

        private ManaType ExpectedType(ManaType type, int count)
        {
            if (IsSaveType(type, count)) return manaType;
            if (IsTransmute(type, count)) return GetMixResult(manaType, type);
            if (IsReplaceType(type, count)) return type;
            return ManaType.None;
        }

        private void ManaExplosion(int explosionCount)
        {
            ConsumeMana(explosionCount);
            character.TargetComponent?.TakeDamage(explosionCount/5);
        }

        public bool ManaPickupAvailable(EditableMapMono mapMono, MapCell src)
        {
            return ManaPickupAvailable(mapMono) && src.Mana.ManaType != ManaType.None;
        }

        public bool ManaPickupAvailable(EditableMapMono mapMono)
        {
            return !pickupUsed;
        }

        public IEnumerator ManaPickup(EditableMapMono mapMono, MapCell src)
        {
            ManaCell srcManaCell = src.Mana;
            var manaType = srcManaCell.ManaType;

            var manaPickupCountPlanned = manaPickupCount;

            var manaControl = character.BasicStats.ManaControl.Value;
            if (this.manaType == ManaType.None || this.manaType == manaType)
            {
                manaPickupCountPlanned = Mathf.Min(manaPickupCount, manaControl - manaCount);
            }

            var consumeCount = srcManaCell.Consume(manaPickupCountPlanned);

            pickupUsed = true;

            AddMana(manaType, consumeCount);

            //map.ManaLayer.OnSync(map);

            yield break;
        }

        public void OnRoundStart()
        {
            pickupUsed = false;
        }

        public void OnRoundEnd()
        {
        }


        static Dictionary<(ManaType, ManaType), ManaType> mixResults = new Dictionary<(ManaType, ManaType), ManaType>() {
            [(ManaType.Magma, ManaType.Wind)] = ManaType.Fire,
            [(ManaType.Magma, ManaType.Water)] = ManaType.Earth,
            [(ManaType.Water, ManaType.Wind)] = ManaType.Ice,
        };

        private ManaType GetMixResult(ManaType first, ManaType second)
        {
            if (mixResults.ContainsKey((first, second))) return mixResults[(first, second)];
            if (mixResults.ContainsKey((second, first))) return mixResults[(second, first)];
            return ManaType.None;
        }
    }
}
*/