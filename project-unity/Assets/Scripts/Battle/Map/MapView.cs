using Battle.Map.Interfaces;
using Battle.Map.Layers.PresentationLayers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Battle
{
    public class MapView : SerializedMonoBehaviour
    {
        [OdinSerialize, Required]
        private IVisualMapLayer visualPresentation;
        [OdinSerialize, Required]
        private InputEmitterLayer inputEmitterLayer;
        [OdinSerialize, Required]
        private ICoordinateConverterLayer coordinateConverter;
        
        public IVisualMapLayer VisualPresentation => visualPresentation;
        public InputEmitterLayer InputEmitterLayer => inputEmitterLayer;
        public ICoordinateConverterLayer CoordinateConverterLayer => coordinateConverter;
    }
}