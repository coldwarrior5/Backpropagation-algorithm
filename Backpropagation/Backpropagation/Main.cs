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

		public Main()
		{
			InitializeComponent();
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
	}
}
