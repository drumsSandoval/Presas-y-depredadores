/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 09/12/2019
 * Time: 04:41 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Thougth.
	/// </summary>
	public class Thougth
	{
		private List<Prey> listPrey{get;set;}
		private List<Predator> listPredator{get;set;}
		public Thougth(List<Prey> listPrey, List<Predator> listPredator)
		{
			this.listPrey = listPrey;
			this.listPredator = listPredator;
		}
		
		public Tuple<int,int>  RadarPredator(Predator predator){
			if(predator.prey == null){
				foreach(Prey prey in listPrey){
					double xy = Math.Pow((prey.X-predator.X),2) + Math.Pow(prey.Y-predator.Y,2);
					double r2= Math.Pow((predator.reach/2)+50,2);
					if((xy - r2) <= 0)
						return new Tuple<int,int>(prey.X+50,prey.Y+50);
				}
				return null;
			}
			foreach(Prey prey in listPrey){
					double xy = Math.Pow((prey.X+50-predator.X+50),2) + Math.Pow(prey.Y+50-predator.Y+50,2);
					double r2= Math.Pow((predator.reach/2)+50,2);
					if(prey.id == predator.prey.id)
						if((xy - r2) <= 0)
							return new Tuple<int,int>(prey.X+50,prey.Y+50);
				}
				return null;
		}
			
		public bool ChangeWay(Edge e){
			foreach(Predator p in listPredator){
				if(p.current == e.Destino)
					return false;
				else if(p.edge != null)
					if(p.edge.Destino == e.Destino)
						return false;
			}
			return true;
		}
		
		public bool KeepWalkingEdge(Edge e){
			foreach(Predator p in listPredator)
				if(p.edge  == null)
					return true;
				else if(e.Origen == p.edge.Destino && e.Destino == p.edge.Origen)
					return false;
				else if(e.Destino == p.edge.Destino && e != p.edge)
					return false;
				else if(e.Destino == p.edge.Origen)
					return false;
			return true;
		}
		
		public bool BitePrey(Prey prey,Predator predator){
			if(prey == null)
				return false;
			double xy = Math.Pow((prey.X-predator.X),2) + Math.Pow(prey.Y-predator.Y,2);
			double r2= Math.Pow(30,2);
			if((xy - r2) <= 0)
				return true;
			return false;
		}
	}
}
