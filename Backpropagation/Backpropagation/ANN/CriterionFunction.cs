using System;
// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace Backpropagation.ANN
{
	public static class CriterionFunction
	{
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
	}
}