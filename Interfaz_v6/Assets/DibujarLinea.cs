using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DibujarLinea : MonoBehaviour
{
    // Start is called before the first frame update

    public float Ganancia = 10;

    List<Vector3> LinePoints;

    Vector3 PosicionAnterior;
    float timer;
    public float  TimerDelay;
    GameObject Linea;
    LineRenderer DrawLinea;
    public float AnchoLinea = 0.1f;

    void Start()
    {
        LinePoints = new List<Vector3>();
        timer = TimerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Linea = new GameObject();
            DrawLinea = Linea.AddComponent<LineRenderer>();
            DrawLinea.material = new Material(Shader.Find("Sprites/Default"));
            DrawLinea.startColor = Color.red;
            DrawLinea.endColor = Color.red;
            DrawLinea.startWidth = AnchoLinea;
            DrawLinea.endWidth = AnchoLinea;
        }
        if(Input.GetMouseButton(0))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition),PosicionMouse(), Color.red);
            timer -= TimerDelay;
            if (timer <= 0)
            {
                if (Input.mousePosition != PosicionAnterior)
                {
                    LinePoints.Add(PosicionMouse());
                    DrawLinea.positionCount = LinePoints.Count;
                    DrawLinea.SetPositions(LinePoints.ToArray());

                    timer = TimerDelay;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            LinePoints.Clear();
        }
        PosicionAnterior = Input.mousePosition;
        //Debug.Log(Input.mousePosition);
    }

    Vector3 PosicionMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.direction * Ganancia;
        // ray.origin +
    }
}
