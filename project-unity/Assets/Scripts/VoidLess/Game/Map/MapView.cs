using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VoidLess.Game.Map.Layers.CoordinateConverterLayer;
using VoidLess.Game.Map.Layers.HighlightAreaLayer;
using VoidLess.Game.Map.Layers.InputEmitterLayer;
using VoidLess.Game.Map.Layers.VisualMapLayer;

namespace VoidLess.Game.Map
{
    public class MapView : SerializedMonoBehaviour
    {
        [OdinSerialize, Required]
        private IVisualMapLayer visualPresentation;
        [OdinSerialize, Required]
        private InputEmitterLayer inputEmitterLayer;
        [OdinSerialize, Required]
        private ICoordinateConverterLayer coordinateConverter;
        [OdinSerialize, Required]
        private HighlightAreaLayer highlightAreaLayer;
        
        public IVisualMapLayer VisualPresentation => visualPresentation;
        public InputEmitterLayer InputEmitterLayer => inputEmitterLayer;
        public ICoordinateConverterLayer CoordinateConverterLayer => coordinateConverter;
        public HighlightAreaLayer HighlightAreaLayer => highlightAreaLayer;
    }
}