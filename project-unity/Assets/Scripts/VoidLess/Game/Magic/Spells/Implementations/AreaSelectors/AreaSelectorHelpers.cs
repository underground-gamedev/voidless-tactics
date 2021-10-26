using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoidLess.Game.Magic.Spells.Implementations.AreaSelectors
{
    public static class AreaSelectorHelpers
    {
        private static List<Vector2Int> GetLine(int x0, int y0, int x1, int y1)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }

            if (x0 > x1)
            {
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }

            var deltaX = Math.Abs(x1 - x0);
            var deltaY = Math.Abs(y1 - y0);
            var positions = new List<Vector2Int>();
            var error = deltaX / 2;
            var y = y0;
            var yStep = y0 < y1 ? 1 : -1;

            for (var x = x0; x <= x1; x++)
            {
                var newPos = steep ? new Vector2Int(y, x) : new Vector2Int(x, y);
                positions.Add(newPos);

                error -= deltaY;
                if (error < 0)
                {
                    y += yStep;
                    error += deltaX;
                }
            }

            return positions;
        }

        public static List<Vector2Int> GetCircle(int cx, int cy, int radius)
        {
            var d = 3 - 2 * radius;
            var (x, y) = (radius, 0);

            var positions = new List<Vector2Int>();
            while (y <= x)
            {
                positions.AddRange(GetLine(cx - x, cy - y, cx + x, cy - y));
                positions.AddRange(GetLine(cx - x, cy + y, cx + x, cy + y));
                positions.AddRange(GetLine(cx - y, cy + x, cx + y, cy + x));
                positions.AddRange(GetLine(cx - y, cy - x, cx + y, cy - x));

                if (d <= 0)
                {
                    d += 4 * y + 6;
                    y += 1;
                }
                else
                {
                    d += 4 * (y - x) + 10;
                    x -= 1;
                    y += 1;
                }
            }

            return positions;
        }
    }
}