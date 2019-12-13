/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:38 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Animation.
	/// </summary>
	public class Animation
	{
		private PictureBox pictureBox;
		private Bitmap bmp;
		private List<Prey> listPrey;
		private List<Predator> listPredator;
		private Bitmap bmpBack;
		private Bitmap animateBmp;
		private Bitmap[] prey;
		private Bitmap predator;
		private Graphics graphics;
		private Graph graph;
		private Font font;
		private Pen p;
		
		public Animation(PictureBox p,Bitmap bmp, List<Prey> lp, List<Predator> ldp, Graph graph)
		{
			this.bmp = bmp;
			this.pictureBox = p;
			this.bmpBack = new Bitmap(bmp); 
			p.BackgroundImageLayout = ImageLayout.Zoom;
			BackgroundImage();
			this.animateBmp = new Bitmap(bmpBack.Width,bmpBack.Height);
			this.pictureBox.Image = this.animateBmp;
			this.graphics = Graphics.FromImage(animateBmp);
			this.graph = graph;
			listPrey = lp;
			listPredator = ldp;
			prey = new Bitmap[3];
			prey[0] = new Bitmap(Image.FromFile("C:\\Users\\oscar\\OneDrive\\Escritorio\\Algoritmia\\Images\\prey.png"));
			prey[1] = new Bitmap(Image.FromFile("C:\\Users\\oscar\\OneDrive\\Escritorio\\Algoritmia\\Images\\prey2.png"));
			prey[2] = new Bitmap(Image.FromFile("C:\\Users\\oscar\\OneDrive\\Escritorio\\Algoritmia\\Images\\prey3.png"));
			predator = new Bitmap(Image.FromFile("C:\\Users\\oscar\\OneDrive\\Escritorio\\Algoritmia\\Images\\predator.png"));
		 	this.p = new Pen(Color.Red,1);
		 	font = new Font("Times New Roman",50);
		}
		
		private void Refresh(){
			if(pictureBox.InvokeRequired){
				pictureBox.Invoke(new Action( () => pictureBox.Image = animateBmp));
				pictureBox.Invoke(new Action( () => pictureBox.Refresh()));
			}else{
				pictureBox.Image=animateBmp;
				pictureBox.Refresh();
			}
		}
		
		private void BackgroundImage(){
			if(pictureBox.InvokeRequired){
				pictureBox.Invoke(new Action( () => pictureBox.BackgroundImage = bmpBack));
		
			}else{
				pictureBox.BackgroundImage = bmpBack;
			}
		}
		
		public void ShowPrey(Circle c,int i){
			int aux = 65+i;
			i = i%3;
			graphics.DrawImage(prey[i],c.Center.X-50,c.Center.Y-50,100,100);
			graphics.DrawString(char.ConvertFromUtf32(aux),font,Brushes.Blue,c.Center.X,c.Center.Y);
			Refresh();
		}
		
		public void ShowPredator(Predator c){
			graphics.DrawImage(predator,c.X-50,c.Y-50,100,100);
			graphics.DrawEllipse(p,c.X-c.reach/2,c.Y-c.reach/2,c.reach,c.reach);
			if(c.prey != null)
					graphics.DrawString(char.ConvertFromUtf32(c.prey.id),font,Brushes.Red,c.X,c.Y);
			Refresh();
		}
		
		private bool DrawAllPrey(){
			int j = 0;
			bool band = true;
			bool game = true;
			for(int i=0;i<listPrey.Count;i++){
				j = i%3;
				band = listPrey[i].Move();
				if(!band)
					game=false;
				graphics.DrawImage(prey[j],listPrey[i].X,listPrey[i].Y,100,100);
				graphics.DrawString(char.ConvertFromUtf32(listPrey[i].id),font,Brushes.Blue,listPrey[i].X+40,listPrey[i].Y+40);
				
			}
			return game;
		}
		
		private void DrawAllPredator(){
			for(int i = 0; i<listPredator.Count;i++){
				listPredator[i].MoveTo();
				p.Width = 1;
				graphics.DrawEllipse(p,listPredator[i].X+50-listPredator[i].reach/2,listPredator[i].Y+50-listPredator[i].reach/2,listPredator[i].reach,listPredator[i].reach);
				p.Width = 3;
				graphics.DrawLine(p, listPredator[i].X+50,listPredator[i].Y+50,listPredator[i].Arrow.X,listPredator[i].Arrow.Y);
				graphics.DrawImage(predator,listPredator[i].X,listPredator[i].Y,100,100);
				if(listPredator[i].prey != null)
					graphics.DrawString(char.ConvertFromUtf32(listPredator[i].prey.id),font,Brushes.Red,listPredator[i].X+40,listPredator[i].Y+40);
				
			}
		}
		
		public void StartGame(){
			bool band=true,game = true;
			while(game){
				if(listPrey.Count==0){
					game = false;
					graphics.Clear(Color.Transparent);
					DrawAllPredator();
					Refresh();
				}
				else{
					graphics.Clear(Color.Transparent);
					band = DrawAllPrey();
					DrawAllPredator();
					DeletePreyDeads();
					Refresh();
					if(!band){
						game = false;
					}
				}
			}
		}
		
		public void DeletePreyDeads(){
			for(int i = 0;i<listPrey.Count;i++){
				if(!listPrey[i].live()){
					listPrey[i].Delete();
					listPrey.Remove(listPrey[i]);
				}
			}
		}
		
	}
}
