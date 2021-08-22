using System;
using System.Collections.Generic;

public static class AreaSelectorHelpers
{
    private static void Swap(ref int a, ref int b)
    {
        int c = a;
        a = b;
        b = c;
    }

    private static List<(int, int)> GetLine(int x0, int y0, int x1, int y1)
    {
        var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

        if (steep)
        {
            Swap(ref x0, ref x1);
            Swap(ref y0, ref y1);
        }

        if (x0 > x1)
        {
            Swap(ref x0, ref x1);
            Swap(ref y0, ref y1);
        }

        var deltaX = Math.Abs(x1 - x0);
        var deltaY = Math.Abs(y1 - y0);
        var positions = new List<(int, int)>();
        var error = deltaX / 2;
        var y = y0;
        var yStep = y0 < y1 ? 1 : -1;

        for (var x = x0; x <= x1; x++)
        {
            var newPos = steep ? (y, x) : (x, y);
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

    public static List<(int, int)> GetCircle(int cx, int cy, int radius)
    {
        var d = 3 - 2 * radius;
        var (x, y) = (radius, 0);

        var positions = new List<(int, int)>();
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