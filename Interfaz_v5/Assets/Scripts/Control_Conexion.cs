using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RosSharp.RosBridgeClient
{
[RequireComponent(typeof(RosConnector))]
public class Control_Conexion : MonoBehaviour
{
    
    //public GameObject ROS;

    public RosConnector ROS;
    public JointStateSubscriber SubArt;

    public CoordenadaSubscriber SubCoor;
    public CoorPublisher PubCoor; 
    public ArtPublisher PubArt;
    public TrajPublisher PubTraj;
    public TMP_Text Estado;


    [SerializeField] private Control_Panel CPanel;

    
    void Start()
    {
        //EjecutarComando("gnome-terminal -- bash -ic 'cd $HOME; roslaunch rosbridge_server rosbridge_websocket.launch'");
        Estado.text = ("Estado: Conectado");
    }

    void Update()
    {

    }

    public void DesconectarRos() 
    {    
        Invoke("",1.0f);
        Estado.text = ("Estado: Desonectado");
    }

    public void ConectarRos()
    {
        //ROS.SetActive(true);
        ROS.AsignarIP();
        Invoke("ActivarPubSub", 1.0f);
        //Invoke("Controlador", 3.0f);
        Estado.text = ("Estado: Conectado");
        //EjecutarComando("gnome-terminal -- bash -ic 'cd $Home; cd Documentos/Proyecto_UR/Interfaz_v5/Assets/ScriptsPython; python3 ListarDatos.py'");
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

    private void ActivarPubSub()
    {
        SubArt.enabled = true;
        SubCoor.enabled = true;
        PubArt.enabled = true;
        PubCoor.enabled = true;
        PubTraj.enabled = true;
    }

    private void DesctivarPubSub()
    {
        SubArt.enabled = false;
        SubCoor.enabled = false;
        PubArt.enabled = false;
        PubCoor.enabled = false;
        PubTraj.enabled = false;
    }

    private void Controlador()
    {
        //EjecutarComando("gnome-terminal -- bash -ic 'cd $HOME; roslaunch ur_robot_driver ur3_bringup.launch robot_ip:=192.168.50.72 limited:=true'");
    }
}
}
