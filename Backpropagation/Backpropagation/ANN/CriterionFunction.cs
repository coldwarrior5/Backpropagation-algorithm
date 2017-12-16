using System;
using System.Collections.Generic;
// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace Backpropagation.ANN
{
	public static class CriterionFunction
	{
		public static double Evaluate(List<List<double>> givenOutputs, List<List<double>> expectedOutputs)
		{
			if (givenOutputs.Count != expectedOutputs.Count)
				throw new Exception("Arrays must be of same size!");

			double error = 0;
			for (int i = 0; i < givenOutputs.Count; i++)
			{
				error += EvaluateSingleSample(givenOutputs[i], expectedOutputs[i]);
			}
			return error/(2 * givenOutputs.Count);
		}

		private static double EvaluateSingleSample(List<double> givenOutputs, List<double> expectedOutputs)
		{
			if(givenOutputs.Count != expectedOutputs.Count)
				throw new Exception("Arrays must be of same size!");

			double sum = 0;
			for (int i = 0; i < givenOutputs.Count; i++)
			{
				sum += Math.Pow(expectedOutputs[i] - givenOutputs[i], 2);
			}
			return sum;
		}
	}
}