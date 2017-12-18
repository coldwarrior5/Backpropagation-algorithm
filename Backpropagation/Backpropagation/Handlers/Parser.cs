using System.Collections.Generic;
using System.Text.RegularExpressions;
using Backpropagation.Interfaces;
using Backpropagation.Structures;

namespace Backpropagation.Handlers
{
	public class Parser : IParser
	{
		private const string Comment = "%%";
		private const string InstanceInfo = "% ";
		private const string EmptyLine = "";
		private const string Sample = "sample";

		private const string SymbolsInFile = "Symbols";
		private const string SamplesInFile = "Samples";
		private const string SymbolSamplesInFile = "Per symbol samples";

		private readonly FileHandler _fileHandler;
		private Instance _instance;

		private int _numSymbols;
		private int _numSamples;
		private int _numSymbolSamples;
		
		private int _iSamples;

		public Parser()
		{
			_fileHandler = new FileHandler();
			_iSamples = 0;

		}

		public Instance ParseData(string fileName)
		{
			var data = _fileHandler.ReadFile(fileName);
			for (var i = 0; i < data.Length; i++)
			{
				ParseLine(data[i], i + 1);
			}
			CheckParameters();
			return _instance;
		}

		public void FormatAndSaveResult(string fileName, Instance result)
		{
			_fileHandler.SaveFile(fileName, FormatData(result));
		}

		private static string[] FormatData(Instance input)
		{
			int displacement = 4;
			string[] result = new string[input.NumSymbols * input.NumSamples + displacement];

			result[0] = InstanceInfo + SymbolsInFile + " " + input.NumSymbols;
			result[1] = InstanceInfo + SamplesInFile + " " + input.NumSamples;
			result[2] = InstanceInfo + SymbolSamplesInFile + " " + input.NumSymbolSamples;
			result[3] = EmptyLine;

			for (var i = 0; i < input.NumSymbols * input.NumSamples; i++)
			{
				result[i + displacement] = Sample + " [";
				for (var j = 0; j < input.NumSymbolSamples; j++)
				{
					result[i + displacement] += "'" + input.Symbols[i].XPositions[j].ToString("G17");
					if (j != input.NumSymbolSamples - 1)
						result[i + displacement] += "',";
					else
						result[i + displacement] += "'] [";
				}
				for (var j = 0; j < input.NumSymbolSamples; j++)
				{
					result[i + displacement] += "'" + input.Symbols[i].YPositions[j].ToString("G17");
					if (j != input.NumSymbolSamples - 1)
						result[i + displacement] += "',";
					else
						result[i + displacement] += "'] [";
				}
				for (var j = 0; j < input.NumSymbols; j++)
				{
					result[i + displacement] += "'" + input.Symbols[i].Class[j];
					if (j != input.NumSymbols - 1)
						result[i + displacement] += "',";
					else
						result[i + displacement] += "']";
				}
			}
				
			return result;
		}

		private void ParseLine(string line, int position)
		{
			if (line.StartsWith(Comment) || line is EmptyLine)
			{
				Instance temp = new Instance(_numSymbols, _numSamples, _numSymbolSamples);
				if (_instance is null || !_instance.Equals(temp))
					_instance = new Instance(_numSymbols, _numSamples, _numSymbolSamples);
				return;
			}
				
			if (line.StartsWith(InstanceInfo))
				ParseInstance(line);
			else if (line.StartsWith(Sample))
				ParseSample(line, position);
			else
				ErrorHandler.TerminateExecution(ErrorCode.ImproperLine, "Line " + position + " is not valid.");
		}

		private void ParseInstance(string line)
		{
			var splits = line.Split(' ');
			if (line.Contains(SymbolsInFile))
			{
				var success = int.TryParse(splits[splits.Length - 1], out _numSymbols);
				if (!success)
					ErrorHandler.TerminateExecution(ErrorCode.ImproperLine);
			}
			else if (line.Contains(SamplesInFile))
			{
				var success = int.TryParse(splits[splits.Length - 1], out _numSamples);
				if (!success)
					ErrorHandler.TerminateExecution(ErrorCode.ImproperLine);
			}
			else if (line.Contains(SymbolSamplesInFile))
			{
				var success = int.TryParse(splits[splits.Length - 1], out _numSymbolSamples);
				if (!success)
					ErrorHandler.TerminateExecution(ErrorCode.ImproperLine);
			}
		}

		private void ParseSample(string line, int position)
		{
			List<double> xPositions = new List<double>();
			List<double> yPositions = new List<double>();
			List<int> classes = new List<int>();

			if (_iSamples++ >= _numSamples * _numSymbolSamples)
				ErrorHandler.TerminateExecution(ErrorCode.TooManySamples);

			var splits = line.Split(' ');
			FillDoubleList(xPositions, splits[1], position);
			FillDoubleList(yPositions, splits[2], position);
			FillIntList(classes, splits[3], position);
			
			_instance.AddSymbol(xPositions, yPositions, classes);
		}

		private static void FillDoubleList(ICollection<double> values, string line, int position)
		{
			if (line != null)
			{
				var matches = Regex.Matches(line, "[-0-9.]+");
				var instances = matches.GetEnumerator();
				while (instances.MoveNext())
				{
					if (instances.Current == null) continue;
					double.TryParse(instances.Current.ToString(), out double elem);
					values.Add(elem);
				}
			}
			else
				ErrorHandler.TerminateExecution(ErrorCode.ImproperLine, "Line " + position + " does not contain x or y values.");
		}

		private static void FillIntList(ICollection<int> values, string line, int position)
		{
			if (line != null)
			{
				var matches = Regex.Matches(line, "[0-9.]+");
				var instances = matches.GetEnumerator();
				while (instances.MoveNext())
				{
					if (instances.Current == null) continue;
					int.TryParse(instances.Current.ToString(), out int elem);
					values.Add(elem);
				}
			}
			else
				ErrorHandler.TerminateExecution(ErrorCode.ImproperLine, "Line " + position + " does not contain values of classes.");
		}

		private void CheckParameters()
		{
			if (_iSamples < _numSymbols * _numSamples - 1)
				ErrorHandler.TerminateExecution(ErrorCode.NotEnoughSamples);
		}
	}
}