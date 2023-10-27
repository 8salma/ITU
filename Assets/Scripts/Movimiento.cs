using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5.0f;
    public Vector2 sensibilidad;
    public new Transform camera;
    private bool enSuelo = true; // Variable para verificar si el personaje está en el suelo
    public float saltoFuerza = 8.0f; // Fuerza del salto


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
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

        UpdateMouseLook();

        // Verificar si se presiona la tecla de espacio y el personaje está en el suelo
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            // Aplicar una fuerza vertical para el salto
            GetComponent<Rigidbody>().AddForce(Vector3.up * saltoFuerza, ForceMode.Impulse);
            enSuelo = false; // El personaje ya no está en el suelo
        }
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
    // Detectar cuando el personaje toca el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo")) // Ajusta la etiqueta según tu escenario
        {
            enSuelo = true; // El personaje está en el suelo
        }
    }

    // Detectar cuando el personaje se separa del suelo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo")) // Ajusta la etiqueta según tu escenario
        {
            enSuelo = false; // El personaje ya no está en el suelo
        }
    }
}
