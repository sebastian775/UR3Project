using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Control_Cartesiano : MonoBehaviour
{
    public GameObject PrefabCoordenadas;
    public RectTransform content;
    public GameObject CuadroDialogo;
    public GameObject Cartesiano;
    private TMP_InputField[] coordenadas = new TMP_InputField[7];    // Vector de casillas de las coordenadas
    private float[] valores = new float[7];                         // Valores de las coordenadas
    [HideInInspector]public int NumCoor = 0;
    private GameObject[] puntos = new GameObject[100];
    private string Nombre;
    private int NT;
    [SerializeField] private Control_Panel CPanel;


    public void inicializar()
    {
        if (CPanel.P_Cartesiano)
        {
            for(int i=0; i<7; i++)
            {
                string NombreCoor = NumToCoor(i);
                coordenadas[i] = GameObject.Find(NombreCoor).GetComponent<TMP_InputField>();
            }
            NumCoor = 0;

            //EjecutarComando("gnome-terminal -- bash -ic 'cd $Home; cd Documentos/Pryecto_UR5/Interfaz_v5/Assets/ScriptsPython; pwd; python3 ListaCoordenadas.py'");
        }
    }

    public void EliminarCoordenada()
    {
        if (NumCoor>0)
        {   
            //Preguntar NT
            Cartesiano.SetActive(false);
            CuadroDialogo.SetActive(true);
        }
    }

    public void eliminar()
    {
        NT = int.Parse((GameObject.Find("DialogoEliminar/DialogoPanel/Dato").GetComponent<TMP_InputField>().text));

        if ((0<NT)&&(NT<=NumCoor))
        {
            //print(NT);
            Destroy(puntos[NT-1]);
            for (int i = NT; i < NumCoor; i++)
            {
                puntos[i].transform.name = "Coor"+(i-1);
                puntos[i].transform.position = puntos[i].transform.position + new Vector3(0, 50 , 0);
                GameObject.Find("Coor"+(i-1)+"/NumTra").GetComponent<TMP_Text>().text = (i-1).ToString();
            }
            Cartesiano.SetActive(true);
            CuadroDialogo.SetActive(false);
            NumCoor = NumCoor-1;
            ConfigContent(70+(50*NumCoor));
        }else
        {
            //Mensaje de error
            print("Numero de coordenada incorrecta");
            //Cartesiano.SetActive(true);
            //CuadroDialogo.SetActive(false);
        }
    }
    
    public void EliminarTodo()
    {
        
    }

    public void cancelar()
    {
        Cartesiano.SetActive(true);
        CuadroDialogo.SetActive(false);
    }

    public void AgregarCoordenada()
    {
        if (NumCoor<puntos.Length)
        {
            for(int i = 0; i < 7; i++)
            {
                valores[i] = float.Parse(coordenadas[i].text);
                //Debug.Log(float.Parse(var[i]));
            }
            puntos[NumCoor] = Instantiate(PrefabCoordenadas, content, false);
            puntos[NumCoor].transform.name = "Coor"+(NumCoor);
            puntos[NumCoor].transform.position = puntos[NumCoor].transform.position + new Vector3(0, -50*NumCoor , 0);

            for(int i = 0; i<7; i++)
            {
                string NombreCoor = NumToCoor(i);
                TMP_Text texto = GameObject.Find("Coor"+(NumCoor)+"/V"+NombreCoor).GetComponent<TMP_Text>();
                texto.text = valores[i].ToString();
            }
            TMP_Text NumeroTrajectoria = GameObject.Find("Coor"+(NumCoor)+"/NumTra").GetComponent<TMP_Text>();
            NumeroTrajectoria.text = (NumCoor+1).ToString();

            ConfigContent(70+(50*NumCoor));
            NumCoor ++;
        }else
        {
            //Mensaje de advertencia
            print("Limite de coordenadas alcanzado");
        }
    }

    public string NumToCoor(int numero)
    {
        switch (numero)
        {
            case 0:
            Nombre = "PosX";
                break;
            case 1:
            Nombre = "PosY";
                break;
            case 2:
            Nombre = "PosZ";
                break;
            case 3:
            Nombre = "OriX";
                break;
            case 4:
            Nombre = "OriY";
                break;
            case 5:
            Nombre = "OriZ";
                break;
            case 6:
            Nombre = "OriW";
                break;
        }
        return(Nombre);
    }

    private void ConfigContent(float largo)
    {
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, largo);
    }

    public static void EjecutarComando(string command)
    {
        Process proc = new System.Diagnostics.Process ();
        proc.StartInfo.FileName = "/bin/bash";
        proc.StartInfo.Arguments = "-c \" " + command + " \"";
        proc.StartInfo.UseShellExecute = false; 
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start ();

        while (!proc.StandardOutput.EndOfStream) {
            Console.WriteLine (proc.StandardOutput.ReadLine ());
        }
    }

}
