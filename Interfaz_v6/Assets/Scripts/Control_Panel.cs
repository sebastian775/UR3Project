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
    [HideInInspector] public bool P_Posiciones;
    [HideInInspector] public bool P_Coordenadas;

    public GameObject PanelCon;
    public GameObject PanelArt;
    public GameObject PanelPos;
    public GameObject PanelCart;
    public GameObject PanelCoor;
    public GameObject PanelPred;


    void Start()
    {
        P_Conexion = false;
        P_Articular = false;
        P_Cartesiano = false;
        P_Predefinidas = false;
        P_Posiciones = false;
        P_Coordenadas = false;
    }

    public void Conexion()
    {
        P_Conexion = !P_Conexion;
        P_Articular = false;
        P_Cartesiano = false;
        P_Predefinidas = false;
        P_Posiciones = false;
        P_Coordenadas = false;
        CambioPanel();
    }

    public void Articular()
    {
        P_Conexion = false;
        P_Articular = !P_Articular;
        P_Cartesiano = false;
        P_Predefinidas = false;
        P_Posiciones = false;
        P_Coordenadas = false;
        CambioPanel();
    }

    public void Cartesiano()
    {
        P_Conexion = false;
        P_Articular = false;
        P_Cartesiano = !P_Cartesiano;
        P_Predefinidas = false;
        P_Posiciones = false;
        P_Coordenadas = false;
        CambioPanel();
    }

    public void Predefinidas()
    {
        P_Conexion = false;
        P_Articular = false;
        P_Cartesiano = false;
        P_Predefinidas = !P_Predefinidas;
        P_Posiciones = false;
        P_Coordenadas = false;
        CambioPanel();
    }

    public void Posiciones()
    {
        P_Posiciones = !P_Posiciones;
        PanelPos.SetActive(P_Posiciones);
    }

    public void Coordenadas()
    {
        P_Coordenadas = !P_Coordenadas;
        PanelCoor.SetActive(P_Coordenadas);
    }

    public void  CambioPanel()
    {
        PanelCon.SetActive(P_Conexion);
        PanelArt.SetActive(P_Articular);
        PanelPos.SetActive(P_Posiciones);
        PanelCart.SetActive(P_Cartesiano);
        PanelCoor.SetActive(P_Coordenadas);
        PanelPred.SetActive(P_Predefinidas);
    }
}
