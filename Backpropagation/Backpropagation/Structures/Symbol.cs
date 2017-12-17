using System;
using System.Collections.Generic;

namespace Backpropagation.Structures
{
	public class Symbol
	{
		public double[] XPositions;
		public double[] YPositions;
		public int[] Class;

		public Symbol(List<double> xPositions, List<double> yPositions, List<int> classes)
		{
			if (xPositions.Count != yPositions.Count)
				throw new Exception("Both list have to have the same number of elements.");

			var size = xPositions.Count;
			XPositions = new double[size];
			YPositions = new double[size];
			
			for (int i = 0; i < size; i++)
			{
				XPositions[i] = xPositions[i];
				YPositions[i] = yPositions[i];
			}

			size = classes.Count;
			Class = new int[size];
			for (var i = 0; i < size; i++)
			{
				Class[i] = classes[i];
			}
		}

		public Symbol(double[] xPositions, double[] yPositions, int[] classes)
		{
			if (xPositions.Length != yPositions.Length)
				throw new Exception("Both list have to have the same number of elements.");
			XPositions = xPositions;
			YPositions = yPositions;
			Class = classes;
		}

	}
}