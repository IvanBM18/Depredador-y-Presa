//Date: 30/11/2021
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


//Amarillo: Objetivo
//Presa: Verde
//Depredador: Tomato

namespace Actividad_6___Depredador_y_Presa
{
	public partial class MainForm : Form{
		//Banderas
		bool imgSelected;
		bool graphExists;
		
		//Bitmap
		Bitmap bmpAnimation; //Bitmap principal
		Bitmap bmpGraph; //Bitmap del grafo
		
		//Variables
		int id;
		int preyId;
		Vertex objective;
		
		//TDAs
		List<Prey> preyL;
		List<Predator> predatorL;
		List<Circle> cL;
		List< List<DijkstraElement> > vDijkstraList; //Un vector para cada vertice inicial
		Graph graph;
		
		Color radarColor = Color.FromArgb(80,100,250,190); //Color RadarNormal
		Color detectedColor = Color.FromArgb(80,190,250,100); //Color Radar Detecto una Presa
		
		public MainForm(){
			InitializeComponent();
			id = 0;
			preyId = 0;
			cL = new List<Circle>();
			preyL = new List<Prey>();
			predatorL = new List<Predator>();
			vDijkstraList = new List<List<DijkstraElement>>();
			graph = new Graph();
			imgSelected = false;
			graphExists = false;
			objective = null;
		}
		
//Herramientras Graficas
		//Seleccion de Imagen
		void ButtonImgClick(object sender, EventArgs e){
			openFileDialog1.ShowDialog();
			if(openFileDialog1.FileName != "openFileDialog1"){
				bmpGraph = new Bitmap(openFileDialog1.FileName);
				bmpAnimation = new Bitmap(bmpGraph.Width,bmpGraph.Height);
				
				pictureBoxImg.BackgroundImage = bmpGraph;
				pictureBoxImg.BackgroundImageLayout = ImageLayout.Zoom;
				pictureBoxImg.Image =bmpAnimation;
				imgSelected = true;
			}
		}
		
		//Generacion del Grafo
		void ButtonGraphClick(object sender, EventArgs e){
			if(!imgSelected)
				return;
			id= 0;
			Color cI;
			Circle newCircle;
			
			reset();
			for (int yK = 0; yK < bmpGraph.Height; yK++) {
				for (int xK = 0; xK < bmpGraph.Width; xK++) {
					cI = bmpGraph.GetPixel(xK,yK);
					if(isBlack(cI)){
						if(findCircle(xK,yK) == null){
							newCircle = createCircle(xK,yK);
							cL.Add(newCircle);
						}
					}
				}
			}
			
			generateGraph();
			drawGraph();
			pictureBoxImg.Refresh();
			
			//Dibujamos Circulos Detectados
			Circle i;
			for (int j = 0; j < graph.vertexCount(); j++) {
				i = graph[j].getData();
				drawCircle(i.P,i.Radius,bmpGraph,Color.SkyBlue,j.ToString());
			}
			trackBarRadar.Minimum = (int)cL[0].Radius + 4;
			trackBarRadar.Maximum = (int)cL[0].Radius * 3;
			pictureBoxImg.Refresh();
			graphExists = true;
		
		}
		
