using System;
using Battle.Map.Interfaces;
using Core.Components;

namespace Battle
{
    [Require(typeof(IManaEditorMapLayer))]
    public class ManaMapLayer: IManaMapLayer
    {
        public event Action<MapCell, ManaInfo> OnManaChanged;
        
        private ILayeredMap map;
        private IManaEditorMapLayer manaEditor;
        
        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
            manaEditor = map.GetLayer<IManaEditorMapLayer>();
            manaEditor.OnManaChanged += OnEditorManaChanged;
        }

        public void OnDeAttached()
        {
            manaEditor.OnManaChanged -= OnEditorManaChanged;
            manaEditor = null;
            map = null;
        }

        public ManaInfo GetMana(MapCell cell)
        {
            return manaEditor.GetMana(cell);
        }

        public ManaInfo Consume(MapCell cell, int count)
        {
            if (count <= 0) return new ManaInfo();
            
            var mana = GetMana(cell);
            
            if (mana.Type == ManaType.None)
            {
                return new ManaInfo();
            }

            var consumeResult = new ManaInfo(mana.Type, mana.Count >= count ? count : mana.Count);
            mana.Count -= consumeResult.Count;
            
            manaEditor.SetMana(cell, consumeResult);

            return consumeResult;
        }

        private void OnEditorManaChanged(MapCell cell, ManaInfo info)
        {
            OnManaChanged?.Invoke(cell, info);
        }
    }
}