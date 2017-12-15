using System;
using System.Collections.Generic;

namespace Backpropagation.Handlers
{
	public class SymbolHandler
	{
		private int _size;
		private List<double> _xPositions;
		private List<double> _yPositions;
		private double _length;
		private int _samples;

		private List<double> _xRepresentors;
		private List<double> _yRepresentors;

		public SymbolHandler(int samples)
		{
			_size = 0;
			_samples = samples;
			_xPositions = new List<double>();
			_yPositions = new List<double>();
			_xRepresentors = new List<double>();
			_yRepresentors = new List<double>();
		}

		public void AddPoint(int x, int y)
		{
			_size++;
			_xPositions.Add(x);
			_yPositions.Add(y);
		}

		public void ResetPoints()
		{
			_size = 0;
			_xPositions.Clear();
			_yPositions.Clear();
			_xRepresentors.Clear();
			_yRepresentors.Clear();
		}

		public void ProcessSymbol()
		{
			MoveToOrigin();
			ScalePoints();
			SymbolLength();
			FindReprentors();
		}

		public List<double> GetXrepresentors()
		{
			return _xRepresentors;
		}

		public List<double> GetYrepresentros()
		{
			return _yRepresentors;
		}

		/// <summary>
		/// Moves the symbol to origin by subtracting arithmetic mean from every point
		/// </summary>
		private void MoveToOrigin()
		{
			double tcX = MathHandler.ArithmeticMean(_xPositions);
			double tcY = MathHandler.ArithmeticMean(_yPositions);
			for (int i = 0; i < _size; i++)
			{
				_xPositions[i] -= tcX;
				_yPositions[i] -= tcY;
			}
		}

		/// <summary>
		/// Scales the points to fit interval -1 to 1 
		/// </summary>
		private void ScalePoints()
		{
			double mX = MathHandler.AbsMax(_xPositions);
			double mY = MathHandler.AbsMax(_yPositions);
			double m = Math.Max(mX, mY);
			for (var i = 0; i < _size; i++)
			{
				_xPositions[i] /= m;
				_yPositions[i] /= m;
			}
		}

		private void SymbolLength()
		{
			_length = 0;
			for (var i = 1; i < _size; i++)
			{
				var segmentLength = Math.Sqrt(Math.Pow(_xPositions[i] - _xPositions[i - 1], 2) + Math.Pow(_yPositions[i] - _yPositions[i - 1], 2));
				_length += segmentLength;
			}
		}

		private void FindReprentors()
		{
			int position = 1;
			double length = 0;
			_xRepresentors.Add(_xPositions[0]);
			_yRepresentors.Add(_yPositions[0]);

			for (var i = 1; i < _size; i++)
			{
				var segmentLength = Math.Sqrt(Math.Pow(_xPositions[i] - _xPositions[i - 1], 2) + Math.Pow(_yPositions[i] - _yPositions[i - 1], 2));
				length += segmentLength;
				double currentCutout = AtLength(position);
				if (length < currentCutout || currentCutout < 0) continue;

				if (Math.Abs(currentCutout - length) > Math.Abs(currentCutout - (length - segmentLength)))
				{
					_xRepresentors.Add(_xPositions[i]);
					_yRepresentors.Add(_yPositions[i]);
				}
				else
				{
					_xRepresentors.Add(_xPositions[i - 1]);
					_yRepresentors.Add(_yPositions[i - 1]);
				}
				position++;
			}
			if (_xRepresentors.Count < _samples)
			{
				_xRepresentors.Add(_xPositions[_size - 1]);
				_yRepresentors.Add(_yPositions[_size - 1]);
			}
		}

		private double AtLength(int position)
		{
			if (position > _samples - 1)
			{
				return -1;
			}
			return position * _length / (_samples - 1);
		}
	}
}