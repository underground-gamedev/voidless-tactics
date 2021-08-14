using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class SolidMapGenerator : Node
{
    [Export]
    private int generationCicleCount = 7;

    [Export]
    private float fillPrecent = 0.45f;

    [Export]
    private int seed;
    private Random rand;

    public override void _Ready()
    {
        rand = seed == -1 ? new Random() : new Random(seed);
    }

    public void Generate(TacticMap map)
    {
        var solidMap = RawGenerate(map.Width, map.Height);
        foreach (var cell in map)
        {
            cell.Solid = solidMap[cell.X, cell.Y];
        }

    }

    private static void Set(bool[,] map, int x, int y, bool value)
    {
        if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) return;
        map[x, y] = value;
    }

    private static bool Get(bool[,] map, int x, int y)
    {
        if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) return true;
        return map[x, y];
    }

    private void RandomFill(bool[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var roll = rand.NextDouble();
                Set(map, x, y, roll > fillPrecent);
            }
        }
    }

    private bool[,] RawGenerate(int width, int height)
    {
        var map = new bool[width, height];

        RandomFill(map);

        var buff = (bool[,])map.Clone();
        for (int _i = 0; _i < generationCicleCount; _i++)
        {
            CalculateCellularGeneration(buff, map);
            var tmp = buff;
            buff = map;
            map = tmp;
        }

        MakeBorder(map);

        var independAreas = FindAllIndependedAreas(map);
        if (independAreas.Count > 1)
        {
            GD.PrintErr($"Independed areas detected. Regenerate");
            return RawGenerate(width, height);
        }
        
        return map;
    }

    private HashSet<(int, int)> SelectArea(bool[,] map, int startX, int startY)
    {
        var area = new HashSet<(int, int)>();
        var checkedPositions = new HashSet<(int, int)>();
        var uncheckedPositions = new HashSet<(int, int)>();
        uncheckedPositions.Add((startX, startY));
        while (uncheckedPositions.Count > 0)
        {
            var currentPosition = uncheckedPositions.First();
            var (currX, currY) = currentPosition;
            uncheckedPositions.Remove(currentPosition);
            checkedPositions.Add(currentPosition);

            if (Get(map, currX, currY))
            {
                continue;
            }

            area.Add(currentPosition);

            foreach (var neighbour in GetNeighbourFor(currX, currY))
            {
                if (!checkedPositions.Contains(neighbour))
                {
                    uncheckedPositions.Add(neighbour);
                }
            }
        }

        return area;
    }

    private List<(int, int)> GetNeighbourFor(int x, int y)
    {
        var neighbourCells = new (int, int)[] {
            (0, 1), (0, -1), (1, 0), (-1, 0), 
            (-1, -1), (-1, 1), (1, -1), (1, 1),
        };
        return neighbourCells.Select(pos => (pos.Item1 + x, pos.Item2 + y)).ToList();
    }

    private void CalculateCellularGeneration(bool[,] buff, bool[,] active)
    {
        var width = active.GetLength(0);
        var height = active.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var neighbourCount = 0;

                foreach (var (neighX, neighY) in GetNeighbourFor(x, y))
                {
                    if (Get(active, neighX, neighY))
                    {
                        neighbourCount += 1;
                    }
                }

                buff[x, y] = neighbourCount > 4 || neighbourCount <= 0;
            }
        }
    }

    private void MakeBorder(bool[,] map)
    {
        var width = map.GetLength(0);
        var height = map.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            Set(map, x, 0, true);
            Set(map, x, height-1, true);
        }

        for (int y = 0; y < height; y++)
        {
            Set(map, 0, y, true);
            Set(map, width-1, y, true);
        }
    }

    private HashSet<(int, int)> FindAllFloor(bool[,] map)
    {
        var floorPositions = new HashSet<(int, int)>();

        var width = map.GetLength(0);
        var height = map.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!map[x, y]) {
                    floorPositions.Add((x, y));
                }
            }
        }
        return floorPositions;
    }

    private List<HashSet<(int, int)>> FindAllIndependedAreas(bool[,] map)
    {
        var floorPositions = FindAllFloor(map);

        // find all areas
        var independAreas = new List<HashSet<(int, int)>>();
        while (floorPositions.Count > 0)
        {
            var targetPos = floorPositions.First();
            var (targetX, targetY) = targetPos;
            var area = SelectArea(map, targetX, targetY);
            independAreas.Add(area);
            floorPositions.RemoveWhere(x => area.Contains(x));
        }

        return independAreas;
    }
}