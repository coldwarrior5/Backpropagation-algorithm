using System;
using System.Drawing;
using System.Windows.Forms;
using Backpropagation.Handlers;

namespace Backpropagation
{
	public partial class Main : Form
	{
		private bool _mouseDown;
		private Point _lastLocation;
		private Drawer _drawer;

		public Main()
		{
			InitializeComponent();
			_drawer = new Drawer();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Location = Owner.Location;
			Size = Owner.Size;
		}

		private void Main_Load(object sender, EventArgs e)
		{

		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			ErrorHandler.TerminateExecution(ErrorCode.UserTermination);
		}

		private void buttonExit_MouseEnter(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.ExitHighlighted;
		}

		private void buttonExit_MouseLeave(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.Exit;
		}

		private void titlebar_MouseDown(object sender, MouseEventArgs e)
		{
			_mouseDown = true;
			_lastLocation = e.Location;
		}

		private void titlebar_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_mouseDown) return;
			Location = new Point(
				Location.X - _lastLocation.X + e.X, Location.Y - _lastLocation.Y + e.Y);

			Update();
		}

		private void titlebar_MouseUp(object sender, MouseEventArgs e)
		{
			_mouseDown = false;
		}

		private void SetSlider(int top, int height)
		{
			panelSlider.Top = top;
			panelSlider.Height = height;
		}

		private void buttonParams_Click(object sender, EventArgs e)
		{
			SetSlider(buttonParams.Top, buttonParams.Height);
		}

		private void buttonTestSet_Click(object sender, EventArgs e)
		{
			SetSlider(buttonTestSet.Top, buttonTestSet.Height);
		}

		private void buttonTrain_Click(object sender, EventArgs e)
		{
			SetSlider(buttonTrain.Top, buttonTrain.Height);
		}

		private void buttonTest_Click(object sender, EventArgs e)
		{
			SetSlider(buttonTest.Top, buttonTest.Height);
		}

		private void DrawingBoard_MouseDown(object sender, MouseEventArgs e)
		{
			_mouseDown = true;
			_drawer.ResetPoints();
			_drawer.ClearBoard(drawingBoard);
			UpdateDrawingBoard(e);
		}

		private void DrawingBoard_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_mouseDown) return;
			UpdateDrawingBoard(e);
		}

		private void DrawingBoard_MouseUp(object sender, MouseEventArgs e)
		{
			UpdateDrawingBoard(e);
			_mouseDown = false;
		}

		private void UpdateDrawingBoard(MouseEventArgs e)
		{
			_drawer.AddPoint(e.Location.X, e.Location.Y);
			_drawer.Draw(drawingBoard);
		}
	}
}
