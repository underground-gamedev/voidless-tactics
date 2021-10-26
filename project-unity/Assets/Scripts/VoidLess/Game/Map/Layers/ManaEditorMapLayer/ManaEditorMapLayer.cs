using System;
using VoidLess.Game.Magic.Mana;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.ManaEditorMapLayer
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