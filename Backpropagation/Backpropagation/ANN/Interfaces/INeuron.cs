using System.Collections.Generic;

namespace Backpropagation.ANN.Interfaces
{
	public interface INeuron
	{
		double GetOutput(double[] x);
		void Reset();
		void ApplyChange(List<double> getRange);
		double Backpropagation(double[] inputs, double[] outputs, double[] deltaOrDesired, ref List<double> changes, int j, bool lastLayer);
	}
}