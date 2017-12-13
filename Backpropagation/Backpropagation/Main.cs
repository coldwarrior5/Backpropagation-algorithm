using System;
using System.Collections.Generic;
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
		private List<Panel> _panels;

		public Main()
		{
			InitializeComponent();
			_drawer = new Drawer();
			_panels = new List<Panel>{panelParam, panelTestSet, panelTrain, panelTest};
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
			buttonTestSet.Enabled = false;
			buttonTrain.Enabled = false;
			buttonTest.Enabled = false;
			UiHandler.PanelVisible(panelParam, _panels);
		}

		private void buttonTestSet_Click(object sender, EventArgs e)
		{
			SetSlider(buttonTestSet.Top, buttonTestSet.Height);
			buttonTrain.Enabled = false;
			buttonTest.Enabled = false;
			UiHandler.PanelVisible(panelTestSet, _panels);
		}

		private void buttonTrain_Click(object sender, EventArgs e)
		{
			SetSlider(buttonTrain.Top, buttonTrain.Height);
			buttonTest.Enabled = false;
			UiHandler.PanelVisible(panelTrain, _panels);
		}

		private void buttonTest_Click(object sender, EventArgs e)
		{
			SetSlider(buttonTest.Top, buttonTest.Height);
			UiHandler.PanelVisible(panelTest, _panels);
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
			_drawer.Draw(drawingBoard, e.Location.X, e.Location.Y);
		}

		private void SetParameters_Click(object sender, EventArgs e)
		{
			UiHandler.PanelVisible(panelTestSet, _panels);
			buttonTestSet.Enabled = true;
			SetSlider(buttonTestSet.Top, buttonTestSet.Height);
		}

		private void SaveTestSet_Click(object sender, EventArgs e)
		{
			UiHandler.PanelVisible(panelTrain, _panels);
			buttonTrain.Enabled = true;
			SetSlider(buttonTrain.Top, buttonTrain.Height);
		}

		private void Train_Click(object sender, EventArgs e)
		{
			Train.Visible = false;
			GoToTest.Visible = true;
		}

		private void GoToTest_Click(object sender, EventArgs e)
		{
			UiHandler.PanelVisible(panelTest, _panels);
			Train.Visible = true;
			buttonTest.Enabled = true;
			SetSlider(buttonTest.Top, buttonTest.Height);
		}

		private void Test_Click(object sender, EventArgs e)
		{

		}
	}
}
