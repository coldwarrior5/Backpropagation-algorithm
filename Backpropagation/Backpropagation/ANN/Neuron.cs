using System;
using System.Collections.Generic;
using Backpropagation.ANN.Interfaces;
using Backpropagation.Handlers;

namespace Backpropagation.ANN
{
	public class Neuron : INeuron
	{
		public int InputSize { get; }
		private double[] _weights;
		private IActivationFunction _function;

		public Neuron(int inputSize, IActivationFunction function)
		{
			InputSize = inputSize;
			if(inputSize != 0) InitWeights();
			ChangeFunction(function);
		}

		// Non zero weights initialization
		private void InitWeights()
		{
			_weights = new double[InputSize + 1];
			for (int i = 0; i < InputSize + 1; i++)
			{
				_weights[i] = MathHandler.Rand.NextDouble();
			}
		}

		public void ChangeFunction(IActivationFunction function)
		{
			_function = function;
		}

		public double GetOutput(List<double> x)
		{
			if(InputSize != 0 && x.Count != InputSize)
				throw new ArgumentException(@"Array must have exact number of elements.", x.ToString());

			if (_weights is null)
				return _function.Function(x[0]);

			double sum = _weights[0];
			for (int i = 0; i < x.Count; i++)
			{
				sum += _weights[i + 1] * _function.Function(x[i]);
			}
			return _function.Function(sum);
		}
	}
}