/*******************
Autores:    Juan David Ruiz (juandavidrf@unicauca.edu.co)
            Sebastian Montenegro (exlogam@unicauca.edu.co)
*******************/

/********************* Librerias ********************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TrayectoriaLibre : MonoBehaviour
{
    /**************** Variables ******************/
    [SerializeField] private Control_Panel CPanel;              //Booleana del panel libre
    public Slider DeslizadorVel;                                //Deslizador de velocidad
    public Text ValorTexto;                                     //Valor velocidad
    private float velocidad;                                    //Velocidad

    // Variables para crear los puntos
    private List<GameObject> puntos = new List<GameObject>();   //Vector de objetos punto
    private int NumPuntos = 0;                                  //Numeero de objetos puntos
    int segmento;                                               //NUmero de segmento de la linea
    public GameObject PrefabPuntoInical;                        //Prefab del punto inicial
    public GameObject PrefabPunto;                              //Prefab de los puntos
    public GameObject ContenedorPuntos;                         //COntenedor de los objetos punto

    // Variable para crea la linea
    public LineRenderer Linea;                                  //Linea
    public bool CerrarTrayectoria = false;                      //Booleana para hacer trayectoria cerrada
    private int numberOfPoints = 50;                            //Numero de puntos por segmento de linea

    // Variables para Colicionar
    RaycastHit hit;
    Ray ray;

    // Variables para publicar mediante ROS
    public RosSharp.RosBridgeClient.TrajPublisher publicador;   //Cliente ROS
    [HideInInspector] public float[,] figura;                   //Matriz con coordenadas de la linea

    // Variables para exportar e importar
    public TMP_InputField Ubicacion;                            //Ubicacion del almacenamiento interno
    public TMP_InputField Nombre;                               //Nombre del archivo
    private DatosTrayectoria Traj;                              //Objeto con los datos de la trayectoria
    private Variables datos;                                    
    private bool Editable = true;


    /**************** Funciones ******************/
    void Start()                                    //Funcion inicial
    {
        Inicializar_linea();
        ValorTexto.text =  Math.Round((DeslizadorVel.value*0.1f),2)+ " cm/s";
    }

    void Update()                                   //Mantine la linea actaulizada
    {
        if (CPanel.P_Libre)
        {
            if (Editable)
            {
                DibujarLinea(puntos);
                AgregarPuntoFinal();              //Funicon en proceso
                AgregarPuntoIntermedio();
            }
        }
    }

    void Inicializar_linea()                        //Dibuja la linea inicial
    {
        NumPuntos = 2;

        puntos.Add(Instantiate(PrefabPuntoInical, ContenedorPuntos.transform, false));
        puntos[0].transform.name = "Punto_1";
        puntos[0].transform.position = new Vector3 (-0.1f,0.244f,0.342f);

        puntos.Add(Instantiate(PrefabPunto, ContenedorPuntos.transform, false));
        puntos[1].transform.name = "Punto_2";
        puntos[1].transform.position = new Vector3 (-0.342f,0.244f,0.1f);
    }

    void AgregarPuntoFinal()                        //Agrega un punto al final de la linea
    {
        if (Input.GetMouseButtonDown(1))
        {
            NumPuntos += 1;

            puntos.Add(Instantiate(PrefabPunto, ContenedorPuntos.transform, false));
            puntos[NumPuntos-1].transform.position = puntos[NumPuntos-2].transform.position + new Vector3(0.05f, 0.05f, 0f);
            puntos[NumPuntos-1].transform.name = "Punto_"+NumPuntos;
        }
    }

    void AgregarPuntoIntermedio()                   //Agrega un punto en donde se clickee sobre la linea
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition),PosicionMouse(), Color.red);

            if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.layer == 6)) //6 = layer "Linea"
            {
                double[] diferencia = new double[Linea.positionCount];
                double menor=10;

                for (int i = 0; i < Linea.positionCount; i++)
                {
                    diferencia[i] = Math.Sqrt(Math.Pow(hit.point.x - Linea.GetPosition(i).x, 2)
                                            +Math.Pow(hit.point.y - Linea.GetPosition(i).y, 2)
                                            +Math.Pow(hit.point.z - Linea.GetPosition(i).z, 2));
                    if (diferencia[i] < menor)
                    {
                        menor = diferencia[i];
                        segmento = (i/numberOfPoints)+1;
                    }
                }
                menor=10;
                NumPuntos += 1;
                puntos.Insert(segmento,Instantiate(PrefabPunto, ContenedorPuntos.transform, false));
                puntos[segmento].transform.position = hit.point;

                for (int i = 0; i < puntos.Count; i++)
                {
                    puntos[i].transform.name = "Punto_"+(i+1);
                }
            }
        }
    }

    void DibujarLinea(List<GameObject> points)      //Dibuja una linea curva entre los objetos punto
    {
        Linea.positionCount = numberOfPoints*(puntos.Count-1);
        
        Vector3 p0, p1, m0, m1;

        for(int j = 0; j < puntos.Count - 1; j++)
		{
			// determine control points of segment
			p0 = puntos[j].transform.position;
			p1 = puntos[j+1].transform.position;
			
			if (j > 0) 
			{
				m0 = 0.5f * (puntos[j+1].transform.position - puntos[j-1].transform.position);
			}
			else
			{
				m0 = puntos[j+1].transform.position - puntos[j].transform.position;
			}
			if (j < puntos.Count - 2)
			{
				m1 = 0.5f * (puntos[j+2].transform.position - puntos[j].transform.position);
			}
			else
			{
				m1 = puntos[j+1].transform.position - puntos[j].transform.position;
			}

			// set points of Hermite curve
			Vector3 position;
			float t;
			float pointStep = 1.0f / numberOfPoints;

			if (j == puntos.Count - 2)
			{
				pointStep = 1.0f / (numberOfPoints - 1.0f);
				// last point of last segment should reach p1
			}  
			for(int i = 0; i < numberOfPoints; i++) 
			{
				t = i * pointStep;
				position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 
					        + (t * t * t - 2.0f * t * t + t) * m0 
					        + (-2.0f * t * t * t + 3.0f * t * t) * p1 
					        + (t * t * t - t * t) * m1;
				Linea.SetPosition(i + j * numberOfPoints, position);
			}
		}
    }

    Vector3 PosicionMouse()                         //Devuelve la posicion del mouse en el espacio tridimensional
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.direction* 0.5f;
    }

    public void PublicarTrayectoria()               //Enviar trayectoria al cliente ROS
    {
        velocidad = DeslizadorVel.value*0.001f;

        figura = new float[Linea.positionCount,3];
        for (int i = 0; i < Linea.positionCount; i++)
        {
            /*
            NOTA: Aqui se aplica el cambio del sistema de refencia de Unity al sistema de referencia de ROS
            -------------------
            |  ROS  |  Unity  |
            |-------|---------|
            | Eje x |  Eje z  |
            | Eje y |  Eje x  |
            | Eje z |  Eje y  |
            -------------------
            */

            // Eje x
            figura[i,0] = -Linea.GetPosition(i).z;
            // Eje y
            figura[i,1] = Linea.GetPosition(i).x;
            // Eje z
            figura[i,2] = Linea.GetPosition(i).y;
        }
        publicador.Trayectoria(figura, Linea.positionCount, velocidad);
    }

    public void ExportarTrayectoria()               //Guardar linea en el almacenamiento interno
    {
        Traj = new DatosTrayectoria();
        Traj.MyArray = new List<Variables>();
        Traj.Muestreo = new int();
        Traj.Muestreo = numberOfPoints;
        for (int i = 0; i < Linea.positionCount; i++)
        {
            datos = new Variables();
            datos.ejex = new float();
            datos.ejey = new float();
            datos.ejez = new float();
            
            datos.ejex = (Linea.GetPosition(i).x);
            datos.ejey = (Linea.GetPosition(i).y);
            datos.ejez = (Linea.GetPosition(i).z);

            Traj.MyArray.Add(datos);
        }
        Exportar_importar.SaveData(Traj,Ubicacion.text,Nombre.text);
        Debug.Log("Exportado");
    }

    public void ImportarTrajEditable()              //Extraer datos de linea del almacenamiento interno (Linea editable = menos precision)
    {
        Traj = Exportar_importar.LoadData<DatosTrayectoria>(Ubicacion.text,Nombre.text);
        VaciarPuntos();
        NumPuntos =(Traj.MyArray.Count/Traj.Muestreo)+1;
        for (int i = 0; i < NumPuntos; i++)
        {
            int j = (i*Traj.Muestreo)-1;
            if (j<0)
                j=0;
            
            /******************
            NOTA:   El sistema de referencia usado es el que corresponde a unity
                    X: De izquierda a derecha
                    Y: De arriba a abajo
                    Z: De adelante hacia atras
            *******************/
            Vector3 posicion;
            // Eje x
            posicion.x = Traj.MyArray[j].ejex;
            // Eje y
            posicion.y = Traj.MyArray[j].ejey;
            // Eje z
            posicion.z = Traj.MyArray[j].ejez;
            
            if (i==0)
                puntos.Add(Instantiate(PrefabPuntoInical, ContenedorPuntos.transform, false));
            else
                puntos.Add(Instantiate(PrefabPunto, ContenedorPuntos.transform, false));
            puntos[i].transform.position = posicion;
            puntos[i].transform.name = "Punto_"+(i+1);
        }
        Editable = true;
        Debug.Log("importado");
    }

    public void ImportarTrajNoEditable()            //Extraer datos de linea del almacenamiento interno (Linea no editable = precision exacta)
    {
        Editable = false;

        VaciarPuntos();
        Traj = Exportar_importar.LoadData<DatosTrayectoria>(Ubicacion.text,Nombre.text);

        Linea.positionCount = Traj.MyArray.Count;
        for (int i = 0; i < Traj.MyArray.Count; i++)
        {
            /******************
            NOTA:   El sistema de referencia usado es el que corresponde a unity
                    X: De izquierda a derecha
                    Y: De arriba a abajo
                    Z: De adelante hacia atras
            *******************/
            Vector3 posicion;
            // Eje x
            posicion.x = Traj.MyArray[i].ejex;
            // Eje y
            posicion.y = Traj.MyArray[i].ejey;
            // Eje z
            posicion.z = Traj.MyArray[i].ejez;
            Linea.SetPosition(i,posicion);
        }
        Debug.Log("importado");
    }

    public void Reiniciar()                         //Elimina la linea actual y dibuja la linea inicial
    {
        VaciarPuntos();
        Inicializar_linea();
        Editable = true;
    }

    private void VaciarPuntos()                     //Elimina y resetea todos los objetos punto
    {
        if (puntos.Count>0)
        {
            for (int i = 0; i < NumPuntos; i++)
            {
                Destroy(puntos[i]);
            }
            puntos.Clear();
        }
    }
}