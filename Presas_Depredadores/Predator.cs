/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:41 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Predator.
	/// </summary>
	public class Predator
	{
		public int X {get;set;}
		public int Y {get;set;}
		public Edge edge{get;set;}
		private Node startingPoint;
		public Node current{get;set;}
		private double angle;
		public bool bandExcited;
		private int cont;
		private int speed;
		private Thougth thought;
		private Tuple<int,int> point;
		public int reach;
		public int Id {get;set;}
		public Point Arrow {get;set;}
		private double inc;
		public Prey prey {get;set;}
		private List<Prey> listPrey;
		public ListView listView{get;set;}
		private int sleep;
		public Predator(Node startingPoint,int id,Thougth thought, ListView lw)
		{
			speed = 15;
			this.startingPoint = startingPoint;
			this.Id = id;
			this.current = startingPoint;
			X = current.circle.Center.X;
			Y = current.circle.Center.Y;
			point = null;
		    reach = 650;
			angle = Math.PI/2;
			inc = Math.PI/8;
			this.thought = thought;
			listView = lw;
			sleep = -1;
		
		}
		
		public void MoveTo(){
			if(edge == null){
				Radar();
				if(bandExcited)
					SelectEdge();
				else
					SelectEdgeRandom();
				SelectPrey();
			}
			else if(edge.Road.Count>cont+speed){
				if(sleep != -1){
					if(sleep == 10)
						sleep = -1;
					else 
						sleep++;
				}else{
					cont+=speed;
					X = edge.Road[cont].X-50;
					Y = edge.Road[cont].Y-50;
					if(thought.BitePrey(prey,this) && sleep == -1){
						BitePrey();
					}
					Radar();
					if(bandExcited)
						excited();
					else
						quiet();
				}
			}else{
				current = edge.Destino;
				if(!bandExcited)
					SelectPrey();
				X = edge.Destino.circle.Center.X-50;
				Y = edge.Destino.circle.Center.Y-50;
				cont=0;
				if(bandExcited){
					SelectEdge();
				}
				else{
					SelectEdgeRandom();
				}
				Radar();
			}
		}
		
		private void SelectEdge(){
			int i = 0, pos = -1;
			double min=int.MaxValue, aux;
			foreach(Edge e in current.Edges){
				aux = MinAngle(e.Angle,this.angle);
				if(min> aux ){
					min = aux;
					pos = i;
				}
				i++;
			}
			edge = current.Edges[pos];
		}
		
		public double MinAngle(double e1,double e2){
			if(e1 <= Math.PI && e2 <= Math.PI){
				return Math.Abs(e2 - e1);
			}
			else if(e1 >= Math.PI && e2 >= Math.PI){
				return Math.Abs(e2 - e1);
			}
			if(e2 < Math.PI && e1 >= Math.PI){
				double aux = Math.PI*2 - e1;
				
				return Math.Abs(e2+aux);
			}
			else {
				double aux = Math.PI*2 - e2;
				return Math.Abs(e1+aux);;
			}
			
		}
		
		private void SearchPrey(){
			if(edge == null){
				angle = Math.Atan2(point.Item2 - Y+50 , point.Item1 - X+50);
				if(angle < 0 ){
					angle+=2*Math.PI;
				}
			}
			else{
				angle = Math.Atan2(point.Item2 - edge.Road[cont].Y , point.Item1 - edge.Road[cont].X);
				if(angle < 0 ){
					angle+=2*Math.PI;
				}
			}
		
		}
		
		private void SelectEdgeRandom(){
			Random r = new Random();
			int i = r.Next(current.Edges.Count);
			edge = current.Edges[i];
		}
		
		
		private void Radar(){
			if(angle+inc>= 2*Math.PI)
				angle = 0;
			angle += inc;
			point = thought.RadarPredator(this);
			if(point != null){
				bandExcited = true;
				SearchPrey();
				if(edge != null)				
					Arrow = new Point((int) Math.Round(60*Math.Cos(angle)+edge.Road[cont].X),(int) Math.Round(60*Math.Sin(angle)+edge.Road[cont].Y));
				else
					Arrow = new Point((int) Math.Round(60*Math.Cos(angle)+X+50),(int) Math.Round(60*Math.Sin(angle)+Y+50));
			}
			else{
				if(edge != null)
					Arrow = new Point((int) Math.Round(reach/2*Math.Cos(angle)+edge.Road[cont].X),(int) Math.Round(reach/2*Math.Sin(angle)+edge.Road[cont].Y));
				bandExcited = false;
			}
		}
			
		
		private void excited(){
			speed+=1;
		}
		
		private void quiet(){
			speed = 15;
		}
		
		public void  BitePrey(){
			sleep =0;
			--prey.Resistance;
			prey.UpdateListView();
		}
		
		public int SelectPrey(List<Prey> lp){
			listPrey = lp;
			if(lp.Count < Id){
				prey = null;
				return -1;
			}
			prey = lp[Id-1];
			prey.AssingPredator(this);
			return lp[Id-1].id;
		}
		
		public void UpdateListView(){
			foreach(ListViewItem i in listView.Items){
				if(i.Text == this.Id.ToString()){
					listView.Items.Remove(i);
					ListViewItem l = new ListViewItem(Id.ToString());
					l.SubItems.Add("--");
					listView.Items.Add(l);
					listView.Refresh();
					return;
				}
			}
		}
		
		public void SelectPrey(){
			double Min = double.MaxValue;
			double aux,aux2;
			Prey definitive = null;
			foreach(Prey p in listPrey){
				if(p.edge == null){
					aux = Algorithms.GetDistance(edge.Road[cont],new Point(p.X+50,p.Y+50));
				}else
					aux = Algorithms.GetDistance(edge.Road[cont],p.edge.Road[p.contRoad]);
				if(aux<Min){
					if(p.predator == null){
						Min = aux;
						definitive = p;
					}
					else{
						if(p.predator.edge == null){
							if(p.edge == null)
								aux2 = Algorithms.GetDistance(new Point(p.predator.X+50,p.predator.Y+50),new Point(p.X+50,p.Y+50));
							else
								aux2 = Algorithms.GetDistance(new Point(p.predator.X+50,p.predator.Y+50),p.edge.Road[p.contRoad]);
						}else{
							if(p.edge == null)
								aux2 = Algorithms.GetDistance(p.predator.edge.Road[p.predator.cont],new Point(p.X+50,p.Y+50));
							else
								aux2 = Algorithms.GetDistance(p.predator.edge.Road[p.predator.cont],p.edge.Road[p.contRoad]);
						}
						if(aux<aux2){
							// Swap
							Min = aux;
							definitive = p;
						}
					}
				}
				
			}
			if(definitive == null)
					return;
			else if(definitive.predator == null){
				this.prey.predator = null;
				this.prey = definitive;
				definitive.predator = this;
				foreach(ListViewItem i in listView.Items){
					if(i.Text == this.Id.ToString()){
						listView.Items.Remove(i);
						listView.Refresh();
					}
				}
				ListViewItem l = new ListViewItem(this.Id.ToString());
				l.SubItems.Add(char.ConvertFromUtf32(definitive.id));
				listView.Items.Add(l);
				listView.Refresh();
			}else if(prey == null){
				Predator pred = null;
				prey = definitive;
				if(definitive.predator != null){
					pred = definitive.predator;
					definitive.predator.prey = null;
				}
				definitive.predator = this;
				foreach(ListViewItem i in listView.Items){
					if(i.Text == this.Id.ToString())
						listView.Items.Remove(i);
					if(i.Text == pred.Id.ToString())
						listView.Items.Remove(i);
				}
				ListViewItem l = new ListViewItem(this.Id.ToString());
				l.SubItems.Add(char.ConvertFromUtf32(definitive.id));
				listView.Items.Add(l);
				l = new ListViewItem(pred.Id.ToString());
				l.SubItems.Add("--");
				listView.Items.Add(l);
				listView.Refresh();
			}
			else{
				Predator pred;
				Prey ap = prey;
				prey = definitive;
				pred = definitive.predator;
				definitive.predator.prey = ap;
				definitive. predator = this;
				ap.predator = pred;
				foreach(ListViewItem i in listView.Items){
					if(i.Text == this.Id.ToString())
						listView.Items.Remove(i);
					if(i.Text == pred.Id.ToString())
						listView.Items.Remove(i);
				}
				ListViewItem l = new ListViewItem(this.Id.ToString());
				l.SubItems.Add(char.ConvertFromUtf32(definitive.id));
				listView.Items.Add(l);
				l = new ListViewItem(pred.Id.ToString());
				l.SubItems.Add(char.ConvertFromUtf32(ap.id));
				listView.Items.Add(l);
				listView.Refresh();
		}
			
	}
		
	}
}
