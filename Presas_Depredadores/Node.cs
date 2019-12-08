/*
 * Created by SharpDevelop.
 * User: oscar
 * Date: 04/12/2019
 * Time: 12:37 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
namespace Presas_Depredadores
{
	/// <summary>
	/// Description of Node.
	/// </summary>
	public class Node
	{
		public Circle circle{get;set;}
		public List<Edge> Edges{get;set;}
		public Dictionary<int,int> marks{get;set;}
		public int Father{get;set;}
		public Node(){
			
		}
		public Node(Circle c)
		{
			circle = c;
			Edges = new List<Edge>();
			marks = new Dictionary<int,int>();
			Father = -1;
		}
		public Node(Node n){
			circle = n.circle;
			Edges = n.Edges;
			marks = n.marks;
			Father = n.Father;
		}
		
		public void LeaveAMark(int idAgent, int mark){
			if(!marks.ContainsKey(idAgent)){
				marks.Add(idAgent,mark);
				return;
			}
			marks[idAgent] = mark;
		}
		public int GetMarks(int id){
			if(!marks.ContainsKey(id))
				return -2;
			return marks[id];
		}
		
		public void ClearMarks(){
			marks.Clear();
		}
		
		public void ClearMarks(int id){
			marks.Remove(id);
		}
	}
}
