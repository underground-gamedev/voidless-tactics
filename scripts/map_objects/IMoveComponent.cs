using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMoveComponent
{
    bool MoveAvailable();
    List<MoveCell> GetMoveAvailableCells();
    
    Task MoveTo(MapCell cell);
}