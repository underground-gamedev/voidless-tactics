using System;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public struct ManaInfo
    {
        private ManaType type;
        private int count;
        
        public ManaType Type
        {
            get => count == 0 ? ManaType.None : type;
            set => type = value;
        }
        
        public int Count
        {
            get => type == ManaType.None ? 0 : count;
            set => count = Mathf.Clamp(value, 0, int.MaxValue);
        }

        public ManaInfo(ManaType type, int count)
        {
            this.type = type;
            this.count = Mathf.Clamp(count, 0, int.MaxValue);
        }
    }
}