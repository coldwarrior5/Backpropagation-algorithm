using System.Collections.Generic;

namespace Backpropagation.ANN.Interfaces
{
	public interface INeuron
	{
		double GetOutput(List<double> x);
	}
}