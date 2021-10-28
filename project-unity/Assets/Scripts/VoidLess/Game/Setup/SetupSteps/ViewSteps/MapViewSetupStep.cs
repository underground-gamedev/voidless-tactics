using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VoidLess.Game.Map;
using VoidLess.Game.Map.Layers.CoordinateConverterLayer;
using VoidLess.Game.Map.Layers.HighlightAreaLayer;
using VoidLess.Game.Map.Layers.InputEmitterLayer;
using VoidLess.Game.Map.Layers.VisualMapLayer;

namespace VoidLess.Game.Setup.SetupSteps.ViewSteps
{
    [CreateAssetMenu(fileName = "MapViewSetupStep.asset", menuName = "CUSTOM/Setups/MapViewSetupStep", order = (int)SetupOrder.MapView)]
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
            map.AddLayer<InputEmitterLayer>(view.InputEmitterLayer);
            map.AddLayer<HighlightAreaLayer>(view.HighlightAreaLayer);
        }

        protected override SetupOrder SetupOrder => SetupOrder.MapView;
    }
}