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
	/// Description of Edge.
	/// </summary>
	public class Edge
	{
		public Node Destino{get;set;}
		public Node Origen{get;set;}
		public List<Point> Road{get;set;}
		public int Id{get;set;}
		public double Angle{get;set;}
		public double Weight{get;set;}
		public Edge(){}
		public Edge(Node v1,Node v2, int id, List<Point> road, double Weight)
		{
			this.Origen = v1;
			this.Destino = v2;
			this.Id = id;
			this.Road = road;
			this.Weight = Weight;
			this.SetAngle();
		}
		public override string ToString()
		{
			return string.Format("[Edge Id Destino={0}, Id Arista={1}]", Destino.circle.Id, Id);
		}
		public void SetAngle(){
			Angle = Math.Atan2(Destino.circle.Center.Y-Origen.circle.Center.Y , Destino.circle.Center.X-Origen.circle.Center.X);
			if(Angle < 0 ){
				Angle+=2*Math.PI;
			}
		}
	

	}
}