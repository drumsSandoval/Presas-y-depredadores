/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:39 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Algorithms.
	/// </summary>
	public class Algorithms
	{
		public static Point GetPicturePosition(int x,int y,PictureBox picture, Bitmap bmp){
			double w_p,w_b,h_p,h_b;int click_x,click_y;
			double k_x, k_y, k;
			int incX=0, incY=0;
			w_p = picture.Width;
			w_b = bmp.Size.Width;
			h_p = picture.Height;
			h_b = bmp.Size.Height;
			
			k_x =  w_p /  w_b;
			k_y = h_p / h_b;
			
			if(k_y < k_x){
				k = k_y;
				incX = (int)Math.Round((w_p-(w_b*k))/2);
			}else{
				k = k_x;
				incY = (int)Math.Round((h_p-(h_b*k))/2);
			}
			click_x = (int)Math.Round((x-incX)/k);
			click_y = (int)Math.Round((y-incY)/k);
			return new Point(click_x,click_y);
		}
		
		static public  int existNode(List<Circle> list,Point click){
			int i = 0;
			foreach(Circle c in list){
				double xy = Math.Pow((click.X-c.Center.X),2) + Math.Pow(click.Y-c.Center.Y,2);
				double r2= Math.Pow(c.Radio,2);
				if((xy - r2) <= 0){
					return i;
				}
				i++;
			}
			return -1;
		}
		
		public static double GetDistance( Point v1, Point v2 ){
            int distX = Math.Abs((v1.X- v2.X));
            int distY = Math.Abs((v1.Y - v2.Y));
            return Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
        }
		
// DIJKSTRA
		public static List<Dijkstra> Dijkrstra(Node v_i,List<Node> Vertex){
			List<Dijkstra> V_Dijkstra = new List<Dijkstra>();
			for(int i=0;i<Vertex.Count;i++){
				if(v_i == Vertex[i]){
					V_Dijkstra.Add(new Dijkstra(Vertex[i]));
					V_Dijkstra[i].weight = 0;
				}
				else
					V_Dijkstra.Add(new Dijkstra(Vertex[i]));
			}
			Dijkstra(V_Dijkstra);
			return V_Dijkstra;
		}
		private static void Dijkstra(List<Dijkstra> V_Dijkstra){
			Dijkstra elem;
			while(!Solution(V_Dijkstra)){
				elem = SelectDefinitive(V_Dijkstra);
				UpdateDijkstra(V_Dijkstra,elem);
			}
			
		}
		private static bool Solution(List<Dijkstra> a_d){
			foreach(Dijkstra d in a_d){
				if(!d.finish)
					return false;
			}
			return true;
		}
		private static Dijkstra SelectDefinitive(List<Dijkstra> V_Dijkstra){
			Dijkstra aux=null;
			double weight = double.MaxValue;
			foreach(Dijkstra d in V_Dijkstra){
				if(!d.finish)
					if(weight >= d.weight){
						aux = d;
						weight = d.weight;
					}
			}
			aux.finish = true;
			return aux;
		}
		private static void UpdateDijkstra(List<Dijkstra> V_Dijkstra,Dijkstra elem){
			if(elem.weight == double.MaxValue)
				return;
			Dijkstra aux;
			foreach(Edge e in elem.vertex.Edges){
				aux = V_Dijkstra.Find(d => d.vertex == e.Destino);
			 	if(aux.weight > elem.weight+e.Weight){
					aux.weight = e.Weight+elem.weight;
					aux.father = e.Origen;
				}
			}
		}
		public static List<int> GetWay(Node v_i, Node v_d, List<Dijkstra>l){
			Dijkstra d = l.Find(e => e.vertex == v_d);
			if(d.weight == double.MaxValue){
				return null;
			}
			List<int> way = new List<int>();
			way.Add(d.vertex.circle.Id);
			while(d.vertex != v_i){
				d = l.Find(e => e.vertex == d.father);
				way.Add(d.vertex.circle.Id);
			}
			way.Reverse();
			return way;
		}
	

//  Fin Dijsktra
		
	}
	
	public class Dijkstra{
		public Node vertex{get;set;}
		public double weight{get;set;}
		public Node father{get;set;}
		public bool finish{get;set;}
		public Dijkstra(Node vertex){
			this.vertex=vertex;
			this.weight= double.MaxValue;
			this.father = null;
			this.finish=false;
		}
	}
}