		//Click en la Imagen
		void PictureBoxImgMouseClick(object sender, MouseEventArgs e){
			if (graphExists) {
				Point bmpPoint = getBitmapPoint(new Point(e.X,e.Y));
				if(radioButtonPrey.Checked && (preyL.Count + predatorL.Count) < cL.Count-1){
					Prey agent;
					Vertex vA = belongsToVertex(bmpPoint);
					if(vA != null){
						foreach (Prey aI in preyL) {
							if (aI.VActual == vA)
								return;
						}
						foreach (Predator aI in predatorL) {
							if (aI.VActual == vA)
								return;
						}
						if(vA == objective)
							return;
						agent = new Prey(vA);
						agent.setSize(vA.getData().Radius);
						agent.iD = preyId++;
						preyL.Add(agent);
						drawCircle(vA.getData().P,agent.Size,bmpAnimation,Color.LimeGreen,"");
					}
				}else if (radioButtonObjective.Checked){
					if(objective == null){
						objective = belongsToVertex(bmpPoint);
						if(objective != null){
							foreach (Prey aI in preyL) {
								if (aI.VActual == objective){
									objective = null;
									return;
								}
							}
							foreach (Predator aI in predatorL) {
								if (aI.VActual == objective){
									objective = null;
									return;
								}
							}
							drawCircle(objective.getData().P,objective.getData().Radius,bmpGraph,Color.Yellow,"");
							generateDijkstra();
						}
					}
					
				}else if(radioButtonPredator.Checked && (preyL.Count + predatorL.Count) < cL.Count-1){
					Predator agent;
					Vertex vA = belongsToVertex(bmpPoint);
					if(vA != null){
						foreach (Prey aI in preyL) {
							if (aI.VActual == vA)
								return;
						}
						foreach (Predator aI in predatorL) {
							if (aI.VActual == vA)
								return;
						}
						if(vA == objective)
							return;
						
						agent = new Predator(vA);
						agent.setSize(vA.getData().Radius);
						predatorL.Add(agent);
						drawRadar(vA.getData().P,agent.Size,agent.Radar,bmpAnimation,radarColor);
					}
				}
				pictureBoxImg.Refresh();
			}
		}
		
		//Tamaño del Radar
		void TrackBarRadarScroll(object sender, EventArgs e){
			if(graphExists){
				Graphics g = Graphics.FromImage(bmpAnimation);
				g.Clear(Color.Transparent);
				foreach (Predator predatorI in predatorL) {
					predatorI.setRadar((double)trackBarRadar.Value);
					drawRadar(predatorI.VActual.getData().P,predatorI.Size,predatorI.Radar,bmpAnimation,radarColor);
				}
				foreach (Prey pI in preyL) {
					drawCircle(pI.VActual.getData().P,pI.Size,bmpAnimation,Color.LimeGreen,"");
				}
				pictureBoxImg.Refresh();
			}
		}
		
		//Animación
		void ButtonAnimationClick(object sender, EventArgs e){
			if(preyL.Count != 0 && objective != null){
				if(graph.vertexCount() <= 3)
					return;
				
				//Variables
				Vertex v;
				Prey inRadar;
				Point p;
				Edge actualEdge;
				Graphics g = Graphics.FromImage(bmpAnimation);
				List<Vertex> auxPath;
				bool lastPath = false;
				
				//Generacion DFS para cada presa
				foreach (Prey preyI in preyL) {
					preyI.pathing = graph.dfs(preyI.VActual);
					preyI.pathing.RemoveAt(0);
				}
				
				
				while (makeAnimation()) {
					g.Clear(Color.Transparent);
					
					//Movimientos de las Presas
					foreach (Prey pI in preyL) {
						if(pI.VActual == objective){
							lastPath = true;
							pI.lastMove = true;
							pI.canMove = false;
						}
						if(pI.canMove){
							if(!pI.walk()){
								pI.selectNewPath();
							}
						}
						v = pI.VActual;
						actualEdge = v.getEdge(pI.EdgeIndex);
						p = actualEdge.getPoint(pI.PathIndex);
						drawCircle(p,pI.Size,bmpAnimation,Color.LimeGreen,"");
						if(lastPath){
							pI.lastMove= true;
						}
					}
					
					if(preyL.Count == 0)
						lastPath = true;
					
					//Movimiento de los depredadores
					foreach (Predator pI in predatorL) {
						if(lastPath){
							pI.lastMove = true;
						}
						if(pI.canMove){
							if(!pI.walk()){
								pI.selectNewPath();
							}
						}
						v = pI.VActual;
						actualEdge = v.getEdge(pI.EdgeIndex);
						p = actualEdge.getPoint(pI.PathIndex);
						if(!pI.onPrey){ //No esta En Caseria(eliminar esto?)
							inRadar = isAnybodyInRadar(pI);
							if(inRadar == null)
								drawRadar(p, v.getData().Radius, pI.Radar,bmpAnimation, radarColor);
							else{ //Entra una presa en su radar
								drawRadar(p, v.getData().Radius, pI.Radar,bmpAnimation, detectedColor);
								auxPath = generatePath(pI.VActual.getEdge(pI.EdgeIndex).VD,inRadar.VActual.getEdge(inRadar.EdgeIndex).VD);
								if(auxPath != null){
									pI.onPrey = true;
									pI.hunting = inRadar;
									pI.pathing = auxPath;
									for (int i = 0; i < pI.VActual.getEdgesCount(); i++) {
										if(pI.VActual.getEdge(i).VD == pI.pathing[0])
											pI.setEdgeIndex(i);
									}
									pI.setSpeed((int)Math.Round((double)pI.Speed * 1.2));
								}
								
							}
						}else { //En caseria
							inRadar = preyL[preyL.IndexOf(pI.hunting)];
							pI.hunting = inRadar; //Actualizamos la presa que esta en su radar
							drawRadar(p, v.getData().Radius, pI.Radar,bmpAnimation, detectedColor);
							pI.pathing = generatePath(pI.VActual.getEdge(pI.EdgeIndex).VD,inRadar.VActual.getEdge(inRadar.EdgeIndex).VD);
							pI.pathCount = 0;
							//Generamos el inicio del Path
							for (int i = 0; i < pI.VActual.getEdgesCount(); i++) {
								if(pI.VActual.getEdge(i).VD == pI.pathing[0])
									pI.setEdgeIndex(i);
							}
							if( (pI.getPoint().X == pI.hunting.getPoint().X) && (pI.getPoint().Y == pI.hunting.getPoint().Y)){
								preyL.Remove(pI.hunting);
								pI.canMove = true;
								pI.onPrey = false;
								drawRadar(p, v.getData().Radius, pI.Radar,bmpAnimation, radarColor);
								pI.setSpeed((pI.Speed * 5)/6);
								foreach (Predator pJ in predatorL) {
									if(pJ != pI){
										if(pJ.hunting.iD == pI.hunting.iD)
											pJ.onPrey = false;
											pJ.hunting = null;
											pJ.canMove = true;;
											pI.setSpeed((pI.Speed * 5)/6);
									}
								}
								pI.hunting = null;
							}
						}
						
					}
					pictureBoxImg.Refresh();
				}
			}
		
		}
		
