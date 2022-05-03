//Date: 16/10/2021
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Actividad_6___Depredador_y_Presa{
	
	public class Prey{
		
		
		protected Vertex vActual;
		protected double size;
		protected int speed;
		
		
		protected int edgeIndex; //Indice de arista dentro del vertice actual
		protected int pathIndex; //Indice dentro del arreglo de Path
		protected int pathingIndex; //Indice dentro del camino que desea recorrer
		
		public List<Vertex> pathing {get;set;} //Camino a recorrer
		public int iD{get;set;}
		public bool lastMove{get;set;} //Bandera de que termine el movimiento actual
		public bool canMove{get; set;} //Bandera de que puede moverse dentro de su arista
		
		public Prey(){
			pathing = null;
			pathingIndex = 0;
			edgeIndex = 0; //Dentro del grafo
			pathIndex = 0;
			speed = 6;
			size = 0;
			canMove = true;
			lastMove = false;
		}
		
		public Prey(Vertex v){
			vActual = v;
			pathing = null;
			pathingIndex = 0;
			edgeIndex = 0; //Dentro del grafo
			pathIndex = 0;
			speed = 5;
			size = 0;
			canMove = true;
			lastMove = false;
			iD = 0;
		}
		
		//Getters
		public Vertex VActual {
			get { return vActual; }
		}
		
		public double Size {
			get { return size; }
		}
		
		public int Speed {
			get { return speed; }
		}
		
		public int PathIndex {
			get { return pathIndex; }
		}
		
		public int EdgeIndex {
			get { return edgeIndex; }
		}
		
		
		public Point getPoint(){
			return vActual.getEdge(edgeIndex).getPoint(pathIndex);
		}
		
		//Setters
		public void setSpeed(int v){
			speed = v;
		}
		
		public void setVActual(Vertex v){
			vActual = v;
		}
		
		public void setPathIndex(int indx){
			pathIndex = indx;
		}
		
		public void setEdgeIndex(int indx){
			edgeIndex = indx;
		}
		
		public virtual void setSize(double s){
			size = s;
		}
		
		
		//Metodos
		public virtual bool selectNewPath(){
			if(lastMove){
				canMove = false;
				pathIndex = 0;
				edgeIndex = 0;
				return false;
			}
			pathingIndex++;
			if(pathingIndex >= pathing.Count){
				pathIndex = 0;
				edgeIndex = 0;
				canMove = false;
				return false;
			}else {
				for(int i = 0; i < vActual.getEdgesCount();i++){
					if(vActual.getEdge(i).VD == pathing[pathingIndex]){
						edgeIndex = i;
						pathIndex = 0;
						canMove = true;
						return true;
					}
				}
				pathIndex = 0;
				edgeIndex = 0;
				canMove = false;
				return false;
			}
			
		}
		
		public virtual bool walk(){
			if(pathIndex == -1)
				return false;
			if (pathIndex + speed <= vActual.getEdge(edgeIndex).getPathCount()){ //Si el lugar es menor a la cantidad de pixeles de la linea
				pathIndex+= speed;
				return true;
			}
			vActual = vActual.getEdge(edgeIndex).VD;
			return false;
			
		}
		
	}

	public class Predator : Prey{
		//Atributos de la Presa
		double radar;
		
		public int pathCount{get;set;}
		public Prey hunting{get;set;}//Presa que sigue
		public bool onPrey{get;set;} //Bandera para indicar que esta en casa
		public List<DijkstraElement> vectorDijkstra{get;set;} //Vector Dijkstra para un vertice inicial
		
		//Constructores del Depredador
		public Predator(){}
		public Predator(Vertex v){
			this.vActual = v;
			this.pathing = null;
			this.pathingIndex = 1;
			this.edgeIndex = 0; //Dentro del grafo
			this.pathIndex = 0;
			this.speed = 5;
			this.size = 0;
			this.canMove = true;
			this.lastMove = false;
			radar = size*2;
			pathCount = 0;
			vectorDijkstra = new List<DijkstraElement>();
			hunting = new Prey();
		}
		//Getters y Setters
		public double Radar {
			get { return radar; }
		}
		
		public override void setSize(double s){
			size = s;
			radar = s*2;
		}
		
		public void setRadar(double r){
			radar = r;
		}
		
		//Metodos
		public override bool walk(){
			if(pathIndex == -1)
				return false;
			if (pathIndex + speed <= vActual.getEdge(edgeIndex).getPathCount()){ //Si el lugar es menor a la cantidad de pixeles de la linea
				pathIndex+= speed;
				return true;
			}
			vActual = vActual.getEdge(edgeIndex).VD;
			return false;
		}
		
		public override bool selectNewPath(){
			if(lastMove){
				canMove = false;
				pathIndex = 0;
				edgeIndex = 0;
				return false;
			}
			if(onPrey){
				if(VActual == pathing[pathCount] && pathCount+1 != pathing.Count)
					pathCount++;
				if(pathCount >= pathing.Count){
					pathIndex = 0;
					edgeIndex = 0;
					canMove = false;
					return false;
				}else {
					for(int i = 0; i < VActual.getEdgesCount();i++){
						if(vActual.getEdge(i).VD == pathing[pathCount]){
							edgeIndex = i;
							pathIndex = 0;
							canMove = true;
							return true;
						}
					}
					pathIndex = 0;
					edgeIndex = 0;
					canMove = false;
					return false;
				}
			}else {
				Random rand = new Random();
				edgeIndex = rand.Next(vActual.EL.Count);
				pathIndex = 0;
				return true;
			}
			
		}
	}
}
