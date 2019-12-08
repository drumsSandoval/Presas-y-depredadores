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
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(788, 551);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseClick);
			// 
			// buttonLoadStage
			// 
			this.buttonLoadStage.Location = new System.Drawing.Point(806, 12);
			this.buttonLoadStage.Name = "buttonLoadStage";
			this.buttonLoadStage.Size = new System.Drawing.Size(106, 36);
			this.buttonLoadStage.TabIndex = 1;
			this.buttonLoadStage.Text = "Load Stage";
			this.buttonLoadStage.UseVisualStyleBackColor = true;
			this.buttonLoadStage.Click += new System.EventHandler(this.ButtonLoadStageClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(924, 608);
			this.Controls.Add(this.buttonLoadStage);
			this.Controls.Add(this.pictureBox);
			this.Name = "MainForm";
			this.Text = "Presas_Depredadores";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
