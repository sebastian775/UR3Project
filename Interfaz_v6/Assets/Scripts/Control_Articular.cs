using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RosSharp.Urdf;

public class Control_Articular : MonoBehaviour
{
    public List<UrdfJoint> urdfJoint = new List<UrdfJoint>(6);
    private float[] position = new float[6];
    private Slider[] Deslizador = new Slider[6];
    private Text[] TextoRobot = new Text[6];
    private double[] ValorRobot = new double[6];
    private Text[] TextoSlider = new Text[6];
    [SerializeField] private Control_Panel CPanel;

////////
    [HideInInspector] public int NumPos = 0;
    private GameObject[] poses = new GameObject[100];
    private float[] valores = new float[6]; 
    public GameObject PrefabPosiciones;
    public RectTransform content;
    private TMP_Text NumeroPosicion;
    public GameObject Articular;
    public GameObject CuadroDialogo;
    private int NP;



///////

    public void Inicializar_Art() 
    {    
        if (CPanel.P_Articular)
        {
            for(int i=0; i<6; i++)
            {
                Deslizador[i] = GameObject.Find("Art"+(i+1)+"/Slider").GetComponent<Slider>();
                TextoRobot[i] = GameObject.Find("Art"+(i+1)+"/ValorRobot").GetComponent<Text>();
                TextoSlider[i] = GameObject.Find("Art"+(i+1)+"/ValorSlider").GetComponent<Text>();
            }

            for (int i = 0; i < urdfJoint.Count; i++)
            {
                position[i] = urdfJoint[i].GetPosition();
                Deslizador[i].value = position[i]*(180/Mathf.PI);
                ValorRobot[i] = Math.Round((position[i]*(180/Mathf.PI)),2);
                TextoRobot[i].text = ValorRobot[i] + "°";
                TextoSlider[i].text = " ";
            }
        }
    }

    void Update()
    {
        if (CPanel.P_Articular)
        {
            for (int i = 0; i < urdfJoint.Count; i++)
            {
                position[i] = urdfJoint[i].GetPosition();
                ValorRobot[i] = Math.Round((position[i]*(180/Mathf.PI)),2);
                TextoRobot[i].text =  ValorRobot[i] + "°";
                if (ValorRobot[i] == Math.Round(Deslizador[i].value,2))
                {
                    LimpiarArticulacion(i);
                }
            }
        }
    }

    public void BotonMas(int NumArt)
    {
        Deslizador[NumArt-1].value = Deslizador[NumArt-1].value + (float)0.1;
    }

    public void BotonMenos(int NumArt)
    {
        Deslizador[NumArt-1].value = Deslizador[NumArt-1].value - (float)0.1;
    }

    public void ValorArticulacion(int NumArt)
    {
        TextoSlider[NumArt-1].text =  Math.Round((float)(Deslizador[NumArt-1].value),2) + "°";
    }

    public void LimpiarArticulacion(int NumArt)
    {
        TextoSlider[NumArt].text = " ";
    }

    public void AgregarPosicion()
    {
        if (NumPos<poses.Length)
        {
            for(int i = 0; i < 6; i++)
            {
                valores[i] = Deslizador[i].value;
            }
            poses[NumPos] = Instantiate(PrefabPosiciones, content, false);
            poses[NumPos].transform.name = "Posi"+(NumPos);
            poses[NumPos].transform.position = poses[NumPos].transform.position + new Vector3(0, -50*NumPos , 0);

            for(int i = 0; i<6; i++)
            {
                TMP_Text texto = GameObject.Find("Posi"+(NumPos)+"/VJoint"+(i+1)).GetComponent<TMP_Text>();
                texto.text = (Math.Round(valores[i],2)).ToString();
            }
            NumeroPosicion = GameObject.Find("Posi"+(NumPos)+"/NumPosicion").GetComponent<TMP_Text>();
            NumeroPosicion.text = (NumPos+1).ToString();

            ConfigContent(70+(50*NumPos));
            NumPos ++;
        }else
        {
            //Mensaje de advertencia
            print("Limite de posiciones alcanzado");
        }
    }

    public void EliminarPosicion()
    {
        if (NumPos>0)
        {   
            //Preguntar NP
            Articular.SetActive(false);
            CuadroDialogo.SetActive(true);
        }
    }

    public void BotonEliminar()
    {
        NP = int.Parse((GameObject.Find("DialogoEliminar/DialogoPanel/Dato").GetComponent<TMP_InputField>().text));

        if ((0<NP)&&(NP<=NumPos))
        {
            //print(NP);
            Destroy(poses[NP-1]);
            for (int i = NP; i < NumPos; i++)
            {
                GameObject.Find("Posi"+(i)+"/NumPosicion").GetComponent<TMP_Text>().text = (i).ToString();
                poses[i-1] = poses[i];
                poses[i-1].transform.name = "Posi"+(i-1);
                poses[i-1].transform.position = poses[i].transform.position + new Vector3(0, 50 , 0);
            }
            Articular.SetActive(true);
            CuadroDialogo.SetActive(false);
            NumPos = NumPos-1;
            ConfigContent(70+(50*NumPos));
        }else
        {
            //Mensaje de error
            print("Numero de posición incorrecta");
            Articular.SetActive(true);
            CuadroDialogo.SetActive(false);
        }
    }

    public void BotonCancelar()
    {
        Articular.SetActive(true);
        CuadroDialogo.SetActive(false);
    }

    public void EliminarTodo()
    {
        if (NumPos>0)
        { 
            for (int i = 0; i < NumPos; i++)
            {
                Destroy(poses[i]);
            }
            NumPos = 0;
            ConfigContent(70+(50*NumPos));
        }
    }

    private void ConfigContent(float largo)
    {
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, largo);
    }




    /*public static void EjecutarComando(string command)
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
        //EjecutarComando("gnome-terminal -- bash -ic 'cd $Home; cd Documentos/Proyecto_UR/Interfaz_v5/Assets/ScriptsPython; python3 contador.py'");
    }*/
}
