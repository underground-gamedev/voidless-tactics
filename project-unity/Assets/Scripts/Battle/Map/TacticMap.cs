using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Battle
{
    public class TacticMap : MonoBehaviour, IEnumerable<MapCell>
    {
        [SerializeField]
        private int width = 30;
        [SerializeField]
        private int height = 20;

        private PathfindLayer pathfindLayer;
        public PathfindLayer PathfindLayer => pathfindLayer ??= GetComponent<PathfindLayer>();

        private MapCell[,] cells;
        public int Width { get => width; }
        public int Height { get => height; }
        public int TileCount { get => width * height; }

        public TacticMap()
        {
            InitCells();
        }

        public TacticMap(int width, int height)
        {
            this.width = width;
            this.height = height;
            InitCells();
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

        public HashSet<MapCell> FindAllFloor()
        {
            var floorPositions = new HashSet<MapCell>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!CellBy(x, y).Solid)
                    {
                        floorPositions.Add(CellBy(x, y));
                    }
                }
            }
            return floorPositions;
        }
    }
}
