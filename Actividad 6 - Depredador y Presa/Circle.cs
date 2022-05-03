//Date: 06/11/2021
using System;
using System.Drawing;

namespace Actividad_6___Depredador_y_Presa
{
	public class Circle{
		//Atributos
		Point p; //Coordena del centro
		
		double radius; //Radio
		int iD;
		double area;
		
		public Circle(){
			iD = -1;
		}
		
		public Circle(int x,int y, double r){
			radius = r;
			p.X = x;
			p.Y = y;
			iD = -1;
			area = Math.PI*(radius*radius);
		}
		
		//Getters
		public int ID {
			get { return iD; }
		}
		
		public double Area{
			get {return area;}
		}
		
		public double Radius {
			get { return radius; }
		}

		public int Y {
			get { return p.Y; }
		}
		
		public int X {
			get { return p.X; }
		}
		
		public Point P {
			get { return p; }
		}
		
		//Setter
		public void setx(int centrox){
			p.X = centrox;
		}
		
		public void sety(int centroy){
			p.Y = centroy;
		}
		
		public void setRadius(double radius){
			this.radius = radius;
			area = Math.PI*(radius*radius);
		}
		
		public void setId(int iD){
			this.iD = iD;
		}
		
	}
}
