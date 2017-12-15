using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Backpropagation.Handlers;

namespace Backpropagation.Structures
{
	public class InputParams
	{
		public int Symbols;
		public int Samples;
		public int PerSymbolSamples;

		public const int NumSymbolMin = 2;
		public const int NumSymbolDefault = 5;
		public const int NumSymbolMax = 10;

		public const int NumSamplesMin = 10;
		public const int NumSamplesDefault = 20;
		public const int NumSamplesMax = 30;

		public const int NumSymbolSamplesMin = 10;
		public const int NumSymbolSamplesDefault = 20;
		public const int NumSymbolSamplesMax = 100;

		public const double EtaMin = 0.001;
		public const double EtaDefault = 0.01;
		public const double EtaMax = 1;

		public const int LimitMin = 100;
		public const int LimitDefault = 10000;
		public const int LimitMax = 1000000;

		public InputParams()
		{
			Symbols = NumSymbolDefault;
			Samples = NumSamplesDefault;
			PerSymbolSamples = NumSymbolSamplesDefault;
		}

		public void OnValueChanged_Symbol(object sender, EventArgs e)
		{
			if (sender is ComboBox box)
				Symbols = (int)box.SelectedItem;
		}

		public void OnValueChanged_Samples(object sender, EventArgs e)
		{
			if (sender is ComboBox box)
				Samples = (int)box.SelectedItem;
		}

		public void OnValueChanged_SymbolSamples(object sender, EventArgs e)
		{
			if (sender is ComboBox box)
				PerSymbolSamples = (int)box.SelectedItem;
		}

		public static void FillParamChoices(ComboBox symbolBox, ComboBox samplesBox, ComboBox symbolSamplesBox, ComboBox loadTestSet)
		{
			FillComboBox(loadTestSet);
			FillComboBox(symbolBox, NumSymbolMin, NumSymbolMax);
			FillComboBox(samplesBox, NumSamplesMin, NumSamplesMax);
			FillComboBox(symbolSamplesBox, NumSymbolSamplesMin, NumSymbolSamplesMax);
			symbolBox.SelectedItem = NumSymbolDefault;
			samplesBox.SelectedItem = NumSamplesDefault;
			symbolSamplesBox.SelectedItem = NumSymbolSamplesDefault;
		}

		public static void FillTrainChoices(Chart graph, Panel architecturePanel, ComboBox backpropagationType, TextBox eta, TextBox limit)
		{
			backpropagationType.Items.Add("Batch");
			backpropagationType.Items.Add("Online");
			backpropagationType.Items.Add("Mini-batch");
			backpropagationType.SelectedItem = backpropagationType.Items[0];
			eta.Text = EtaDefault.ToString(CultureInfo.InvariantCulture);
			limit.Text = LimitDefault.ToString(CultureInfo.InvariantCulture);
		}

		private static void FillComboBox(ComboBox comboBox, int minValue, int maxValue)
		{
			if(comboBox.Items.Count != 0)
				comboBox.Items.Clear();
			for (int i = minValue; i <= maxValue; i++)
			{
				comboBox.Items.Add(i);
			}
		}

		private static void FillComboBox(ComboBox comboBox)
		{
			FileHandler fileHandler = new FileHandler();
			List<string> files = fileHandler.FileList();
			foreach (string file in files)
			{
				comboBox.Items.Add(file);
			}
			if (comboBox.Items.Count > 0)
				comboBox.SelectedItem = comboBox.Items[0];
		}

		public static void SetClasses(TableLayoutPanel panel, int numSymbols, int numSamples)
		{
			int columnCount = numSymbols > 4 ? 5 : numSymbols;
			int rowCount = (numSymbols - 1) / 5 + 1;

			panel.ColumnCount = columnCount * 2;
			panel.RowCount = rowCount;

			panel.ColumnStyles.Clear();
			panel.RowStyles.Clear();
			panel.Controls.Clear();

			for (var i = 0; i < columnCount; i++)
			{
				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount * 0.7f));
				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount * 0.3f));
			}
			for (var i = 0; i < rowCount; i++)
			{
				panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rowCount));
			}

			for (var i = 0; i < numSymbols; i++)
			{
				var button = new Button
				{
					Text = (i + 1).ToString(),
					Name = $"b_{i + 1}",
					Dock = DockStyle.Fill
				};

				var label = new Label
				{
					Text = numSamples.ToString(),
					Name = $"l_{i + 1}",
					AutoSize = false,
					TextAlign = ContentAlignment.MiddleCenter,
					Dock = DockStyle.Fill
				};

				panel.Controls.Add(button);
				panel.Controls.Add(label);
			}
		}
	}
}