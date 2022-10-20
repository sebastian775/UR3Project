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
        
    }

    public void AsignarValor()
    {
        ValorTexto.text = (float)(Deslizador.value) + "°";
    }
}
