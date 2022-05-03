//Date: 30/11/2021
namespace Actividad_6___Depredador_y_Presa
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBoxImg = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonObjective = new System.Windows.Forms.RadioButton();
			this.radioButtonPredator = new System.Windows.Forms.RadioButton();
			this.radioButtonPrey = new System.Windows.Forms.RadioButton();
			this.buttonImg = new System.Windows.Forms.Button();
			this.buttonGraph = new System.Windows.Forms.Button();
			this.buttonAnimation = new System.Windows.Forms.Button();
			this.trackBarRadar = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonReset = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarRadar)).BeginInit();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(380, 33);
			this.label1.TabIndex = 0;
			this.label1.Text = "Depredadores y Presas";
			// 
			// pictureBoxImg
			// 
			this.pictureBoxImg.Location = new System.Drawing.Point(13, 50);
			this.pictureBoxImg.Name = "pictureBoxImg";
			this.pictureBoxImg.Size = new System.Drawing.Size(536, 535);
			this.pictureBoxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxImg.TabIndex = 1;
			this.pictureBoxImg.TabStop = false;
			this.pictureBoxImg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImgMouseClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonObjective);
			this.groupBox1.Controls.Add(this.radioButtonPredator);
			this.groupBox1.Controls.Add(this.radioButtonPrey);
			this.groupBox1.Location = new System.Drawing.Point(554, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(215, 122);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Insertar: ";
			// 
			// radioButtonObjective
			// 
			this.radioButtonObjective.Location = new System.Drawing.Point(6, 81);
			this.radioButtonObjective.Name = "radioButtonObjective";
			this.radioButtonObjective.Size = new System.Drawing.Size(104, 24);
			this.radioButtonObjective.TabIndex = 2;
			this.radioButtonObjective.TabStop = true;
			this.radioButtonObjective.Text = "Objetivo";
			this.radioButtonObjective.UseVisualStyleBackColor = true;
			// 
			// radioButtonPredator
			// 
			this.radioButtonPredator.Location = new System.Drawing.Point(6, 51);
			this.radioButtonPredator.Name = "radioButtonPredator";
			this.radioButtonPredator.Size = new System.Drawing.Size(113, 24);
			this.radioButtonPredator.TabIndex = 1;
			this.radioButtonPredator.TabStop = true;
			this.radioButtonPredator.Text = "Depredador";
			this.radioButtonPredator.UseVisualStyleBackColor = true;
			// 
			// radioButtonPrey
			// 
			this.radioButtonPrey.Location = new System.Drawing.Point(6, 21);
			this.radioButtonPrey.Name = "radioButtonPrey";
			this.radioButtonPrey.Size = new System.Drawing.Size(104, 24);
			this.radioButtonPrey.TabIndex = 0;
			this.radioButtonPrey.TabStop = true;
			this.radioButtonPrey.Text = "Presa";
			this.radioButtonPrey.UseVisualStyleBackColor = true;
			// 
			// buttonImg
			// 
			this.buttonImg.Location = new System.Drawing.Point(556, 142);
			this.buttonImg.Name = "buttonImg";
			this.buttonImg.Size = new System.Drawing.Size(213, 72);
			this.buttonImg.TabIndex = 3;
			this.buttonImg.Text = "Abrir Imagen";
			this.buttonImg.UseVisualStyleBackColor = true;
			this.buttonImg.Click += new System.EventHandler(this.ButtonImgClick);
			// 
			// buttonGraph
			// 
			this.buttonGraph.Location = new System.Drawing.Point(556, 220);
			this.buttonGraph.Name = "buttonGraph";
			this.buttonGraph.Size = new System.Drawing.Size(213, 72);
			this.buttonGraph.TabIndex = 4;
			this.buttonGraph.Text = "Generar Grafo";
			this.buttonGraph.UseVisualStyleBackColor = true;
			this.buttonGraph.Click += new System.EventHandler(this.ButtonGraphClick);
			// 
			// buttonAnimation
			// 
			this.buttonAnimation.Location = new System.Drawing.Point(555, 298);
			this.buttonAnimation.Name = "buttonAnimation";
			this.buttonAnimation.Size = new System.Drawing.Size(213, 72);
			this.buttonAnimation.TabIndex = 5;
			this.buttonAnimation.Text = "Iniciar Recorrido";
			this.buttonAnimation.UseVisualStyleBackColor = true;
			this.buttonAnimation.Click += new System.EventHandler(this.ButtonAnimationClick);
			// 
			// trackBarRadar
			// 
			this.trackBarRadar.Location = new System.Drawing.Point(560, 376);
			this.trackBarRadar.Name = "trackBarRadar";
			this.trackBarRadar.Size = new System.Drawing.Size(209, 56);
			this.trackBarRadar.SmallChange = 5;
			this.trackBarRadar.TabIndex = 5;
			this.trackBarRadar.Scroll += new System.EventHandler(this.TrackBarRadarScroll);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(596, 435);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Tamaño del Radar";
			// 
			// buttonReset
			// 
			this.buttonReset.BackColor = System.Drawing.Color.OrangeRed;
			this.buttonReset.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.buttonReset.Location = new System.Drawing.Point(556, 503);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(213, 72);
			this.buttonReset.TabIndex = 8;
			this.buttonReset.Text = "Reiniciar Imagen";
			this.buttonReset.UseVisualStyleBackColor = false;
			this.buttonReset.Click += new System.EventHandler(this.ButtonResetClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(781, 597);
			this.Controls.Add(this.buttonReset);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.trackBarRadar);
			this.Controls.Add(this.buttonAnimation);
			this.Controls.Add(this.buttonGraph);
			this.Controls.Add(this.buttonImg);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBoxImg);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "Actividad 6 - Depredador y Presa";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBarRadar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trackBarRadar;
		private System.Windows.Forms.Button buttonAnimation;
		private System.Windows.Forms.Button buttonGraph;
		private System.Windows.Forms.Button buttonImg;
		private System.Windows.Forms.RadioButton radioButtonPrey;
		private System.Windows.Forms.RadioButton radioButtonPredator;
		private System.Windows.Forms.RadioButton radioButtonObjective;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox pictureBoxImg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}
