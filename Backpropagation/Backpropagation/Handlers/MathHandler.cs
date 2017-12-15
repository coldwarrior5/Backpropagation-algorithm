using System;
using System.Collections.Generic;

namespace Backpropagation.Handlers
{
	public static class MathHandler
	{
		public static double ArithmeticMean(List<double> values)
		{
			double mean = 0;
			foreach (double t in values)
				mean += t;
			return mean / values.Count;
		}

		public static double AbsMax(List<double> values)
		{
			double max = 0;
			foreach (double t in values)
			{
				double temp = Math.Abs(t);
				if (temp > max)
					max = temp;
			}
			return max;
		}

		public static int Clamp(int value, int min, int max)
		{
			return value < min ? min : value > max ? max : value;
		}
	}
}