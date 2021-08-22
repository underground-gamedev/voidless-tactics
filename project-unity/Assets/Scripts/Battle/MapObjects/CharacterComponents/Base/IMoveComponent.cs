using System.Collections;
using System.Collections.Generic;

namespace Battle
{
    public interface IMoveComponent
    {
        bool MoveAvailable();
        List<MapCell> GetMoveArea();
        
        IEnumerator MoveTo(MapCell cell);
    }
}