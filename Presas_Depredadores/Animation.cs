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
		}
		
		public void Refresh(){
			if(pictureBox.InvokeRequired){
				pictureBox.Invoke(new Action( () => pictureBox.Image = animateBmp));
				pictureBox.Invoke(new Action( () => pictureBox.Refresh()));
			}else{
				pictureBox.Image=animateBmp;
				pictureBox.Refresh();
			}
		}
		
		public void BackgroundImage(){
			if(pictureBox.InvokeRequired){
				pictureBox.Invoke(new Action( () => pictureBox.BackgroundImage = bmpBack));
		
			}else{
				pictureBox.BackgroundImage = bmpBack;
			}
		}
		
		public void ShowPrey(Circle c,int i){
			graphics.DrawImage(prey[i],c.Center.X-50,c.Center.Y-50,100,100);
			Refresh();
		}
		
		public void ShowPredator(Circle c){
			graphics.DrawImage(predator,c.Center.X-50,c.Center.Y-50,100,100);
			Refresh();
		}
		
	}
}
