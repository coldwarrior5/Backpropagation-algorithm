using System.Windows.Forms;

namespace Backpropagation
{
	partial class Main
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonParams = new System.Windows.Forms.Button();
			this.buttonTestSet = new System.Windows.Forms.Button();
			this.buttonTrain = new System.Windows.Forms.Button();
			this.buttonTest = new System.Windows.Forms.Button();
			this.titleBar = new System.Windows.Forms.Panel();
			this.panelSlider = new System.Windows.Forms.Panel();
			this.drawingBoard = new System.Windows.Forms.PictureBox();
			this.buttonExit = new System.Windows.Forms.Button();
			this.panelIcon = new System.Windows.Forms.Panel();
			this.flowLayoutPanel1.SuspendLayout();
			this.titleBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.drawingBoard)).BeginInit();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.panelIcon);
			this.flowLayoutPanel1.Controls.Add(this.buttonParams);
			this.flowLayoutPanel1.Controls.Add(this.buttonTestSet);
			this.flowLayoutPanel1.Controls.Add(this.buttonTrain);
			this.flowLayoutPanel1.Controls.Add(this.buttonTest);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(156, 600);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// buttonParams
			// 
			this.buttonParams.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonParams.FlatAppearance.BorderSize = 0;
			this.buttonParams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonParams.Location = new System.Drawing.Point(0, 92);
			this.buttonParams.Margin = new System.Windows.Forms.Padding(0);
			this.buttonParams.Name = "buttonParams";
			this.buttonParams.Size = new System.Drawing.Size(156, 93);
			this.buttonParams.TabIndex = 1;
			this.buttonParams.Text = "Define parameters";
			this.buttonParams.UseVisualStyleBackColor = true;
			this.buttonParams.Click += new System.EventHandler(this.buttonParams_Click);
			// 
			// buttonTestSet
			// 
			this.buttonTestSet.Enabled = false;
			this.buttonTestSet.FlatAppearance.BorderSize = 0;
			this.buttonTestSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTestSet.Location = new System.Drawing.Point(0, 185);
			this.buttonTestSet.Margin = new System.Windows.Forms.Padding(0);
			this.buttonTestSet.Name = "buttonTestSet";
			this.buttonTestSet.Size = new System.Drawing.Size(156, 93);
			this.buttonTestSet.TabIndex = 4;
			this.buttonTestSet.Text = "Enter test set";
			this.buttonTestSet.UseVisualStyleBackColor = true;
			this.buttonTestSet.Click += new System.EventHandler(this.buttonTestSet_Click);
			// 
			// buttonTrain
			// 
			this.buttonTrain.Enabled = false;
			this.buttonTrain.FlatAppearance.BorderSize = 0;
			this.buttonTrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTrain.Location = new System.Drawing.Point(0, 278);
			this.buttonTrain.Margin = new System.Windows.Forms.Padding(0);
			this.buttonTrain.Name = "buttonTrain";
			this.buttonTrain.Size = new System.Drawing.Size(156, 93);
			this.buttonTrain.TabIndex = 4;
			this.buttonTrain.Text = "Train neural network";
			this.buttonTrain.UseVisualStyleBackColor = true;
			this.buttonTrain.Click += new System.EventHandler(this.buttonTrain_Click);
			// 
			// buttonTest
			// 
			this.buttonTest.Enabled = false;
			this.buttonTest.FlatAppearance.BorderSize = 0;
			this.buttonTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTest.Location = new System.Drawing.Point(0, 371);
			this.buttonTest.Margin = new System.Windows.Forms.Padding(0);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(156, 93);
			this.buttonTest.TabIndex = 4;
			this.buttonTest.Text = "Test neural network";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// titleBar
			// 
			this.titleBar.BackColor = System.Drawing.Color.Transparent;
			this.titleBar.Controls.Add(this.buttonExit);
			this.titleBar.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.titleBar.Location = new System.Drawing.Point(156, 0);
			this.titleBar.Name = "titleBar";
			this.titleBar.Size = new System.Drawing.Size(644, 32);
			this.titleBar.TabIndex = 3;
			this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
			this.titleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseMove);
			this.titleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseUp);
			// 
			// panelSlider
			// 
			this.panelSlider.BackColor = System.Drawing.Color.LightSkyBlue;
			this.panelSlider.Location = new System.Drawing.Point(159, 92);
			this.panelSlider.Name = "panelSlider";
			this.panelSlider.Size = new System.Drawing.Size(10, 93);
			this.panelSlider.TabIndex = 4;
			// 
			// drawingBoard
			// 
			this.drawingBoard.BackColor = System.Drawing.Color.White;
			this.drawingBoard.Image = global::Backpropagation.Properties.Resources.Board;
			this.drawingBoard.Location = new System.Drawing.Point(230, 40);
			this.drawingBoard.Name = "drawingBoard";
			this.drawingBoard.Size = new System.Drawing.Size(500, 400);
			this.drawingBoard.TabIndex = 5;
			this.drawingBoard.TabStop = false;
			this.drawingBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingBoard_MouseDown);
			this.drawingBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingBoard_MouseMove);
			this.drawingBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingBoard_MouseUp);
			// 
			// buttonExit
			// 
			this.buttonExit.BackColor = System.Drawing.Color.Transparent;
			this.buttonExit.BackgroundImage = global::Backpropagation.Properties.Resources.Exit;
			this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonExit.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.buttonExit.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonExit.FlatAppearance.BorderSize = 0;
			this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonExit.Location = new System.Drawing.Point(612, 0);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(32, 32);
			this.buttonExit.TabIndex = 0;
			this.buttonExit.UseVisualStyleBackColor = false;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			this.buttonExit.MouseEnter += new System.EventHandler(this.buttonExit_MouseEnter);
			this.buttonExit.MouseLeave += new System.EventHandler(this.buttonExit_MouseLeave);
			// 
			// panelIcon
			// 
			this.panelIcon.BackgroundImage = global::Backpropagation.Properties.Resources.NN;
			this.panelIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panelIcon.Location = new System.Drawing.Point(0, 0);
			this.panelIcon.Margin = new System.Windows.Forms.Padding(0);
			this.panelIcon.Name = "panelIcon";
			this.panelIcon.Size = new System.Drawing.Size(159, 92);
			this.panelIcon.TabIndex = 0;
			// 
			// Main
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.drawingBoard);
			this.Controls.Add(this.panelSlider);
			this.Controls.Add(this.titleBar);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "Main";
			this.Text = "Main";
			this.Load += new System.EventHandler(this.Main_Load);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.titleBar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.drawingBoard)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private FlowLayoutPanel flowLayoutPanel1;
		private Button buttonExit;
		private Panel titleBar;
		private Panel panelIcon;
		private Button buttonParams;
		private Button buttonTestSet;
		private Button buttonTrain;
		private Button buttonTest;
		private Panel panelSlider;
		private PictureBox drawingBoard;
	}
}

