using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Backpropagation.Handlers;
using Backpropagation.Structures;

namespace Backpropagation
{
	public partial class Main : Form
	{
		private Mover _screenMover;
		private Drawer _drawer;
		private InputParams _params;
		private Parser _parser;
		private Instance _instance;
		private int _whichClass;
		private SymbolHandler _symbol;
		private List<Panel> _panels;
		private bool _mouseDown;

		public Main()
		{
			InitializeComponent();
			_params = new InputParams();
			_parser = new Parser();
			_screenMover = new Mover();
			_panels = new List<Panel> { panelParam, panelTestSet, panelTrain, panelTest };
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

		//
		// Additional methods
		//
		private void SetDrawer(PictureBox board)
		{
			if(_symbol is null)
				_symbol = new SymbolHandler(_params.PerSymbolSamples);
			_drawer = new Drawer(board, _symbol);
		}

		private void SetButtons()
		{
			for (int i = 0; i < tableClasses.Controls.Count; i++)
			{
				if(tableClasses.Controls[i] is Button)
					tableClasses.Controls[i].Click += B_Click;
			}
		}
		// ______________________________________________________________

		//
		// Exit button
		//
		private void ButtonExit_Click(object sender, EventArgs e)
		{
			ErrorHandler.TerminateExecution(ErrorCode.UserTermination);
		}

		private void ButtonExit_MouseEnter(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.ExitHighlighted;
		}

		private void ButtonExit_MouseLeave(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.Exit;
		}
		// ______________________________________________________________

		//
		// Titlebar
		//
		private void Titlebar_MouseDown(object sender, MouseEventArgs e)
		{
			_screenMover.MouseDown(e.Location);
		}

		private void Titlebar_MouseMove(object sender, MouseEventArgs e)
		{
			var moved = _screenMover.MouseMove(e.Location, Location, out Point newLocation);
			if (moved)
				Location = newLocation;
			Update();
		}

		private void Titlebar_MouseUp(object sender, MouseEventArgs e)
		{
			_screenMover.MouseUp();
		}
		// ______________________________________________________________

		//
		// Parameters panel
		//
		private void ParamsPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible) return;

			InputParams.FillParamChoices(numOfSymbols, numOfSamples, numOfSymbolSamples, loadTestSet);
			buttonLoadTestSet.Enabled = loadTestSet.SelectedItem != null;
		}

		private void OnValueChanged_Symbol(object sender, EventArgs e)
		{
			_params?.OnValueChanged_Symbol(sender, e);
		}

		private void OnValueChanged_Samples(object sender, EventArgs e)
		{
			_params?.OnValueChanged_Samples(sender, e);
		}

		private void OnValueChanged_SymbolSamples(object sender, EventArgs e)
		{
			_params?.OnValueChanged_SymbolSamples(sender, e);
		}

		private void SetParameters_Click(object sender, EventArgs e)
		{
			UiHandler.PanelVisible(panelTestSet, _panels);
			buttonTestSet.Enabled = true;
			UiHandler.SetSlider(panelSlider, buttonTestSet.Top, buttonTestSet.Height);
		}

		private void ButtonLoadTestSet_Click(object sender, EventArgs e)
		{
			_instance = _parser.ParseData(loadTestSet.Text);
			_params.Samples = _instance.NumSamples;
			_params.Symbols = _instance.NumSymbols;
			_params.PerSymbolSamples = _instance.NumSymbolSamples;
			UiHandler.SetSlider(panelSlider, buttonTrain.Top, buttonTrain.Height);
			buttonTestSet.Enabled = true;
			buttonTrain.Enabled = true;
			UiHandler.PanelVisible(panelTrain, _panels);
		}

		private void ButtonParams_Click(object sender, EventArgs e)
		{
			UiHandler.SetSlider(panelSlider, buttonParams.Top, buttonParams.Height);
			buttonTestSet.Enabled = false;
			buttonTrain.Enabled = false;
			buttonTest.Enabled = false;
			UiHandler.PanelVisible(panelParam, _panels);
		}
		// ______________________________________________________________

