using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMoveComponent
{
    bool MoveAvailable();
    List<MoveCell> GetMoveArea();
    
    Task MoveTo(MapCell cell);
}