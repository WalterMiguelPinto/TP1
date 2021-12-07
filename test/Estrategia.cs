
using System;
using System.Collections;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{

		private List<Movimiento> conquistas = new List<Movimiento>();
		private int contadorConquistas = 0;
		
		
		public String Consulta1( ArbolGeneral<Planeta> arbol)
		{
			int nivelMaximo = 0;
			Cola<ArbolGeneral<Planeta>> q = new Cola<ArbolGeneral<Planeta>>();
			q.encolar(arbol);
            while (!q.esVacia())
            {
				int nivel = q.cantElementos();
				nivelMaximo++;
                while (nivel --> 0)
                {
					ArbolGeneral < Planeta > nodo = q.desencolar();
					foreach(ArbolGeneral<Planeta> hijo in nodo.getHijos())
                    {
						q.encolar(hijo);
                    }
                }
            }

			String maximo = nivelMaximo.ToString();

			return maximo;


		}


		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			Planeta planeta = arbol.getDatoRaiz();
            if (arbol.esHoja()&& planeta.Poblacion() > 3 )
            {
				return "1";
            }
			int sum = 0;
            foreach (ArbolGeneral<Planeta> nodo in arbol.getHijos())
            {
				sum += Int32.Parse(Consulta2(nodo));
            }
			string suma = Convert.ToString(sum);
			return suma;
		
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			int promedio = promedioArbol(arbol,false);
			int nivelActual = 0;
			int contadorPlanetasMayor = 0;
			string mensaje = "promedio de planetas por nivel \n";
			Cola<ArbolGeneral<Planeta>> q = new Cola<ArbolGeneral<Planeta>>();
			q.encolar(arbol);
			while (!q.esVacia())
			{
				int nivel = q.cantElementos();
				nivelActual++;
				mensaje += "Nivel Actual: "+ Convert.ToString(nivelActual) + "\n";
				contadorPlanetasMayor = 0;
				while (nivel-- > 0)
				{
					ArbolGeneral<Planeta> nodo = q.desencolar();
					foreach (ArbolGeneral<Planeta> hijo in nodo.getHijos())
					{
						q.encolar(hijo);
						Planeta planeta = hijo.getDatoRaiz();
                        if (planeta.Poblacion()>promedio)
                        {
							contadorPlanetasMayor++;
                        }

					}
					
				}
				mensaje += "tiene " + Convert.ToString(contadorPlanetasMayor) + "con poblacion mayor al promedio " + "\n";
			}
			return mensaje;
		}

		private int promedioArbol(ArbolGeneral<Planeta> arbol, bool flag)
        {			
			Planeta planeta = arbol.getDatoRaiz();
            if (flag)
            {
				return planeta.Poblacion();
            }
			

			int sum = planeta.Poblacion();
			int cantidadPlanetas = 1;
			foreach (ArbolGeneral<Planeta> nodo in arbol.getHijos())
			{
				sum += promedioArbol(nodo,true);
				cantidadPlanetas++;
			}
			int promedio = sum / cantidadPlanetas;
			return promedio;
		}
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{

			Planeta origen = null;
			Planeta destino = null;
			

			Cola<ArbolGeneral<Planeta>> q = new Cola<ArbolGeneral<Planeta>>();
			q.encolar(arbol);
			while (!q.esVacia())
			{
				int nivel = q.cantElementos();
				while (nivel-- > 0)
				{
					ArbolGeneral<Planeta> nodo = q.desencolar();
					foreach (ArbolGeneral<Planeta> hijo in nodo.getHijos())
					{
						q.encolar(hijo);
						Planeta planeta = hijo.getDatoRaiz();
						if (planeta.EsPlanetaDeLaIA())
						{
							origen = planeta;

							foreach (ArbolGeneral<Planeta> hijo2 in hijo.getHijos())
							{
								Planeta planetahijo = hijo2.getDatoRaiz();
								if (planetahijo.EsPlanetaNeutral() || planetahijo.EsPlanetaDelJugador())
								{
									destino = planetahijo;

									Movimiento nuevaConquista = new Movimiento(origen, destino);
									Movimiento reagrupar = new Movimiento(destino, origen);
									conquistas.Add(reagrupar);
									return nuevaConquista;

								}                                
							}
						}
					}
				}
			}

			
			
			if(contadorConquistas >= conquistas.Count)
            {
				contadorConquistas = 0;
            }

			Movimiento reagrupando = conquistas[contadorConquistas];

			contadorConquistas++;

			return reagrupando;
		}

		

		
	}
}


