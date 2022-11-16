using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MostrarValor : MonoBehaviour
{
    public Slider Deslizador;
    public Text ValorTexto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AsignarValor();
    }

    public void AsignarValor()
    {
        ValorTexto.text =  Math.Round((float)(Deslizador.value),1) + "°";
    }

    public void ValorArticulacion()
    {
        ValorTexto.text =  Math.Round((float)(Deslizador.value),2) + "°";
    }

    public void Limpiar()
    {
        ValorTexto.text = " ";
    }

    public void ValorVelocidad()
    {
        ValorTexto.text =  Math.Round((Deslizador.value*0.1f),2)+ " cm/s";
    }
}
