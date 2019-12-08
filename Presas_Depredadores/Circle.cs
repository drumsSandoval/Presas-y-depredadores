/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:36 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Collections.Generic;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Circle.
	/// </summary>
	public class Circle
	{
		public Point Center {get; set;}
		public double Radio {get;set;}
		public int Id {get;set;}
		public Point Top{get;set;}
		public Point Down{get;set;}
		public Point Right{get;set;}
		public Point Left{get;set;}
		public Circle(){}
		
		public override string ToString(){
			return string.Format("[Circle  Id={2}, Center={0}, Radio={1}]", Center, Radio, Id);
		}
		
		private bool isCircle(){
			int diametro_height = 0;
			int diametro_width = 0;
			for(int i = this.Top.Y; i<=this.Down.Y;i++)
				diametro_height++;
			for(int i = this.Left.X;i<=this.Right.X;i++)
				diametro_width++;
			int diference = Math.Abs(diametro_height - diametro_width);
			if(diference>10)
				return false;
			return true;
		}
		
		public static void DrawCircle(Circle circle,Color color,Bitmap CloneBmp){
			for(int y = circle.Top.Y-1;y<=circle.Down.Y+1;y++){
				int x_der = circle.Top.X;
				int x_izq = circle.Top.X-1;
				while(CloneBmp.GetPixel(x_der,y).ToArgb() != Color.White.ToArgb() && CloneBmp.GetPixel(x_der,y).ToArgb() != Color.DarkBlue.ToArgb()){
					CloneBmp.SetPixel(x_der,y,color);
					x_der++;
				}
				while(CloneBmp.GetPixel(x_izq,y).ToArgb() != Color.White.ToArgb()&& CloneBmp.GetPixel(x_izq,y).ToArgb() != Color.DarkBlue.ToArgb()){
					CloneBmp.SetPixel(x_izq,y,color);
					x_izq--;
				}
			}
		}
		
		private static  Circle FindCenter(int x_izq,int y_sup,int counterCircles, Bitmap CloneBmp){
			Circle circle = new Circle();
			int x_der = x_izq;
			int c;
			while(isBlack(CloneBmp.GetPixel(x_der,y_sup)))
				x_der++;
			circle.Top = new Point((x_der + x_izq)/2,y_sup);
			while(isBlack(CloneBmp.GetPixel(circle.Top.X,y_sup)))
				y_sup++;
			circle.Down = new Point(circle.Top.X, y_sup);
			c = (circle.Top.Y+circle.Down.Y)/2;
			x_izq = circle.Top.X;
			while(isBlack(CloneBmp.GetPixel(x_izq,c)))
				x_izq--;
			circle.Left = new Point(x_izq,c);
			x_der = circle.Top.X;
			while(isBlack(CloneBmp.GetPixel(x_der,c)))
				x_der++;
			circle.Right = new Point(x_der,c);
			
			circle.Center = new Point((circle.Left.X+circle.Right.X)/2,c);
			circle.Radio = Algorithms.GetDistance(circle.Center,circle.Left);
			if(circle.isCircle()){
				Circle.DrawCircle(circle,Color.Purple,CloneBmp);
				circle.Id = counterCircles;
				return circle;
			}
			Circle.DrawCircle(circle,Color.White,CloneBmp);
			return null;
		}
		
		private static bool isBlack(Color color){
			if(color.R == 0)
				if(color.G == 0)
					if(color.B == 0)
						return true;
			return false;
		}
		
		public static bool isNoisy(Color color){
			if(color.R == 223)
				if(color.G == 223)
					if(color.B == 223)
						return true;
			return false;
		}
		
		public static List<Circle> FindCircles(Bitmap bmp){
			List<Circle> list_circles = new List<Circle>();
			int counterCircles = 0;
			Circle circle;
			Color newColor = Color.FromArgb(244, 000, 000);
			// Iteramos pixel por pixel sobre la imagen
			for(int y=0; y<bmp.Height; y++)
           		for(int x=0; x<bmp.Width; x++)
					if(Circle.isBlack(bmp.GetPixel(x,y))){
						circle = Circle.FindCenter(x,y,++counterCircles,bmp);
						if(circle != null)
							list_circles.Add(circle);
						else
							counterCircles--;
						
					}	
			return list_circles;
		}

	}
}