		//Reinicio de Imagen
		void ButtonResetClick(object sender, EventArgs e){
			preyId = 0;
			preyL.Clear();
			predatorL.Clear();
			objective = null;
			vDijkstraList.Clear();
			trackBarRadar.Value = trackBarRadar.Minimum;
			
			Graphics g = Graphics.FromImage(bmpAnimation);
			g.Clear(Color.Transparent);
			g = Graphics.FromImage(bmpGraph);
			g.Clear(Color.Transparent);
			drawGraph();
			Circle i;
			for (int j = 0; j < graph.vertexCount(); j++) {
				i = graph[j].getData();
				drawCircle(i.P,i.Radius,bmpGraph,Color.SkyBlue,j.ToString());
			}
			pictureBoxImg.Refresh();
		}
//Validaciones
		bool isCircle(Circle c,int x, int y){
			int x2,y2;
			double r;
			x2 = c.X;
			y2 = c.Y;
			r = c.Radius;  
			if( ( ((x-x2) * (x-x2)) + ((y-y2) * (y-y2)) - (r*r) ) <= 0 ){
				return true;
			}
			return false;
		}
		
		bool isCircle(Point p,double radius,int x, int y){
			int x2,y2;
			double r;
			x2 = p.X;
			y2 = p.Y;
			r = radius;  
			if( ( ((x-x2) * (x-x2)) + ((y-y2) * (y-y2)) - (r*r) ) <= 0 ){
				return true;
			}
			return false;
		}
		
		bool isBlack(Color c){
			if (c.R == 0)
				if (c.G == 0) 
					if(c.B == 0)
						return true;
			return false;
		}
		
		bool isWhite(Color c){
			if(c.R == 255)
				if(c.G == 255)
					if(c.B == 255)
						return true;
			return false;
		}
		
		bool isSolution(List<DijkstraElement> VD){
			foreach (DijkstraElement dI in VD) {
				if(!dI.isDefinitive)
					return false;
			}
			return true;
		}
		
		bool makeAnimation(){
			foreach (Prey pI in preyL) {
				if(pI.canMove)
					return true;
			}
			foreach(Predator pI in predatorL){
				if(pI.canMove)
					return true;
			}
			return false; 
		}
		
