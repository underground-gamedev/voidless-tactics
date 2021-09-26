using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle
{
    public class MapHolder : MonoBehaviour, ILayeredMapHolder
    {
        public ILayeredMap Map { get; set; }
    }
}