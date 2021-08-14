using System.Collections.Generic;
using System.Linq;
using Godot;

public enum MoveHighlightType
{
    None,
    NormalMove,
    LongMove,
    Attack,
    Active,
}

public class MoveHighlightLayer : Node, IMapLayer
{
    private TileMap TileMap => GetNode<TileMap>("TileMap");

    public void Clear()
    {
        TileMap.Clear();
    }

    public void Highlight(int x, int y, MoveHighlightType type)
    {
        var highlightMovement = TileMap;

        if (type == MoveHighlightType.None)
        {
            highlightMovement.SetCell(x, y, -1);
            return;
        }

        var highlightTile = highlightMovement.TileSet.FindTileByName(type.ToString());
        highlightMovement.SetCell(x, y, highlightTile);
    }

    public void OnSync(TacticMap map)
    {
    }

    public override void _Ready()
    {
        Clear();
    }
}