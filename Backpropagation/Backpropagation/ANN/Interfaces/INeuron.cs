namespace Backpropagation.ANN.Interfaces
{
	public interface INeuron
	{
		double GetOutput(double[] x);
		void Reset();
	}
}