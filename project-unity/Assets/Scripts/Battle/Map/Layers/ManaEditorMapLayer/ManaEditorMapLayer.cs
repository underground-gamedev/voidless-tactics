using System;
using Battle.Map.Extensions;
using Battle.Map.Interfaces;

namespace Battle
{
    public class ManaEditorMapLayer: IManaEditorMapLayer
    {
        private ILayeredMap map;
        private ManaInfo[,] mana;
        
        public event Action<MapCell, ManaInfo> OnManaChanged;
        
        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
            mana = new ManaInfo[map.Width, map.Height];
        }

        public void OnDeAttached()
        {
            this.map = null;
            this.mana = null;
        }
        
        public ManaInfo GetMana(MapCell cell)
        {
            map.CheckOutOfBounds(cell);
            return mana[cell.X, cell.Y];
        }

        public void SetMana(MapCell cell, ManaInfo manaInfo)
        {
            map.CheckOutOfBounds(cell);
            mana[cell.X, cell.Y] = manaInfo;
            OnManaChanged?.Invoke(cell, manaInfo);
        }
    }
}