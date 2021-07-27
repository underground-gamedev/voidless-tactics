using System.Threading.Tasks;
using Godot;
public class BaseControllerState: BaseState
{
    public virtual bool CellClick(int x, int y) => false;

    public virtual bool MenuActionSelected(string action) => false;
}