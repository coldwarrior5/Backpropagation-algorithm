using Backpropagation.Structures;

namespace Backpropagation.Interfaces
{
	public interface IParser
	{
		Instance ParseData(string fileName);
		void FormatAndSaveResult(string fileName, Instance result);
	}
}