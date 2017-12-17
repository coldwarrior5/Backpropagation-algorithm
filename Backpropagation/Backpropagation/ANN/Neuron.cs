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
			if (inputSize != 0) InitWeights();
			ChangeFunction(function);
		}

		// Non zero weights initialization
		private void InitWeights()
		{
			_weights = new double[InputSize + 1];
			RandomWeights();
		}

		private void RandomWeights()
		{
			for (var i = 0; i < InputSize + 1; i++)
			{
				_weights[i] = MathHandler.Rand.NextDouble();
			}
		}

		public void Reset()
		{
			if(InputSize != 0)
				RandomWeights();
		}

		public void ChangeFunction(IActivationFunction function)
		{
			_function = function;
		}

		public double GetOutput(double[] x)
		{
			if(InputSize != 0 && x.Length != InputSize)
				throw new ArgumentException(@"Array must have exact number of elements.", x.ToString());

			if (_weights is null)
				return _function.Function(x[0]);

			double sum = _weights[0];
			for (int i = 0; i < x.Length; i++)
			{
				sum += _weights[i + 1] * _function.Function(x[i]);
			}
			return _function.Function(sum);
		}

		public void ApplyChange(List<double> getRange)
		{
			if (InputSize != 0 && getRange.Count != InputSize)
				throw new ArgumentException(@"Array must have exact number of elements.", getRange.ToString());
			for (int i = 0; i < InputSize; i++)
			{
				_weights[i + 1] += getRange[i];
			}
		}

		public double Backpropagation(double[] inputs, double[] outputs, double[] deltaOrDesired, ref List<double> changes, int j, bool lastLayer)
		{
			double delta;
			if (lastLayer)
				delta = outputs[j] * (1 - outputs[j]) * (deltaOrDesired[j] - outputs[j]);
			else
			{
				double sum = 0;
				for (int i = 0; i < deltaOrDesired.Length; i++)
				{
					sum += _weights[i + 1] * deltaOrDesired[i];
				}
				delta = outputs[j] * (1 - outputs[j]) * sum;
			}
			for (int i = 0; i < InputSize; i++)
			{
				changes[j * InputSize + i] += delta * inputs[i];
			}
			return delta;
		}
	}
}