		//
		// Test set panel
		//
		private void TestSetPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible) return;

			SetDrawer(drawingBoard);
			_instance = new Instance(_params.Symbols, _params.Samples, _params.PerSymbolSamples);
			InputParams.SetClasses(tableClasses, _params.Symbols, _params.Samples);
			SetButtons();
		}

		private void DrawingBoard_MouseDown(object sender, MouseEventArgs e)
		{
			_mouseDown = true;
			_drawer.ResetPoints();
			_drawer.AddPoint(e.Location.X, e.Location.Y);
		}

		private void DrawingBoard_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_mouseDown) return;
			_drawer.AddPoint(e.Location.X, e.Location.Y);
		}

		private void DrawingBoard_MouseUp(object sender, MouseEventArgs e)
		{
			_drawer.AddPoint(e.Location.X, e.Location.Y);
			_symbol.ProcessSymbol();
			_mouseDown = false;
		}

		void B_Click(object sender, EventArgs e)
		{
			if (sender is Button b)
			{
				buttonSetSample.Enabled = true;
				int.TryParse(b.Text, out int which);
				_whichClass = which - 1;
			}
		}

		private void ButtonSetSample_Click(object sender, EventArgs e)
		{
			UiHandler.DecrementValue(tableClasses, _whichClass);
			buttonSetSample.Enabled = false;
			_instance.AddSymbol(_symbol.GetXrepresentors(), _symbol.GetYrepresentros(), _whichClass, _params.Symbols);
			_drawer.ResetPoints();
			if (UiHandler.AllButtonsDisabled(tableClasses))
				SaveTestSet.Enabled = true;
		}

		private void SaveTestSet_Click(object sender, EventArgs e)
		{
			savePanel.Visible = true;
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			savePanel.Visible = false;
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			SaveTestSet.Enabled = false;
			savePanel.Visible = false;
			_parser.FormatAndSaveResult(fileNameBox.Text, _instance);
			UiHandler.PanelVisible(panelTrain, _panels);
			buttonTrain.Enabled = true;
			UiHandler.SetSlider(panelSlider, buttonTrain.Top, buttonTrain.Height);
		}

		private void ButtonTestSet_Click(object sender, EventArgs e)
		{
			UiHandler.SetSlider(panelSlider, buttonTestSet.Top, buttonTestSet.Height);
			buttonTrain.Enabled = false;
			buttonTest.Enabled = false;
			UiHandler.PanelVisible(panelTestSet, _panels);
		}
		// ______________________________________________________________

		//
		// Train panel
		//
		private void TrainPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible) return;
			InputParams.FillTrainChoices(errorChart, layoutArchitexture, comboBoxType, textBoxEta, textBoxLimit);
		}

		private void Train_Click(object sender, EventArgs e)
		{
			Train.Visible = false;
			GoToTest.Visible = true;
		}

		private void GoToTest_Click(object sender, EventArgs e)
		{
			Train.Visible = true;
			GoToTest.Visible = false;
			UiHandler.PanelVisible(panelTest, _panels);
			buttonTest.Enabled = true;
			UiHandler.SetSlider(panelSlider, buttonTest.Top, buttonTest.Height);
		}

		private void ButtonTrain_Click(object sender, EventArgs e)
		{
			UiHandler.SetSlider(panelSlider, buttonTrain.Top, buttonTrain.Height);
			buttonTest.Enabled = false;
			UiHandler.PanelVisible(panelTrain, _panels);
		}
		// ______________________________________________________________

		//
		// Test panel
		//
		private void TestPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible) return;
			SetDrawer(drawingBoardTest);
		}

		private void Test_Click(object sender, EventArgs e)
		{
			// TODO ANN.Guess();
			_drawer.ResetPoints();

			// TODO Remove later
			Random rand = new Random();
			labelClass.Text = rand.Next(1, _params.Symbols + 1).ToString();
		}

		private void ButtonTest_Click(object sender, EventArgs e)
		{
			UiHandler.SetSlider(panelSlider, buttonTest.Top, buttonTest.Height);
			UiHandler.PanelVisible(panelTest, _panels);
		}
		// ______________________________________________________________
	}
}
