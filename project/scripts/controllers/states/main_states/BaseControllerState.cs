using System.Threading.Tasks;
using Godot;
public class BaseControllerState: BaseState
{
    public virtual bool CellClick(int x, int y, Vector2 offset) => false;
    public virtual bool DragStart(int x, int y, Vector2 offset) => false;
    public virtual bool DragEnd(int x, int y, Vector2 offset) => false;

    public virtual bool MenuActionSelected(string action) => false;
}