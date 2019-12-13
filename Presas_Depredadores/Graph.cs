/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:36 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Graph.
	/// </summary>
	public class Graph
	{
		public List<Node> Vertex{get;set;}
		public Bitmap bmp{get;set;}
		private List<Color> colors;
		public Graph(){
			this.Vertex = new List<Node>();
			
		}
		
		public Graph(List<Circle> l, Bitmap bmp){
			this.Vertex = new List<Node>();
			colors = new List<Color>();
			colors.Add(Color.White);
			colors.Add(Color.Purple);
			colors.Add(Color.DarkBlue);
			colors.Add(Color.Yellow);
			this.bmp = new Bitmap(bmp);
			for(int i = 0; i<l.Count;i++){
				Node node = new Node(l[i]);
				this.Vertex.Add(node);
			}
			if(l.Count > 1)
				ConnectGraphBmp();
			
			Graphics GFX = Graphics.FromImage(this.bmp);
			Font font = new Font("Times New Roman",20);
			for(int i = 1; i<=l.Count;i++){
				GFX.DrawString(i.ToString(),font,Brushes.Black,l[i-1].Right.X,l[i-1].Right.Y);
			}
		}
		
		public void ConnectGraphBmp(){
			int contEdges = 0;
			double Weight;
			for(int i = 0; i<Vertex.Count;i++){
				List<Point> road;
				for(int j=i+1; j<Vertex.Count;j++){
					road = new List<Point>();
					if(Bresenham(Vertex[i],Vertex[j],road)){
						Weight = Algorithms.GetDistance(Vertex[i].circle.Center,Vertex[j].circle.Center);
						Vertex[i].Edges.Add(new Edge(Vertex[i],Vertex[j],++contEdges,road,Weight));
						List<Point> lr = new List<Point>(road);
						lr.Reverse();
						Vertex[j].Edges.Add(new Edge(Vertex[j],Vertex[i],contEdges,lr,Weight));
						DrawLine(road,bmp);
					}
				}
			}
		
		
		}
		
		private bool Bresenham(Node origin, Node destination, List<Point> road){
			// Calculamos la distancia de la linea tanto para x y para y 
			int w = destination.circle.Center.X - origin.circle.Center.X;
			int h = destination.circle.Center.Y - origin.circle.Center.Y;
			int x, y;
			x = origin.circle.Center.X; 
			y = origin.circle.Center.Y;
			int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
			// Incrementamos para las secciones de avanze inclinado
			dx1 = (w < 0) ? -1 : 1;
			dy1 = (h < 0) ? -1 : 1;
			// Incrementamos para las secciones de avanze recto
			dx2 = (w < 0) ? -1 : 1;
			int longest = Math.Abs(w);
			int shortest = Math.Abs(h);
			//  cuando la distancia de y es mayor que la de x, intercambiamos los valores 
			if(!(longest > shortest)){
				longest = Math.Abs(h);
				shortest = Math.Abs(w);
				dy2 = (h < 0) ? -1 : 1;
				dx2 = 0;
			}
			int numerator = longest >> 1;
			for(int i = 0; i<=longest; i++){
				road.Add(new Point(x,y));
				numerator+=shortest;
				if(!(numerator < longest)){
					numerator-=longest;  
					x+=dx1;           // x aumenta en inclinado
					y+=dy1;          // Y aumenta en inclinado
				}else{
					x+=dx2;       // X aumenta recto
					y+=dy2;       // Y aumenta recto
				}
			}
			if(origin.circle.Id == 27 && destination.circle.Id == 58){
				return isValidRoad(road);
			}
			return isValidRoad(road);
		}
		
		private bool isValidRoad(List<Point> l){
			int i=0;
			Color c;
			int circlesCount=0;
			while(i<l.Count){
				c=bmp.GetPixel(l[i].X,l[i].Y);
				if(c.ToArgb() == Color.Purple.ToArgb()){
					i++; int j=0;
					while(c.ToArgb() == Color.Purple.ToArgb()){
						if(i==l.Count)
							return true;
						if(j>602){
							return false;
						}
						c=bmp.GetPixel(l[i].X,l[i].Y);
						i++;
						j++;
					}
					if(j<3){}
					else{
						circlesCount++;
						if(circlesCount>1){
							l.Clear();
							return false;
						}	
					}
				}
				if(!isValidColor(c)){
					l.Clear();
					return false;
				}
				i++;
			}
			return true;
		}
		
		private bool isValidColor(Color color){
			foreach(Color c in colors){
				if(c.ToArgb() == color.ToArgb()){
					return true;
				}
			}
			
			return Circle.isNoisy(color);
		}
		
		public static void DrawLine(List<Point> l, Bitmap bmp){
			for(int i=0;i<l.Count;i++){
				if(bmp.GetPixel(l[i].X,l[i].Y).ToArgb() != Color.Purple.ToArgb())
					bmp.SetPixel(l[i].X,l[i].Y,Color.DarkBlue);
			}
		}
		
		public void ClearMarks(){
			foreach(Node n in Vertex){
				n.ClearMarks();
			}
		}
		
	}
}
