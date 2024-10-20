using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{
	/* 
	 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
	 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
	 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
	 * https://en.wikipedia.org/wiki/Median_filter
	 * 
	 * Используйте окно размером 3х3 для не граничных пикселей,
	 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
	 */
/*	public static double[] Sort(double[] pixels)
	{
		return Sort(pixels);
	}*/
    public static double GetMediana(List<double> pixel)
    {
        var i =pixel.Count / 2;
        if (pixel.Count % 2 == 1)
        {
            return pixel[i];
        }
        else
        {
            return (pixel[i] + pixel[i-1])/2;
        }
    }
    public static List<double> Get(double[,] original,int x,int y)
    {
        List<double> list = new List<double>();
        for(int i = x - 1; i <= x + 1; i++)
        {
            for(int j=y-1; j <= y + 1; j++)
            {
                if (i < 0 || i >= original.GetLength(0) || j < 0 || j >= original.GetLength(1))
                    continue;
                list.Add(original[i,j]);
            }
        }
        return list;
    }
	public static double[,] MedianFilter(double[,] original)
	{
        double[,] result = new double[original.GetLength(0),original.GetLength(1)];
        for (int i = 0; i < original.GetLength(0); i++)
        {
            for (int j = 0; j < original.GetLength(1); j++)
            {
                var pixels = Get(original, i, j);
                pixels.Sort();
                result[i,j] =GetMediana(pixels);
            }
        }
		return result;
	}
}
