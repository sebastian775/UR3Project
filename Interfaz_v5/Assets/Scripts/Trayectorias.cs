using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trayectorias : MonoBehaviour
{
    [HideInInspector] public float[,] figura = new float[500,3]; 
    public RosSharp.RosBridgeClient.TrajPublisher publicador;
    public TMP_InputField[] CirculoCoor = new TMP_InputField[3];
    public TMP_InputField CirculoRadio;
    public TMP_InputField[] CuadradoCoor = new TMP_InputField[3];
    public TMP_InputField CuadradoLado;
    public TMP_InputField[] EspiralCoor = new TMP_InputField[3];
    public TMP_InputField EspiralRadio;

    public void Circulo()
    {
        int Tfinal = 50;
        double Tem = 1;
        int nbench = (int)(Tfinal/Tem);
        float Radio = float.Parse(CirculoRadio.text);
        float desviacionX = float.Parse(CirculoCoor[0].text);
        float desviacionY = float.Parse(CirculoCoor[1].text);
        float altura = float.Parse(CirculoCoor[2].text);
        float[] angulo;

        angulo = linspace(0, 2*Mathf.PI, 500);
        
        for (int i = 0; i < 500; i++)
        {
            figura[i,0] = ((Radio * Mathf.Cos(angulo[i]))+desviacionX);
            figura[i,1] = ((Radio * Mathf.Sin(angulo[i]))+desviacionY);
            figura[i,2] = altura;
        }
        publicador.Figuras(figura);
    }

    public void EspiralConica()
    {
        int Tfinal = 30;
        double Tem = 0.05;
        int nbech = (int)(Tfinal/Tem)+1;
        double t=0;
        float Radio = float.Parse(EspiralRadio.text);
        float desviacionX = float.Parse(EspiralCoor[0].text);
        float desviacionY = float.Parse(EspiralCoor[1].text);
        float altura = float.Parse(EspiralCoor[2].text);

        for (int i = 0; i < 500; i++)
        {
            t=t+Tem;
            figura[i,0] = (float)((Radio*t*Mathf.Sin((float)t))+desviacionX);
            figura[i,1] = (float)((Radio*t*Mathf.Cos((float)t))+desviacionY);
            figura[i,2] = (float)((0.008*t)+altura);
        }
        publicador.Figuras(figura);
    }

    public void Rombo()
    {

    }

    public static float[] linspace(float startval, float endval, int steps)
    {
        float interval = (endval / Mathf.Abs(endval)) * Mathf.Abs(endval - startval) / (steps - 1);
        return (from val in Enumerable.Range(0,steps)
                select startval + (val * interval)).ToArray(); 
    }
}
