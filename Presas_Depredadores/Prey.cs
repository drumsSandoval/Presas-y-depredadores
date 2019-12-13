/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:41 a. m.
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
	/// Description of Prey.
	/// </summary>
	public class Prey
	{	
		private List<Node> listVertex;
		public int id{get;set;}
		public int X{get;set;}
		public int Y{get;set;}
		private List<int> Way;
		public Predator predator{get;set;}
		public Edge edge{get;set;}
		private Node startingPoint;
		private Node current;
		private  Node objective;
		private int contWay;
		public int contRoad {get;set;}
		private int speed;
		public int Resistance{get;set;}
		private List<Dijkstra> V_Dijkstra;
		private bool bandReturn;
		public ListView listView{get;set;}
		private Thougth thougth;
		private bool pivotea;
		public Prey(List<Node> listVertex, int id,Node startingPoint,ListView lw,Thougth thougth)
		{
			this.speed = 10;
			this.id = 64 + id;
			this.contRoad = 0;
			this.startingPoint = startingPoint;
			this.listVertex = listVertex;
			this.current = startingPoint;
			this.Resistance = 4;
			this.listView = lw;
			this.thougth = thougth;
			pivotea = false;
			bandReturn = false;
		}
		
		public void  SetObjective(Node objective){
			V_Dijkstra = Algorithms.Dijkrstra(startingPoint,listVertex);
			this.objective = objective;
			Way = Algorithms.GetWay(startingPoint,objective,V_Dijkstra);
			contWay = 0;
			if(edge == null)
				SetEdge();
		}
		
		public void SetEdge(){
			if(Way[contWay] == current.circle.Id){
				contWay++;
				SetEdge();
				return;
			}
			for(int i = 0; i<current.Edges.Count;i++){
				if(Way[contWay] == current.Edges[i].Destino.circle.Id){
					if(!thougth.ChangeWay(current.Edges[i])){
						SetEdgeWarning();
						pivotea = true;
						return;
					}
					else{
						edge = current.Edges[i];
						++contWay;
						pivotea = false;
						return;
					}
				}
			}
		}
		
		private void FlipEdge(){
			foreach(Edge e in edge.Destino.Edges){
				if(e.Origen == edge.Destino && e.Destino == edge.Origen){
					contRoad = e.Road.IndexOf(edge.Road[contRoad]);
					edge = e;
				}
			}
		}
		
		public bool Move(){
			if(edge.Road.Count>contRoad+speed){
				if(!thougth.KeepWalkingEdge(edge) && !bandReturn){
					//MessageBox.Show("Entro");
					bandReturn = true;
					FlipEdge();
					return Move();
				}else{
					contRoad+=speed;
					X = edge.Road[contRoad].X-50;
					Y = edge.Road[contRoad].Y-50;
				}
			}else{
				X = edge.Destino.circle.Center.X-50;
				Y = edge.Destino.circle.Center.Y-50;
				contRoad = 0;
				current = edge.Destino;
				startingPoint = current;
				edge = null;
				if(bandReturn){
					V_Dijkstra = Algorithms.Dijkrstra(startingPoint,listVertex);
					Way = Algorithms.GetWay(startingPoint,objective,V_Dijkstra);
					contWay = 0;
				}
				bandReturn = false;
				if(current.circle.Id == objective.circle.Id){
					Resistance++;
					UpdateListView();
					return false;
				}
				else if(pivotea){
					V_Dijkstra = Algorithms.Dijkrstra(startingPoint,listVertex);
					Way = Algorithms.GetWay(startingPoint,objective,V_Dijkstra);
					contWay = 0;
				}
				SetEdge();
			}	
			return true;
		}
		
		private void SetEdgeWarning(){
			if(current.Edges.Count == 1){
				edge = current.Edges[0];
				pivotea = false;
			}
			else{
				Random r = new Random();
				int i = r.Next(current.Edges.Count);
				while(current.Edges[i].Destino.circle.Id == Way[contWay])
					i = r.Next(current.Edges.Count);
				edge = current.Edges[i];
			}
		}
		
		public void AssingPredator(Predator p){
			predator = p;
		}
		
		public void UpdateListView(){
			foreach(ListViewItem i in listView.Items){
				if(i.Text == char.ConvertFromUtf32(this.id)){
					listView.Items.Remove(i);
					ListViewItem l = new ListViewItem(char.ConvertFromUtf32(id));
					l.SubItems.Add(Resistance.ToString());
					listView.Items.Add(l);
					listView.Refresh();
					return;
				}
			}
		}
		
		public bool live(){
			return !(Resistance == 0);
		}
		
		public void Delete(){
			this.predator.prey = null;
			predator.UpdateListView();
			predator = null;
			edge = null;
			foreach(ListViewItem i in listView.Items){
				if(i.Text == char.ConvertFromUtf32(this.id)){
					listView.Items.Remove(i);
					listView.Refresh();
					return;
				}
			}
		}
		
		public Point GetUbication(){
			return new Point(X+50,Y+50);
		}
	}
}
