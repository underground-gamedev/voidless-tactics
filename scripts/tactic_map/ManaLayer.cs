using System.Collections.Generic;
using System.Linq;
using Godot;

public class ManaLayer: Node, IMapLayer
{
    private int width;
    private int height;
    public TileMap TileMap => GetNode<TileMap>("TileMap");
    private void Visualize(TacticMap map)
    {
        var destMap = TileMap;
        destMap.Clear();

        var tileset = destMap.TileSet;
        var natureMana = tileset.FindTileByName("nature");
        var fireMana = tileset.FindTileByName("fire");
        var waterMana = tileset.FindTileByName("water");

        var manaTiles = new Dictionary<ManaType, int>() {
            [ManaType.Nature] = natureMana,
            [ManaType.Magma] = fireMana,
            [ManaType.Water] = waterMana,
            [ManaType.Wind] = -1,
            [ManaType.None] = -1,
        };

        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                var mana = map.CellBy(x, y).Mana;
                var manaTile = manaTiles[mana.ManaType];
                destMap.SetCell(x, y, manaTile);
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