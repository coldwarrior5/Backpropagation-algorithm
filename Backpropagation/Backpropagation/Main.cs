using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Backpropagation.ANN;
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
		
		private NeuralNetwork _ann;

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
			if(_symbol is null || _symbol.GetSampleSize() != _params.PerSymbolSamples)
				_symbol = new SymbolHandler(_params.PerSymbolSamples);
			_drawer = new Drawer(board, _symbol);
		}

		private void SetNeuralNetwork()
		{
			if(_ann is null)
				_ann = new NeuralNetwork(_instance);
			else if(!_ann.Consistent())
				_ann = new NeuralNetwork(_instance);
		}

		private void SetButtons()
		{
			for (int i = 0; i < tableClasses.Controls.Count; i++)
			{
				if(tableClasses.Controls[i] is Button)
					tableClasses.Controls[i].Click += ClassButton_Click;
			}
		}

		private void SetTexts()
		{
			for (int i = 0; i < layoutArchitexture.Controls.Count; i++)
			{
				if (layoutArchitexture.Controls[i] is TextBox)
				{
					layoutArchitexture.Controls[i].KeyDown += LayerNumber_Changed;
					layoutArchitexture.Controls[i].Leave += LayerNumber_Left;
				}
					
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
			_symbol = new SymbolHandler(_params.PerSymbolSamples);
			UiHandler.SetSlider(panelSlider, buttonTrain.Top, buttonTrain.Height);
			buttonTestSet.Enabled = true;
			buttonTrain.Enabled = true;
			UiHandler.PanelVisible(panelTrain, _panels);
		}

		private void ButtonParams_Click(object sender, EventArgs e)
		{
			_drawer?.ResetPoints();
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

			Test.Enabled = true;
		}

		private void ClassButton_Click(object sender, EventArgs e)
		{
			if (!(sender is Button b)) return;

			buttonSetSample.Enabled = true;
			int.TryParse(b.Text, out int which);
			_whichClass = which - 1;
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
			_drawer?.ResetPoints();
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
			SetNeuralNetwork();
			NeuralNetwork.FillTrainChoices(errorChart, labelTotalError, layoutArchitexture, comboBoxType, textBoxEta, textBoxDesiredError, _ann);
			SetTexts();
		}

		private void LayerNumber_Changed(object sender, KeyEventArgs e)
		{
			if (!(sender is TextBox t)) return;

			switch (e.KeyCode)
			{
				case Keys.Enter:
					var splits = t.Name.Split('_');
					int.TryParse(splits[1], out int whichLayer);
					int.TryParse(t.Text, out int howMany);
					howMany = MathHandler.Clamp(howMany, NeuralNetwork.NeuronMin, NeuralNetwork.NeuronMax);
					t.Text = howMany.ToString();
					_ann.UpdateLayer(whichLayer, howMany);
					break;
				case Keys.Escape:
					splits = t.Name.Split('_');
					int.TryParse(splits[1], out whichLayer);
					t.Text = _ann.GetArchitecture()[whichLayer].ToString();
					break;
			}
			
		}

		private void LayerNumber_Left(object sender, EventArgs e)
		{
			if (!(sender is TextBox t)) return;
			
			var splits = t.Name.Split('_');
			int.TryParse(splits[1], out int whichLayer);
			int.TryParse(t.Text, out int howMany);
			howMany = MathHandler.Clamp(howMany, NeuralNetwork.NeuronMin, NeuralNetwork.NeuronMax);
			t.Text = howMany.ToString();
			_ann.UpdateLayer(whichLayer, howMany);
		}

		private void ButtonRemoveLayer_Click(object sender, EventArgs e)
		{
			buttonAddLayer.Enabled = true;
			_ann.RemoveLayer();
			NeuralNetwork.FillPanel(layoutArchitexture, _ann);
			SetTexts();
			if (_ann.GetArchitecture().Count == NeuralNetwork.LayersMin)
				buttonRemoveLayer.Enabled = false;
		}

		private void ButtonAddLayer_Click(object sender, EventArgs e)
		{
			buttonRemoveLayer.Enabled = true;
			_ann.AddLayer();
			NeuralNetwork.FillPanel(layoutArchitexture, _ann);
			SetTexts();
			if (_ann.GetArchitecture().Count == NeuralNetwork.LayersMax)
				buttonAddLayer.Enabled = false;
		}

		private void OnValueChanged_Type(object sender, EventArgs e)
		{
			_ann?.OnValueChanged_Type(sender, e);
		}

		private void Eta_Changed(object sender, KeyEventArgs e)
		{
			if (!(sender is TextBox t)) return;

			switch (e.KeyCode)
			{
				case Keys.Enter:
					double.TryParse(t.Text, out double eta);
					eta = MathHandler.Clamp(eta, NeuralNetwork.EtaMin, NeuralNetwork.EtaMax);
					t.Text = eta.ToString(CultureInfo.InvariantCulture);
					_ann.ChangeEta(eta);
					break;
				case Keys.Escape:
					t.Text = _ann.GetEta().ToString(CultureInfo.InvariantCulture);
					break;
			}
		}

		private void Eta_Left(object sender, EventArgs e)
		{
			if (!(sender is TextBox t)) return;
			
			double.TryParse(t.Text, out double eta);
			eta = MathHandler.Clamp(eta, NeuralNetwork.EtaMin, NeuralNetwork.EtaMax);
			t.Text = eta.ToString(CultureInfo.InvariantCulture);
			_ann.ChangeEta(eta);
		}

		private void DesiredError_Changed(object sender, KeyEventArgs e)
		{
			if (!(sender is TextBox t)) return;

			switch(e.KeyCode)
			{
				case Keys.Enter:
					double.TryParse(t.Text, out double desiredError);
					desiredError = MathHandler.Clamp(desiredError, NeuralNetwork.DesiredErrorMin, NeuralNetwork.DesiredErrorMax);
					t.Text = desiredError.ToString(CultureInfo.InvariantCulture);
					_ann.ChangeDesiredError(desiredError);
					break;
				case Keys.Escape:
					t.Text = _ann.GetDesiredError().ToString(CultureInfo.InvariantCulture);
					break;
			}
		}

		private void DesiredError_Left(object sender, EventArgs e)
		{
			if (!(sender is TextBox t)) return;

			double.TryParse(t.Text, out double desiredError);
			desiredError = MathHandler.Clamp(desiredError, NeuralNetwork.DesiredErrorMin, NeuralNetwork.DesiredErrorMax);
			t.Text = desiredError.ToString(CultureInfo.InvariantCulture);
			_ann.ChangeDesiredError(desiredError);
		}

		private void Train_Click(object sender, EventArgs e)
		{
			Train.Visible = false;
			GoToTest.Visible = true;
			_ann.Train();
			NeuralNetwork.FillChart(errorChart, labelTotalError, _ann);
		}

		private void GoToTest_Click(object sender, EventArgs e)
		{
			Train.Visible = true;
			GoToTest.Visible = false;
			_ann.FixArchitecture();
			UiHandler.PanelVisible(panelTest, _panels);
			buttonTest.Enabled = true;
			UiHandler.SetSlider(panelSlider, buttonTest.Top, buttonTest.Height);
		}

		private void ButtonTrain_Click(object sender, EventArgs e)
		{
			_ann?.ResetNetwork();
			_drawer?.ResetPoints();
			Train.Visible = true;
			GoToTest.Visible = false;
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
			if (!panel.Visible)
			{
				_drawer.ResetPoints();
				return;
			}
			SetDrawer(drawingBoardTest);
			labelClass.Text = "";
		}

		private void Test_Click(object sender, EventArgs e)
		{
			double[] results = _ann.GetOutputs(_symbol.GetXrepresentors().ToArray(), _symbol.GetYrepresentros().ToArray());
			int whichClass = NeuralNetwork.WhichClass(results) + 1;
			labelClass.Text = whichClass.ToString();
		}

		private void ButtonTest_Click(object sender, EventArgs e)
		{
			Test.Enabled = false;
			UiHandler.SetSlider(panelSlider, buttonTest.Top, buttonTest.Height);
			UiHandler.PanelVisible(panelTest, _panels);
		}
		// ______________________________________________________________
	}
}
