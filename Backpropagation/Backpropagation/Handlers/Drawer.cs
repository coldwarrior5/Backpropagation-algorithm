using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Backpropagation.Handlers
{
	public class Drawer
	{
		private List<int> xPositions;
		private List<int> yPositions;

		public Drawer()
		{
			xPositions = new List<int>();
			yPositions = new List<int>();
		}

		public void AddPoint(int x, int y)
		{
			xPositions.Add(x);
			yPositions.Add(y);
		}

		public void Draw(PictureBox drawingBoard)
		{
			for (int i = 0; i < xPositions.Count; i++)
			{
				((Bitmap)drawingBoard.Image).SetPixel(xPositions[i], yPositions[i], Color.Black);
			}
			drawingBoard.Refresh();
		}

		public void ResetPoints()
		{
			xPositions.Clear();
			yPositions.Clear();
		}

		public void ClearBoard(PictureBox drawingBoard)
		{
			drawingBoard.Image = Properties.Resources.Board;
			drawingBoard.Refresh();
		}
	}
}