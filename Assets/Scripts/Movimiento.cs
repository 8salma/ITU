using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5.0f;
    public Vector2 sensibilidad;
    public new Transform camera;
    public float jumpSpeed = 8.0f;
        public float gravedad = 20.0f;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtén la entrada del teclado
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcula la dirección del movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical);

        // Normaliza el vector de movimiento para evitar movimientos diagonales más rápidos
        movimiento.Normalize();

        // Aplica el movimiento al personaje
        transform.Translate(movimiento * velocidad * Time.deltaTime);

        // Salto
        if (Input.GetKey(KeyCode.Space))
        {
            movimiento.y = jumpSpeed;
            Debug.Log("aaa");
        }
        movimiento.y -= gravedad * Time.deltaTime;

        UpdateMouseLook();
    }

    // Movimiento camara en primera persona
    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensibilidad.x, 0);
        }

        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensibilidad.y + 360) % 360;

            if (rotation.x > 80 && rotation.x < 180)
            {
                rotation.x = 80;
            }
            if (rotation.x < 280 && rotation.x > 180)
            {
                rotation.x = 280;
            }
            camera.Rotate(-ver * sensibilidad.y, 0, 0);
        }
    }

}
