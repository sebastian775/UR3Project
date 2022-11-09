using UnityEngine;

public class Observador : MonoBehaviour
{
    public float VelMov;
    public float VelRot;
    Vector3 Movimiento;
    Vector2 Rotacion;
    Vector3 PosicionInicial;
    Quaternion RotacionInicial;

    void Awake()
    {
        PosicionInicial = transform.position;
        RotacionInicial = transform.rotation;
    }

    void Update()
    {
        Movimiento.x = Input.GetAxis("Horizontal")* VelMov * Time.deltaTime;
        Movimiento.y = Input.GetAxis("Vertical")* VelMov * Time.deltaTime;
        Movimiento.z = Input.GetAxis("Altura")* VelMov * Time.deltaTime;
        Rotacion.x = Input.GetAxis("RotHorizontal")* VelRot * Time.deltaTime;
        

        MoverVista();

        if(Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarVista();
        }

    }

    void MoverVista()
    {
        transform.Translate(Movimiento.x, Movimiento.z, Movimiento.y);
        transform.Rotate(0, Rotacion.x, 0);
    }

    void ReiniciarVista()
    {
        transform.position = PosicionInicial;
        transform.rotation = RotacionInicial;

    }

}