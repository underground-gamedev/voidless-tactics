using System.Threading.Tasks;
using Godot;

public static class MapEffects
{
    private static PackedScene mapTextPopup;
    public static async Task PopupText(this TacticMap map, MapCell cell, string message, Color color)
    {
        mapTextPopup = mapTextPopup ?? GD.Load<PackedScene>("res://prefabs/gameobjects/MapTextPopup.tscn");
        var popup = (MapTextPopup)mapTextPopup.Instance();
        var visualLayer = map.VisualLayer;
        var globalPosition = visualLayer.MapPositionToGlobal(cell.X, cell.Y);
        GD.Print(globalPosition);
        popup.GlobalPosition = globalPosition;

        map.AddChild(popup);
        await popup.Animate(message, color);
        map.RemoveChild(popup);
        popup.QueueFree();
    }
}