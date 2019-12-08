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
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Prey.
	/// </summary>
	public class Prey
	{	
		private List<Node> listVertex;
		private int id; 
		private Node startingPoint;
		private Node current;
		private Node objective;
		private List<Dijkstra> V_Dijkstra;
		public Prey(List<Node> listVertex, int id,Node startingPoint,Node objective)
		{
			this.id = id;
			this.startingPoint = startingPoint;
			this.listVertex = listVertex;
			this.objective = objective;
			this.current = startingPoint;
			V_Dijkstra = Algorithms.Dijkrstra(startingPoint,listVertex);
		}
		
	}
}
