/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:35 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Bitmap bmp;
		private List<Circle> listCircles;
		private List<Predator> listPredator;
		private List<Prey> listPrey;
		private Graph graph;
		private Thread thead;
		private Animation animation;
		private int idPrey, idPredator;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			listPredator = new List<Predator>();
			listPrey = new List<Prey>();
		}
		
		void ButtonLoadStageClick(object sender, EventArgs e)
		{
			OpenFileDialog openArchive = new OpenFileDialog();
			if(openArchive.ShowDialog() == DialogResult.OK){
				bmp = new Bitmap(openArchive.FileName);
			}
			if(bmp != null){
				listCircles = Circle.FindCircles(bmp);
				graph = new Graph(listCircles,bmp);
				pictureBox.Image = graph.bmp;
				idPrey =0;
				listPrey.Clear();
				idPredator = 0;
				listPredator.Clear();
				animation = null;
			}else
				MessageBox.Show("Seleccione una imagen");
		}
		void PictureBoxMouseClick(object sender, MouseEventArgs e)
		{
			Point click =  Algorithms.GetPicturePosition(e.X,e.Y,pictureBox,graph.bmp);
			int i = Algorithms.existNode(listCircles,click);
			if(i == -1){
				return;
			}
			else if(e.Button == MouseButtons.Left){
				if(animation == null)
					animation = new Animation(pictureBox,graph.bmp,listPrey,listPredator,graph);
				animation.ShowPrey(listCircles[i],idPrey++);
	
			}else if(e.Button == MouseButtons.Right){
				if(animation == null)
					animation = new Animation(pictureBox,graph.bmp,listPrey,listPredator,graph);
				animation.ShowPredator(listCircles[i]);
			}
		}	
		
		
	}
}