		Prey isAnybodyInRadar(Predator detect){
			foreach (Prey pI in preyL) {
				Point p = pI.getPoint();
				if(isCircle(detect.getPoint(),detect.Radar,p.X ,p.Y))
					return pI;
				
			}
			return null;
		}
		
//Busquedas
		Circle findCircle(int x, int y){
			foreach (Circle i in cL) {
				if(isCircle(i,x,y))
				   return i;
			}
			return null;
		}
		
		Vertex belongsToVertex(Point p){
			float rI;
			int xI,yI;
			for(int i = 0;i < graph.vertexCount(); i++){
				xI = graph[i].getData().X;
				yI = graph[i].getData().Y;
				rI= (float)graph[i].getData().Radius;
				
				float solution = ((xI- p.X)*(xI - p.X) + (yI- p.Y)*(yI - p.Y) - rI*rI);
				if(solution < 0){
					return graph[i];
				}
				
			}
			return null;
		}
		
//Dibujo del BitMap
		void drawCircle(Point p, double r, Bitmap bmpLocal,Color c,string id){
			Graphics graphic = Graphics.FromImage(bmpLocal);
			Brush cBrush = new SolidBrush(c);
			Brush colorT = new SolidBrush(Color.PaleVioletRed);
			
			graphic.FillEllipse(cBrush,(float)(p.X-r),(float)(p.Y-r),(float)r*2,(float)r*2);
			graphic.DrawString(id.ToString(),new Font("Arial",30),colorT,p.X,p.Y);
		}
		
		void drawEdge(Point pI, Point pF,Color color,Bitmap bmpLocal){
			Graphics g = Graphics.FromImage(bmpLocal);
			Pen pen = new Pen(color,18);
			g.DrawLine(pen,pI.X,pI.Y,pF.X,pF.Y);
		}
		
		void drawRadar(Point p, double rC,double rSize ,Bitmap bmpLocal,Color radarC){
			Graphics graphic = Graphics.FromImage(bmpLocal);
			Brush radarBrush = new SolidBrush(radarC);
			Brush circleBrush = new SolidBrush(Color.Tomato);
			
			graphic.FillEllipse(radarBrush,(float)(p.X-rSize),(float)(p.Y-rSize),(float)rSize*2,(float)rSize*2);
			graphic.FillEllipse(circleBrush,(float)(p.X-rC),(float)(p.Y-rC),(float)rC*2,(float)rC*2);
		}
		
		void drawGraph(){
			for (int i = 0; i < graph.vertexCount(); i++) {
				Vertex v = graph[i];
				for (int j = 0; j < v.getEdgesCount(); j++) {
					Edge e = v.getEdge(j);
					drawEdge(v.getData().P,e.VD.getData().P, Color.Silver,bmpGraph);
				}
			}
			
		}
		
//Metodos Generales
		//Convierte una coordenada de PictureBox a BitMap
		Point getBitmapPoint(Point imgPoint){
			double wp,hp; //Valores alto y ancho del pictureBox
			double wb,hb; //Valores de alto y ancho del Bitmap
			double pw,ph; //Factor de escala para alto y ancho
			double dw,dh; //Distancia dw,dh
			double pfactor; //Factor de escala
			double xp,yp;
			double xb,yb;
			
			Point bP;
			
			dw = 0;
			dh = 0;
			wp = pictureBoxImg.Width;
			hp = pictureBoxImg.Height;
			wb = bmpGraph.Width;
			hb = bmpGraph.Height;
			xp = imgPoint.X;
			yp = imgPoint.Y;
			
			pw = wp/wb;
			ph = hp/hb;
			
			pw = wp/wb;
			ph = hp/hb;
			
			if(pw < ph){
				pfactor = pw;
				dh = Math.Abs(hb*pfactor-hp)/2;
			}else{
				pfactor = ph;
				dw = Math.Abs(wb*pfactor-wp)/2;
			}
			xb = (xp-dw)/pfactor;
			yb = (yp-dh)/pfactor;
			bP = new Point((int)Math.Round(xb),(int)Math.Round(yb));
			return bP;
		}
		
