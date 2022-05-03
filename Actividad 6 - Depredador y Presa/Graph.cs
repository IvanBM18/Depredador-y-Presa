//Date: 30/09/2021
using System;
using System.Drawing;
using System.Collections.Generic;


namespace Actividad_6___Depredador_y_Presa{
	
	public class Graph{
		List<Vertex> vL;
		
		public Graph(){
			vL = new List<Vertex>();
		}
		
		public void addVertex(Circle circle){
			Vertex v = new Vertex(circle); //Le colocamos algo nuevo
			vL.Add(v);
		}
		
		public int vertexCount(){
			return vL.Count;
		}
		
		//Cargamos el operador[]
		public Vertex this[int i]{
			get {return vL[i]; }
		}
		
		//Buscamos el inidice de un vertice
		public int findIndex(Vertex v){
			for (int i = 0; i < vL.Count; i++) {
				if(vL[i] == v){
					return i;
				}
			}
			return -1;
			
		}
		
		//Retornamos todos las Aristas
		public List<Edge> getAllEdges(){
			Vertex aux;
			List<Edge> edgesList = new List<Edge>();
			for(int i = 0; i < vertexCount();i++){
				aux = vL[i];
				for(int j = 0; j < aux.getEdgesCount(); j++){
					//Sino existe una arista repetida(con VO y VD iguales o intercalados con otra arista)
					if(!edgesList.Exists( e => (e.VO == aux.getEdge(j).VD && e.VD == aux.getEdge(j).VO))) 
						edgesList.Add(aux.getEdge(j));
				}
			}
			return edgesList;
		}
		
		//Algoritmo DFS
		public List<Vertex> dfs(Vertex first){
			List<Vertex> visited = new List<Vertex>();
			Stack<Vertex> stack = new Stack<Vertex>();
			List<Vertex> path = new List<Vertex>();
			dfs(first,visited,stack,path);
			return path;
		}
		
		private List<Vertex> dfs(Vertex actual, List<Vertex> visited,Stack<Vertex> stack,List<Vertex> path){
			visited.Add(actual);
			path.Add(actual);
			bool exists;
			
			foreach (Edge eI in actual.EL) {
				exists = false;
				exists = visited.Exists(vJ => vJ.getData().ID == eI.VD.getData().ID);
				if(!exists){ //Recursividad
					stack.Push(actual);
					dfs(eI.VD,visited,stack,path);
					if(visited.Count == vL.Count)
						return path;
					path.Add(stack.Pop());
				}
				
			}
			return path; 
		}
		
		//Limpiar grafo
		public void clear(){
			vL.Clear();
		}
		
	}
	
	//Vertice
	public class Vertex{
		List<Edge> eL;
		Circle data;
		
		public Vertex(Circle circle){
			eL = new List<Edge>();
			data = circle;
		}
		
		public int getEdgesCount(){
			return eL.Count;
		}
		
		public Circle getData(){
			return data;
		}
		
		public void addEdge(Vertex vD,Vertex vO,Point[] path,double value){
			Edge e = new Edge(vD,vO,path,value); 
			eL.Add(e);
		}
		
		public override string ToString()
		{
			return data.ID.ToString();
		}
		
		public Edge getEdge(int index){
			return eL[index];
		}
		
		public List<Edge> EL {
			get { return eL; }
		}
		
	}
	
	//Arista
	public class Edge{
		Point[] path;
		Vertex vO,vD;
		double value;
			
		public Edge(Vertex vD,Vertex vO,Point[] path,double value){
			this.vD = vD;
			this.vO = vO;
			this.path = path; 
			this.value = value;
		}
		
		public int getPathCount(){
			return path.Length-1; 
		}
		
		public Point getPoint(int i){
			return path[i];
		}
		
		public Vertex VD {
			get { return vD; }
		}
		
		public Vertex VO {
			get { return vO; }
		}
		
		public double Value {
			get { return value; }
		}
	}
	
}

