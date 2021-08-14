using System.Threading.Tasks;
using Godot;

public static class MapEffects
{
    private static PackedScene mapTextPopup;
    public static async Task PopupText(this TacticMap map, MapCell cell, string message, Color color)
    {
        mapTextPopup = mapTextPopup ?? GD.Load<PackedScene>("res://prefabs/ui/MapTextPopup.tscn");
        var popup = (MapTextPopup)mapTextPopup.Instance();
        var visualLayer = map.VisualLayer;
        var globalPosition = visualLayer.MapPositionToGlobal(cell.X, cell.Y);
        popup.GlobalPosition = globalPosition;
        popup.ZIndex = visualLayer.GetZ(map, cell, 1);

        map.AddChild(popup);
        await popup.Animate(message, color);
        map.RemoveChild(popup);
        popup.QueueFree();
    }
}