		//Genera un camino
		List<Vertex> generatePath(Vertex v0,Vertex vK){
			List<DijkstraElement> vDijkstra;
			List<Vertex> path = new List<Vertex>();
			path.Add(vK);
			
			vDijkstra = vDijkstraList[v0.getData().ID];
			vK = vDijkstra[vK.getData().ID].comingFrom;
			path.Insert(0,vK);
			while (vK != v0) {
				vK = vDijkstra[vK.getData().ID].comingFrom;
				path.Insert(0,vK);
				if(!vDijkstra[vK.getData().ID].isDefinitive)
					return null;
			}
			return path;
		}
		
		//Genera un VEctor Dijkstra
		List<DijkstraElement> generateDijkstra(Vertex v){
			List<DijkstraElement> vDijkstra = new List<DijkstraElement>();//Vector dijkstra
			Vertex vActual = v;//Vactual que sera definitivo
			
			//Variables aux
			double distance;
			int index;
			double min;
			Vertex vAux;
			
			//Generacion vector dijkstra
			for(int i = 0;i < graph.vertexCount() ;i++){
				if(graph[i] == vActual){
					DijkstraElement dI = new DijkstraElement(graph[i],0);
					dI.isDefinitive = true;
					vDijkstra.Add(dI);
				}else{
					DijkstraElement dI = new DijkstraElement(graph[i]);
					vDijkstra.Add(dI);
				}
			}

			
			//Mientras no sea solucion
			distance = 0;
			while(!isSolution(vDijkstra)) {
				//Hacemos definitivo al menor 
				vDijkstra[vActual.getData().ID].isDefinitive = true;
				
				//Actualizamos vector Dijkstra
				for (int i = 0; i < vActual.getEdgesCount(); i++) {
					index =vActual.getEdge(i).VD.getData().ID; //Indice en vectorDijkstra y Grafo 
					if(vDijkstra[index].accumulatedDistance == -1){//Caso es infinito
						vDijkstra[index].accumulatedDistance = vActual.getEdge(i).Value + distance; 
						vDijkstra[index].comingFrom = vActual;
					}else if( (distance + vActual.getEdge(i).Value) < vDijkstra[index].accumulatedDistance){//Caso nuevo camino menor
						vDijkstra[index].accumulatedDistance = vActual.getEdge(i).Value + distance;
						vDijkstra[index].comingFrom = vActual;
					}
				}
				//Seleccion del menor
				min = -1;
				vAux = null;
				for (int i = 0; i < vDijkstra.Count; i++) {
					DijkstraElement di = vDijkstra[i];
					if(!di.isDefinitive && (di.accumulatedDistance < min || min == -1) && di.accumulatedDistance != -1){
						vAux = graph[i];
						min  = di.accumulatedDistance;
					}
				}
				
				//Si se llego a disconexo
				if(vAux == null){
					for (int i = 0; i < vDijkstra.Count; i++) {
						DijkstraElement di = vDijkstra[i];
						if(!di.isDefinitive){
							di.accumulatedDistance = 0;
							vActual = di.comingFrom;
							break;
						}
					}
					return vDijkstra;
				}else{
					vActual = vAux;
				}
				
				//Calculo peso Acumulado
				distance = vDijkstra[vActual.getData().ID].accumulatedDistance;
			}
			return vDijkstra;
		}
		
		//Genera el grafo tomando en cuenta los obstaculos
		void generateGraph(){
			Vertex vI,vJ;
			double distance;
			Point[] aux;
			foreach (Circle i in cL) {
				graph.addVertex(i);
			}
			for (int i = 0; i < graph.vertexCount(); i++) {
				vI = graph[i];
				for(int j = 0; j < graph.vertexCount();j++){
					if( j != i){
						vJ = graph[j];
						aux = checkPath(vI.getData().P,vJ.getData().P);
						if(aux != null && aux.Length > 2){
							distance = Math.Sqrt( Math.Pow((vJ.getData().X-vI.getData().X),2) + Math.Pow((vJ.getData().Y-vI.getData().Y),2));
							vI.addEdge(vJ,vI,aux,distance);
						}
					}
				}
				
			}
		}
		
		//Genera una Lista con los vectores dijkstra de cada vertice
		void generateDijkstra(){
			for (int i = 0; i < graph.vertexCount(); i++) {
				vDijkstraList.Add(generateDijkstra(graph[i]));
			}
		}
		
