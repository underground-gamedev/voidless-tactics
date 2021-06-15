using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class TacticMap: Node, IEnumerable<MapCell>
{
    public int Width { get => width; }
    public int Height { get => height; }

    public int TileCount { get => width * height; }

    [Signal]
    public delegate void OnSync(TacticMap map);

    [Export]
    private int width = 30;
    [Export]
    private int height = 20;

    public PathfindLayer PathfindLayer => GetNode<PathfindLayer>("PathfindLayer");
    public VisualLayer VisualLayer => GetNode<VisualLayer>("VisualLayer");
    public MoveHighlightLayer MoveHighlightLayer => GetNode<MoveHighlightLayer>("MoveHighlightLayer");

    public TacticMap()
    {
        InitCells();
    }

    public override void _Ready()
    {
        var mapLayers = this.GetChilds<IMapLayer>(".");
        foreach (var layer in mapLayers)
        {
            Connect(nameof(OnSync), (Godot.Object) layer, nameof(IMapLayer.OnSync));
        }
    }

    public TacticMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        InitCells();
    }

    public void Sync()
    {
        EmitSignal(nameof(OnSync), this);
    }

    private void InitCells()
    {
        cells = new MapCell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new MapCell(x, y);
            }
        }
    }

    private MapCell[,] cells;

    public MapCell CellBy(int x, int y)
    {
        return cells[x, y];
    }

    public bool IsOutOfBounds(int x, int y)
    {
        return x < 0 || x >= width || y < 0 || y >= height;
    }

    public List<MapCell> DirectNeighboursFor(int x, int y)
    {
        return NeighboursByDirections(x, y, new List<(int, int)>() {
            (0, -1), (-1, 0), (1, 0), (0, 1),
        });
    }

    public List<MapCell> DiagonalNeighboursFor(int x, int y)
    {
        return NeighboursByDirections(x, y, new List<(int, int)>() {
            (-1, -1), (1, -1), (-1, 1),  (1, 1),
        });
    }

    public List<MapCell> AllNeighboursFor(int x, int y)
    {
        return NeighboursByDirections(x, y, new List<(int, int)>() {
            (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1),
        });
    }

    private List<MapCell> NeighboursByDirections(int x, int y, List<(int, int)> directions)
    {
        var neigh = new List<MapCell>();
        foreach (var (dirX, dirY) in directions)
        {
            var neighX = x + dirX;
            var neighY = y + dirY;

            if (IsOutOfBounds(neighX, neighY)) continue;
            neigh.Add(CellBy(neighX, neighY));
        }
        return neigh;
    }

    public IEnumerator<MapCell> GetEnumerator()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                yield return CellBy(x, y);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
