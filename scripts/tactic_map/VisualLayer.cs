using System.Collections.Generic;
using System.Linq;
using Godot;

public class VisualLayer: Node, IMapLayer
{
    private int width;
    private int height;
    public TileMap TileMap => GetNode<TileMap>("TileMap");
    [Signal]
    public delegate void OnDragStart(int x, int y);
    [Signal]
    public delegate void OnDragEnd(int x, int y);
    [Signal]
    public delegate void OnCellClick(int x, int y);
    [Signal]
    public delegate void OnCellHover(int x, int y);

    private (int, int) previousFramePos = (0, 0);
    private (int, int) dragStartFramePos = (0, 0);
    private bool previousFramePressed = false;
    private bool pressedNow = false;
    private bool exitFromStartDrag = false;

    public Vector2 MapPositionToGlobal(int x, int y)
    {
        var basePos = new Vector2(x, y);
        basePos *= TileMap.CellSize;
        basePos += TileMap.CellSize * 0.5f;
        basePos += TileMap.GlobalPosition;
        return basePos;
    }

    public (int, int) GlobalToMapPosition(Vector2 globalPos)
    {
        var tilemap = TileMap;
        globalPos -= tilemap.GlobalPosition;
        var tilePos = globalPos / tilemap.CellSize;
        var x = (int)tilePos.x;
        var y = (int)tilePos.y;
        return (x, y);
    }

    public int GetZ(TacticMap map, MapCell cell, int layer)
    {
        var totalCellCount = map.Width * map.Height;
        var layerOffset = totalCellCount * layer;
        var yOffset = cell.Y * map.Width;
        return layerOffset + yOffset + cell.X;
    }

    private (int, int) GetMousePosition()
    {
        var camera2D = GetNode<Camera2D>("/root/TacticBattle/Camera2D");
        var mousePos = camera2D.GetGlobalMousePosition();
        return GlobalToMapPosition(mousePos);
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseMotion motion) HandleMouseMove(motion);
        if (inputEvent is InputEventMouseButton button) HandleMouseButton(button);
    }

    private void HandleMouseMove(InputEventMouseMotion inputEvent)
    {
        var (currentX, currentY) = GetMousePosition();
        var (lastX, lastY) = previousFramePos;

        EmitSignal(nameof(OnCellHover), currentX, currentY);

        if (pressedNow)
        {
            if (previousFramePressed && !exitFromStartDrag && (currentX != lastX || currentY != lastY))
            {
                EmitSignal(nameof(OnDragStart), lastX, lastY);
                exitFromStartDrag = true;
            }
            previousFramePressed = true; 
        }

        previousFramePos = (currentX, currentY);
    }

    private void HandleMouseButton(InputEventMouseButton inputEvent)
    {
        var mainAction = inputEvent.IsAction("main_action");
        if (!mainAction) return;

        var (currentX, currentY) = GetMousePosition();

        if (inputEvent.Pressed)
        {
            pressedNow = true;
            previousFramePressed = false; 
        }
        else
        {
            var eventName = exitFromStartDrag ? nameof(OnDragEnd) : nameof(OnCellClick);
            EmitSignal(eventName, currentX, currentY);
            pressedNow = false;
            exitFromStartDrag = false;
            previousFramePressed = false;
        }

    }

    private void Visualize(TacticMap map)
    {
        var destMap = TileMap;
        destMap.Clear();

        var tileset = destMap.TileSet;
        var wall = tileset.FindTileByName("wall");
        var floor = tileset.FindTileByName("floor");
        var fall = tileset.FindTileByName("fall");

        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                var isSolid = map.GetSolid(x, y);
                var targetTile = isSolid ? wall : floor;
                if (!map.IsOutOfBounds(x, y-1) && !map.GetSolid(x, y-1) && isSolid) { targetTile = fall; }
                destMap.SetCell(x, y, targetTile);
            }
        }

        destMap.UpdateBitmaskRegion(
            new Vector2(),
            OS.WindowSize
        );
    }

    public void OnSync(TacticMap map)
    {
        Visualize(map);
    }
}