using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Panel : MonoBehaviour
{
    // Variables
    [HideInInspector] public bool P_Conexion;
    [HideInInspector] public bool P_Articular;
    [HideInInspector] public bool P_Cartesiano;
    [HideInInspector] public bool P_Predefinidas;

    public GameObject PanelCon;
    public GameObject PanelArt;
    public GameObject PanelCart1;
    public GameObject PanelCart2;
    public GameObject PanelPred;

    void Start()
    {
        P_Conexion = false;
        P_Articular = false;
        P_Cartesiano = false;
        P_Predefinidas = false;
    }

    public void Conexion()
    {
        P_Conexion = !P_Conexion;
        P_Articular = false;
        P_Cartesiano = false;
        P_Predefinidas = false;
        CambioPanel();
    }

    public void Articular()
    {
        P_Conexion = false;
        P_Articular = !P_Articular;
        P_Cartesiano = false;
        P_Predefinidas = false;
        CambioPanel();
    }

    public void Cartesiano()
    {
        P_Conexion = false;
        P_Articular = false;
        P_Cartesiano = !P_Cartesiano;
        P_Predefinidas = false;
        CambioPanel();
    }

    public void Predefinidas()
    {
        P_Conexion = false;
        P_Articular = false;
        P_Cartesiano = false;
        P_Predefinidas = !P_Predefinidas;
        CambioPanel();
    }

    public void  CambioPanel()
    {
        PanelCon.SetActive(P_Conexion);
        PanelArt.SetActive(P_Articular);
        PanelCart1.SetActive(P_Cartesiano);
        PanelCart2.SetActive(P_Cartesiano);
        PanelPred.SetActive(P_Predefinidas);
    }
}
