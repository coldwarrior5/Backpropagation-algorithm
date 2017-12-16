using System;
using System.Collections.Generic;
using Backpropagation.ANN.Interfaces;

namespace Backpropagation.ANN
{
	public class NeuronLayer
	{
		public int NumberOfNeurons { get; }
		public int InputSize { get; }
		private List<Neuron> _neurons;

		public NeuronLayer(int numberOfNeurons, int numberOfNeuronsInPreviousLayer, IActivationFunction function = null)
		{
			NumberOfNeurons = numberOfNeurons;
			InputSize = numberOfNeuronsInPreviousLayer;
			InitNeurons(function);
		}

		private void InitNeurons(IActivationFunction function)
		{
			_neurons = new List<Neuron>();
			DetermineRightFunction(ref function);
			for (int i = 0; i < NumberOfNeurons; i++)
				_neurons.Add(new Neuron(InputSize, function));
		}

		private void DetermineRightFunction(ref IActivationFunction function)
		{
			if (function is null)
				function = new Sigmoid();
			if (InputSize == 0)
				function = new Adaline();
		}

		public List<double> GetOutputs(List<double> inputs)
		{
			if(InputSize == 0 && inputs.Count != NumberOfNeurons)
				throw new ArgumentException(@"Input needs to contain the amount of elements that corresponds to input layer size!", inputs.ToString());
			if(InputSize != 0 && inputs.Count != InputSize)
				throw new ArgumentException(@"Input needs to contain the amount of elements that corresponds to former layer size!", inputs.ToString());

			List<double> outputs = new List<double>();
			for (int i = 0; i < NumberOfNeurons; i++)
			{
				outputs.Add(InputSize == 0 ? _neurons[i].GetOutput(new List<double> {inputs[i]}) : _neurons[i].GetOutput(inputs));
			}
			return outputs;
		}
	}
}