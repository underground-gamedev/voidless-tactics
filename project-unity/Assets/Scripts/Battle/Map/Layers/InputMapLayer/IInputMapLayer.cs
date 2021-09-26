using System;
using UnityEngine;
using UnityEngine.Events;

namespace Battle.Map.Interfaces
{
    public interface IInputMapLayer: IMapLayer
    {
        event Action<MapCell, Vector2> OnCellClick;
        event Action<MapCell, Vector2> OnCellHover;
    }
}