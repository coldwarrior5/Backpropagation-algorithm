using System;
// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace Backpropagation.ANN
{
	public static class CriterionFunction
	{
		public static double[] EvaluatePerSymbol(double[][] givenOutputs, int[][] expectedOutputs)
		{
			if (givenOutputs.Length != expectedOutputs.Length)
				throw new Exception("Arrays must be of same size!");

			double[] perSymbolError = new double[givenOutputs[0].Length];

			for (int i = 0; i < givenOutputs.Length; i++)
			{
				int index = WhichClass(expectedOutputs[i]);
				perSymbolError[index] += EvaluateSingleSample(givenOutputs[i], expectedOutputs[i]);
			}
			for (int i = 0; i < givenOutputs[0].Length; i++)
			{
				perSymbolError[i] /= 2 * givenOutputs.Length;
			}
			return perSymbolError;
		}

		public static double Evaluate(double[][] givenOutputs, int[][] expectedOutputs)
		{
			if (givenOutputs.Length != expectedOutputs.Length)
				throw new Exception("Arrays must be of same size!");

			double error = 0;
			for (int i = 0; i < givenOutputs.Length; i++)
			{
				error += EvaluateSingleSample(givenOutputs[i], expectedOutputs[i]);
			}
			return error/(2 * givenOutputs.Length);
		}

		private static double EvaluateSingleSample(double[] givenOutputs, int[] expectedOutputs)
		{
			if(givenOutputs.Length != expectedOutputs.Length)
				throw new Exception("Arrays must be of same size!");

			double sum = 0;
			for (int i = 0; i < givenOutputs.Length; i++)
			{
				sum += Math.Pow(expectedOutputs[i] - givenOutputs[i], 2);
			}
			return sum;
		}

		private static int WhichClass(int[] classes)
		{
			int which = 0;
			for (int i = 0; i < classes.Length; i++)
			{
				if (classes[i] == 1)
					which = i;
			}
			return which;
		}
	}
}