using System;
using System.Collections.Generic;

namespace Backpropagation.Structures
{
	public class Instance
	{
		private int _index;
		public int NumSymbols { get; }
		public int NumSamples { get; }
		public int NumSymbolSamples { get; }
		public Symbol[] Symbols;

		public Instance(int numSymbols, int numSamples, int numSymbolSamples)
		{
			_index = 0;
			NumSymbols = numSymbols;
			NumSamples = numSamples;
			NumSymbolSamples = numSymbolSamples;
			Symbols = new Symbol[NumSymbols * numSamples];
		}

		public void AddSymbol(List<double> xPositions, List<double> yPositions, List<int> classes)
		{
			if(xPositions.Count != NumSymbolSamples || yPositions.Count != NumSymbolSamples)
				throw new Exception("Arrays must have expected number of elements.");
			Symbols[_index++] = new Symbol(xPositions, yPositions, classes);
		}

		public void AddSymbol(List<double> xPositions, List<double> yPositions, int whichClass, int numClasses)
		{
			List<int> classes = new List<int>();
			for (int i = 0; i < numClasses; i++)
			{
				classes.Add(i == whichClass ? 1 : 0);
			}
			AddSymbol(xPositions, yPositions, classes);
		}
	}
}