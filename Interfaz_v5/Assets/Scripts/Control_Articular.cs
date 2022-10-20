using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RosSharp.Urdf;

public class Control_Articular : MonoBehaviour
{
    public List<UrdfJoint> urdfJoint = new List<UrdfJoint>(6);
    private float[] position = new float[6];
    private Slider[] Deslizador = new Slider[6];
    private Text[] Valor = new Text[6];
    private Button[,] Botones = new Button[6,2];
    [SerializeField] private Control_Panel CPanel;

    public void Inicializar_Art() 
    {    
        if (CPanel.P_Articular)
        {
            for(int i=0; i<6; i++)
            {
                Deslizador[i] = GameObject.Find("SliderArt"+(i+1)).GetComponent<Slider>();
                Valor[i] = GameObject.Find("SliderArt"+(i+1)+"/Valor").GetComponent<Text>();
                for (int j=0; j<2; j++)
                {
                    Botones[i,j] = GameObject.Find("SliderArt"+(i+1)+"/Botones/B"+(j+1)).GetComponent<Button>();
                }
            }

            for (int i = 0; i < urdfJoint.Count; i++)
            {
                position[i] = urdfJoint[i].GetPosition();
                Deslizador[i].value = position[i]*(180/Mathf.PI);
                Valor[i].text = (position[i]*(180/Mathf.PI)) + "°";
            }
        }
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
    public void contador()
    {
        EjecutarComando("gnome-terminal -- bash -ic 'cd $Home; cd Documentos/Proyecto_UR/Interfaz_v5/Assets/ScriptsPython; python3 contador.py'");
    }
}
