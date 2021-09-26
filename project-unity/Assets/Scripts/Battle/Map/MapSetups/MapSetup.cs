using System.Collections.Generic;
using Battle.Map.Interfaces;
using Battle.Map.Layers.PresentationLayers;
using Battle.MapGenerators;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    public class MapSetup : SerializedMonoBehaviour, IMapSetup
    {
        [SerializeField, Required] private MapBuilder mapBuilder;
        [SerializeField, Required] private MapView mapView;
        [OdinSerialize, Required] private List<IMapGeneratorStep> generateSteps;

        public ILayeredMap Setup()
        {
            // Data Layers
            mapBuilder
                .AddLayer<ISolidMapLayer>(new NonSolidMapLayer())
                .AddLayer<ICharacterMapLayer>(new CharacterMapLayer())
                .AddLayer<IManaEditorMapLayer>(new ManaEditorMapLayer())
                .AddLayer<IManaMapLayer, IManaInfoMapLayer>(new ManaMapLayer());
                
            // Logical Layers
            mapBuilder
                .AddLayer<IPathfindMapLayer>(new PathfindLayer());
            
            // Presentation Layers
            mapBuilder
                .AddLayer<ICoordinateConverterLayer>(mapView.CoordinateConverterLayer)
                .AddLayer<IVisualMapLayer>(mapView.VisualPresentation)
                .AddLayer<IInputMapLayer>(mapView.InputController);

            // Generators
            var map = mapBuilder.Build();
            foreach (var step in generateSteps)
            {
                step.Generate(map);
            }

            return map;
        }
    }
}