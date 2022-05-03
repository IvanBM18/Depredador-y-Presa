//Date: 20/11/2021
using System;

//Para construir camino
//Seleccionar vD(su ID)
//Guardarlo en un vector
//Si no es origen ir a su proveniente, repetir paso 2
//Fin
namespace Actividad_6___Depredador_y_Presa{
	public class DijkstraElement{
		//Atributos
		public bool isDefinitive {get;set;}
		public double accumulatedDistance {get;set;}
		public Vertex comingFrom {get;set;}
		
		public DijkstraElement(Vertex v){
			isDefinitive = false;
			comingFrom = v;
			accumulatedDistance =-1;
		}
	
		public DijkstraElement(Vertex v,int weight){ //Deberian de ser en un inicio infinitamente distantes
			isDefinitive = false;
			comingFrom = v;
			accumulatedDistance = weight;
		}
	}
}
