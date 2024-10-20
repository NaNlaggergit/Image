using Avalonia.Controls;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static void SortList(double[,] original, List<double> listPixels)
	{
		foreach(var pixel in original)
		{
			listPixels.Add(pixel);
		}
		listPixels.Sort((a, b) => b.CompareTo(a));
	}
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
		List <double> list = new List <double>();
		double n = original.Length;
		int minCountWhitePixels = (int)(whitePixelsFraction * n);
		double threshold = double.MaxValue;
        SortList(original, list);
		if (minCountWhitePixels <= double.MaxValue && minCountWhitePixels!=0)
			threshold = list[minCountWhitePixels-1];
		for(int i= 0; i < original.GetLength(0); i++)
		{
			for(int j= 0; j < original.GetLength(1); j++)
			{
                if (original[i, j] >= threshold)
                {
                    original[i, j] = 1.0;
                }
                else original[i, j] = 0.0;
            }
		}
		return original;
	}
}
