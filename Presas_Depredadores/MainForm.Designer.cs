/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:35 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Presas_Depredadores
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button buttonLoadStage;
		private System.Windows.Forms.Button buttonStartGame;
		private System.Windows.Forms.ListView listViewPrey;
		private System.Windows.Forms.ColumnHeader IdPresa;
		private System.Windows.Forms.ColumnHeader Resistencia;
		private System.Windows.Forms.ListView listViewPredator;
		private System.Windows.Forms.ColumnHeader id;
		private System.Windows.Forms.ColumnHeader Presa;
		
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
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.buttonLoadStage = new System.Windows.Forms.Button();
			this.buttonStartGame = new System.Windows.Forms.Button();
			this.listViewPrey = new System.Windows.Forms.ListView();
			this.IdPresa = new System.Windows.Forms.ColumnHeader();
			this.Resistencia = new System.Windows.Forms.ColumnHeader();
			this.listViewPredator = new System.Windows.Forms.ListView();
			this.id = new System.Windows.Forms.ColumnHeader();
			this.Presa = new System.Windows.Forms.ColumnHeader();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(153, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(768, 529);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseClick);
			// 
			// buttonLoadStage
			// 
			this.buttonLoadStage.Location = new System.Drawing.Point(6, 306);
			this.buttonLoadStage.Name = "buttonLoadStage";
			this.buttonLoadStage.Size = new System.Drawing.Size(141, 57);
			this.buttonLoadStage.TabIndex = 1;
			this.buttonLoadStage.Text = "Load Stage";
			this.buttonLoadStage.UseVisualStyleBackColor = true;
			this.buttonLoadStage.Click += new System.EventHandler(this.ButtonLoadStageClick);
			// 
			// buttonStartGame
			// 
			this.buttonStartGame.Location = new System.Drawing.Point(927, 306);
			this.buttonStartGame.Name = "buttonStartGame";
			this.buttonStartGame.Size = new System.Drawing.Size(143, 57);
			this.buttonStartGame.TabIndex = 2;
			this.buttonStartGame.Text = "Start Game";
			this.buttonStartGame.UseVisualStyleBackColor = true;
			this.buttonStartGame.Click += new System.EventHandler(this.ButtonStartGameClick);
			// 
			// listViewPrey
			// 
			this.listViewPrey.BackColor = System.Drawing.SystemColors.MenuBar;
			this.listViewPrey.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.IdPresa,
			this.Resistencia});
			this.listViewPrey.Location = new System.Drawing.Point(927, 12);
			this.listViewPrey.Name = "listViewPrey";
			this.listViewPrey.Size = new System.Drawing.Size(143, 271);
			this.listViewPrey.TabIndex = 3;
			this.listViewPrey.UseCompatibleStateImageBehavior = false;
			this.listViewPrey.View = System.Windows.Forms.View.Details;
			// 
			// IdPresa
			// 
			this.IdPresa.Text = "Id Presa";
			// 
			// Resistencia
			// 
			this.Resistencia.Text = "Resistencia";
			this.Resistencia.Width = 76;
			// 
			// listViewPredator
			// 
			this.listViewPredator.BackColor = System.Drawing.SystemColors.MenuBar;
			this.listViewPredator.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.id,
			this.Presa});
			this.listViewPredator.Location = new System.Drawing.Point(6, 12);
			this.listViewPredator.Name = "listViewPredator";
			this.listViewPredator.Size = new System.Drawing.Size(141, 271);
			this.listViewPredator.TabIndex = 4;
			this.listViewPredator.UseCompatibleStateImageBehavior = false;
			this.listViewPredator.View = System.Windows.Forms.View.Details;
			// 
			// id
			// 
			this.id.Text = "Id Predador";
			this.id.Width = 69;
			// 
			// Presa
			// 
			this.Presa.Text = "Presa Asechada";
			this.Presa.Width = 83;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1082, 614);
			this.Controls.Add(this.listViewPredator);
			this.Controls.Add(this.listViewPrey);
			this.Controls.Add(this.buttonStartGame);
			this.Controls.Add(this.buttonLoadStage);
			this.Controls.Add(this.pictureBox);
			this.Name = "MainForm";
			this.Text = "Presas y Depredadores";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
