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
		private bool first;
		public Thougth thougth;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			listPredator = new List<Predator>();
			listPrey = new List<Prey>();
			thougth  = new Thougth(listPrey,listPredator);
			pictureBox.Image = Bitmap.FromFile("C:\\Users\\oscar\\OneDrive\\Escritorio\\Algoritmia\\Images\\picture.png");
			first = false;
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
				pictureBox.BackgroundImage = null;
				pictureBox.Image = graph.bmp;
				idPrey =0;
				listPrey.Clear();
				idPredator = 0;
				listPredator.Clear();
				animation = null;
				listViewPrey.Items.Clear();
				listViewPredator.Items.Clear();
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
				Prey p = new Prey(graph.Vertex,idPrey,graph.Vertex[i],listViewPrey,thougth);
				listPrey.Add(p);
				ListViewItem l = new ListViewItem(char.ConvertFromUtf32(p.id));
				l.SubItems.Add(p.Resistance.ToString());
				listViewPrey.Items.Add(l);
				
	
			}else if(e.Button == MouseButtons.Right){
				if(animation == null)
					animation = new Animation(pictureBox,graph.bmp,listPrey,listPredator,graph);
				Predator p = new Predator(graph.Vertex[i],++idPredator,thougth,listViewPredator);
				listPredator.Add(p);
				ListViewItem l = new ListViewItem(idPredator.ToString());
				int aux = p.SelectPrey(listPrey);
				if(aux != -1){
					l.SubItems.Add(char.ConvertFromUtf32(aux));
				}else
					l.SubItems.Add("--");
				animation.ShowPredator(p);
				listViewPredator.Items.Add(l);
			}
		}
		
		void ButtonStartGameClick(object sender, EventArgs e)
		{
			if(listPrey.Count == 0)
				return;
			bool band;
			int i =-1;
			do{
				band = false;
				string vertex = Microsoft.VisualBasic.Interaction.InputBox("Ingrese un vertice objetivo","Vertice Objetivo");
				if(vertex == ""){
					MessageBox.Show("Selecciona un vertice objetivo","Error");
				}
				try{
					i = int.Parse(vertex);
				}catch(System.FormatException ){
					band = true;
				}
			}while(band);
			Node aux = graph.Vertex.Find((n)=> n.circle.Id == i);
			if(aux == null){
				MessageBox.Show("Vertice objetivo no encontrado","Error");
			}else{
				
				foreach(Prey p in listPrey){
					p.SetObjective(aux);
				}
				if(first){
					foreach(Predator p in listPredator){
						p.SelectPrey(listPrey);
						animation.ShowPredator(p);
					}
				}
				first =true;
				animation.StartGame();
			}
		}
		
		
		
	}
}
