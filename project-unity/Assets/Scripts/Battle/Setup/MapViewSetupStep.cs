using Battle.Map.Interfaces;
using Battle.Map.Layers.PresentationLayers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "MapViewSetupStep.asset", menuName = "CUSTOM/Setups/MapViewSetupStep", order = 1)]
    public class MapViewSetupStep : SerializableSetupStep
    {
        [OdinSerialize, AssetsOnly, Required]
        private MapView viewAsset;

        public override void Setup(BattleState state)
        {
            if (viewAsset == null)
            {
                Debug.LogError($"{nameof(MapViewSetupStep)}:: invalid configuration. View is null");
                return;
            }
            
            var map = state.Map.Map;
            
            if (map == null)
            {
                Debug.LogError($"{nameof(MapViewSetupStep)}:: invalid configuration. Map is null");
                return;
            }

            var view = Instantiate(viewAsset);
            
            map.AddLayer<ICoordinateConverterLayer>(view.CoordinateConverterLayer);
            map.AddLayer<IVisualMapLayer>(view.VisualPresentation);
            map.AddLayer<IInputMapLayer>(view.InputController);
        }

        protected override SetupOrder SetupOrder => SetupOrder.MapView;
    }
}