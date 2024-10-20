using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] Transpose(double[,] sx)
    {
        double[,] sy = new double[sx.GetLength(1), sx.GetLength(0)];
        for (int i = 0; i < sx.GetLength(0); i++)
        {
            for (int j = 0; j < sx.GetLength(1); j++)
            {
                sy[i, j] = sx[j, i];
            }
        }
        return sy;
    }
    private static double GetConvolution(double[,] original, double[,] s, int x, int y, int offset)
    {
        var width = s.GetLength(0);
        var height = s.GetLength(1);
        var result = 0.0;

        for (var sx = 0; sx < width; sx++)
            for (var sy = 0; sy < height; sy++)
                result += s[sx, sy] * original[x + sx - offset, y + sy - offset];

        return result;
    }

    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];
        var offsetX = sx.GetLength(0) / 2;
        var offsetY = sx.GetLength(1) / 2;
        var sy = Transpose(sx);
        for (var x = offsetX; x < width - offsetX; x++)
        {
            for (var y = offsetY; y < height - offsetY; y++)
            {
                var gx = GetConvolution(g, sx, x, y, offsetX);
                var gy = GetConvolution(g, sy, x, y, offsetY);

                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        }
        return result;
    }
}