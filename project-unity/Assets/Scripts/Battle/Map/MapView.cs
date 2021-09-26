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
        private IInputMapLayer inputController;
        [OdinSerialize, Required]
        private ICoordinateConverterLayer coordinateConverter;
        
        public IVisualMapLayer VisualPresentation => visualPresentation;
        public IInputMapLayer InputController => inputController;
        public ICoordinateConverterLayer CoordinateConverterLayer => coordinateConverter;
    }
}