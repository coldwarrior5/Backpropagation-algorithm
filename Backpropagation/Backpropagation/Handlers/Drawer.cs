using System;
using System.Drawing;
using System.Windows.Forms;

namespace Backpropagation.Handlers
{
	public class Drawer
	{
		private readonly SymbolHandler _symbol;
		private readonly PictureBox _screen;

		public Drawer(PictureBox drawingBoard, SymbolHandler symbol)
		{
			_symbol = symbol;
			
			if (drawingBoard.Image is null)
				throw new ArgumentException("Picture box must have image.");
			_screen = drawingBoard;
		}

		public void AddPoint(int x, int y)
		{
			_symbol.AddPoint(x, y);
			Draw(x, y);
		}

		private void Draw(int x, int y)
		{
			((Bitmap)_screen.Image).SetPixel(x, y, Color.Black);
			_screen.Refresh();
		}

		public void ResetPoints()
		{
			_symbol.ResetPoints();
			ClearBoard();
		}

		private void ClearBoard()
		{
			_screen.Image = Properties.Resources.Board;
			_screen.Refresh();
		}
	}
}