		//Reinicia todos los valores
		void reset(){
			cL.Clear();
			graph.clear();
			preyL.Clear();
			predatorL.Clear();
			vDijkstraList.Clear();
			objective  = null;
			graphExists = false;
			imgSelected = false;
			id = 0;
			preyId = 0;
		}
		
//Analisis de BitMap
		//Crea un Circulo dado un punto de contacto
		Circle createCircle(int xK,int yK){
			int xR,xL,xC;
			int yU,yL,yC;
			double r;
			Color cI;
			
			yL = yK;
			do{
				cI = bmpGraph.GetPixel(xK,++yL);
			}while (isBlack(cI));
			yL--;
			
			yU = yK;
			do{
				cI = bmpGraph.GetPixel(xK,--yU);
			}while (isBlack(cI));
			yU++;
			yC = (int)Math.Round(((double)yU+(double)yL) / (double)2) ;
			
			xR = xK;
			do{
				cI = bmpGraph.GetPixel(++xR,yC);
			}while (isBlack(cI));
			xR--; 
			
			xL = xK;
			do{
				cI = bmpGraph.GetPixel(--xL,yC);
			}while (isBlack(cI));
			xR++;
			
			xC = (int)Math.Round(((double)xL+(double)xR) / (double)2) ;
			
			r = ( ((double)xR-(double)xL) / (double)2);
			Circle c = new Circle(xC,yC,r+2);
			c.setId(id++);
			return c;
		}
		
		//Crea una arista si no hay obstrucciones
		Point[] checkPath(Point p0, Point pF){
			//Variables para el camino
			Point[] path;
			float yk,xk;
			float x0 = p0.X;
			float xf = pF.X;
			float y0 = p0.Y;
			float yf = pF.Y;
			
			//Variables para la ecuacion
			float m = (yf-y0)/(xf-x0);
			if (double.IsInfinity(m)) 
				m = 0;
			float b = y0-m*x0;
			
			//variables auxiliares
			int n;
			int i = 0;
			int inc = 1;
			
			//Banderas para comprobacion
			Color cK;
			
			
			xk = x0;
			yk = y0;
			if(m < 1 && m > -1 ){//Caso Pendiente entre -1 y 1
				if(xf < x0)
					inc *= -1;
				if(x0 == xf){
					if(yf < y0)
						inc *= -1;
					n = Math.Abs((int)(yf-y0))+1;
					path = new Point[n];
					
					path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk)); 
					do{
						yk += inc;
						cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk)); 
						path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
					}while(!isWhite(cK));
					do{
						yk += inc;
						cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk));
						path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
					}while(isWhite(cK));
					do{
						yk += inc;
						cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk));
						path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
						if(yk == yf)
							return path;
					}while(!isWhite(cK));
					return  null;
				}else{ //Caso Normal
					n = Math.Abs((int)(xf-x0))+1;
					path = new Point[n];
					
					path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk)); 
					do{
						xk += inc;
						yk = m*xk+b;
						cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk)); 
						path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
					}while(!isWhite(cK));
					do{
						xk += inc;
						yk = m*xk+b;
						cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk));
						path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
					}while(isWhite(cK));
					do{
						xk += inc;
						yk = m*xk+b;
						cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk));
						path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
						if(xk == xf)
							return path;
					}while(!isWhite(cK));
					return  null;
				}
				
				
				
			}else {
				n = Math.Abs((int)(yf-y0))+1;
				path = new Point[n];
				if(yf < y0)
					inc *= -1;
				path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk)); 
				do{
					yk += inc;
					xk = (yk - b)/m;
					cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk));
					path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
				}while(!isWhite(cK));
				do{
					yk += inc;
					xk = (yk - b)/m;
					cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk));
					path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
				}while(isWhite(cK));
				do{
					yk += inc;
					xk = (yk - b)/m;
					cK = bmpGraph.GetPixel((int)Math.Round(xk), (int)Math.Round(yk));
					path[i++] = new Point((int)Math.Round(xk), (int)Math.Round(yk));
					if(yk == yf)
						return path;
				}while(!isWhite(cK));
				return  null;
			}
		}
	}
}
